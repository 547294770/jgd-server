using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CreateTableTool
{
    class Program
    {
        private static DataTable columnSchemTable;

        static void Main(string[] args)
        {
            Console.WriteLine("输入表名：");

            var templateFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "template.txt");
            var subtemplateFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "subtemplate.txt");

            if (args.Length < 1) {
                Console.WriteLine("请设置参数：$(ItemPath)");
                return;
            }

            string filePath = args[0];
            string tablename = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(tablename))
            {
                Console.WriteLine("输入表名：");
                tablename = Console.ReadLine();
            }

            string newTableName = tablename;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                var tables = conn.GetSchema(SqlClientMetaDataCollectionNames.Tables);
                var isExists = false;

                while (!isExists)
                {
                    foreach (DataRow dr in tables.Rows)
                    {
                        var eq = string.Compare(dr["TABLE_NAME"].ToString(), tablename, true);
                        if (eq == 0)
                        {
                            isExists = true;
                        }
                    }

                    if (!isExists)
                    {
                        Console.WriteLine("表不存在,请重新输入：");
                        tablename = Console.ReadLine();
                    }
                }
                
                string sqlText = "Select top 1 * from [" + tablename + "]";
                SqlCommand comm = new SqlCommand(sqlText, conn);
                SqlDataReader reader = comm.ExecuteReader();
                System.Data.DataTable dt = reader.GetSchemaTable();
                var tableProperty = GetTableExtendedProperties(tablename);

                //[Type=DateTime]
                if (tableProperty.ContainsKey("MS_Description"))
                {
                    var proValue = tableProperty["MS_Description"].ToString();
                    var isMatch = Regex.IsMatch(proValue, "\\[Name=(?<TableName>.+?)\\]", RegexOptions.IgnorePatternWhitespace);
                    if (isMatch)
                    {
                        var match = Regex.Match(proValue, "\\[Name=(?<TableName>.+?)\\]", RegexOptions.IgnorePatternWhitespace);
                        newTableName = match.Groups["TableName"].Value;
                    }
                }

                var subtemplate = File.ReadAllText(subtemplateFile);
                List<string> pros = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    string attrs = "";
                    if (dr["IsIdentity"].ToString().Equals("True"))
                    {
                        attrs += ",IsPrimaryKey = true ,IsDbGenerated = true,IsVersion = true, AutoSync = AutoSync.OnInsert";
                    }
                    var template = subtemplate;
                    var columnName = dr["ColumnName"].ToString();
                    var newColumnName = columnName;
                    var dataType = dr["DataType"].ToString();
                    var newDataType = dataType;
                    var columnProperty = GetColumnExtendedProperties(tablename, columnName);
                    var typePattern = "\\[Type=(?<Type>.+?)\\]";
                    var namePattern = "\\[Name=(?<Name>.+?)\\]";
                    var msDescription = "MS_Description";

                    //[Type=DateTime]
                    if (columnProperty.ContainsKey(msDescription))
                    {
                        var proValue = columnProperty[msDescription].ToString();
                        var isMatch = Regex.IsMatch(proValue, typePattern, RegexOptions.IgnorePatternWhitespace);
                        if (isMatch)
                        {
                            var match = Regex.Match(proValue, typePattern, RegexOptions.IgnorePatternWhitespace);
                            newDataType = match.Groups["Type"].Value;
                        }

                        isMatch = Regex.IsMatch(proValue, namePattern, RegexOptions.IgnorePatternWhitespace);
                        if (isMatch)
                        {
                            var match = Regex.Match(proValue, namePattern, RegexOptions.IgnorePatternWhitespace);
                            newColumnName = match.Groups["Name"].Value;
                        }
                    }

                    switch (newDataType)
                    {
                        case "System.String":
                            newDataType = "string";
                            break;
                        case "System.Decimal":
                            newDataType = "decimal";
                            break;
                        case "System.Int32":
                            newDataType = "int";
                            break;
                        case "System.Int64":
                            newDataType = "long";
                            break;
                        case "System.DateTime":
                            newDataType = "DateTime";
                            break;
                        case "System.Byte":
                            newDataType = "byte";
                            break;
                        case "System.Boolean":
                            newDataType = "bool";
                            break;
                        default:
                            break;
                    }

                    template = Regex.Replace(template, "{{attrs}}", attrs, RegexOptions.IgnorePatternWhitespace);
                    template = Regex.Replace(template, "{{datatype}}", newDataType, RegexOptions.IgnorePatternWhitespace);
                    template = Regex.Replace(template, "{{columname1}}", columnName, RegexOptions.IgnorePatternWhitespace);
                    template = Regex.Replace(template, "{{columname2}}", newColumnName, RegexOptions.IgnorePatternWhitespace);

                    pros.Add(template);

                }

                string entString = File.ReadAllText(templateFile);

                entString = Regex.Replace(entString, "{{tablename1}}", tablename, RegexOptions.IgnorePatternWhitespace);
                entString = Regex.Replace(entString, "{{tablename2}}", newTableName, RegexOptions.IgnorePatternWhitespace);
                entString = Regex.Replace(entString, "{{Content}}", string.Join("\r\n\r\n        ", pros), RegexOptions.IgnorePatternWhitespace);

                

                using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    writer.Write(entString);
                }
            }
        }

        public static string GetConnectionString()
        {
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return connection;
        }

        public static PropertyCollection GetColumnExtendedProperties(string tableName, string colName)
        {
            string sql = @"select C.name AS ColumnName,T.name as TableName,E.name as ExtendName,E.value as ExtendValue from sys.columns C,sys.tables T,sys.extended_properties E
where C.column_id = E.minor_id and T.object_id = E.major_id and c.object_id = T.object_id
and T.name = '" + tableName + @"'";

            if (columnSchemTable == null)
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(sql, conn);

                    SqlDataAdapter ada = new SqlDataAdapter(comm);
                    columnSchemTable = new DataTable();
                    ada.Fill(columnSchemTable);

                    ada.Dispose();
                    comm.Dispose();

                }
            }

            PropertyCollection pro = new PropertyCollection();

            if (columnSchemTable.Rows.Count > 0)
            {
                foreach (System.Data.DataRow dr in columnSchemTable.Rows)
                {
                    string columnName = dr["ColumnName"].ToString();
                    if (columnName == colName)
                    {
                        pro.Add(dr["ExtendName"].ToString(), dr["ExtendValue"].ToString());
                        break;
                    }
                }
            }

            return pro;
        }

        public static PropertyCollection GetTableExtendedProperties(string tableName)
        {
            string sql = @"select T.name as TableName,E.name as ExtendName,E.value as ExtendValue
                             from sys.extended_properties E,
                            sys.tables T
                            where E.major_id = T.object_id AND E.minor_id = 0 AND T.name = '" + tableName + @"'";

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);

                SqlDataAdapter ada = new SqlDataAdapter(comm);
                DataTable newTable = new DataTable();
                ada.Fill(newTable);

                PropertyCollection pro = new PropertyCollection();

                if (newTable.Rows.Count > 0)
                {

                    foreach (System.Data.DataRow dr in newTable.Rows)
                    {
                        pro.Add(dr["ExtendName"].ToString(), dr["ExtendValue"].ToString());
                    }
                }

                ada.Dispose();
                comm.Dispose();
                return pro;
            }

        }
    }
}
