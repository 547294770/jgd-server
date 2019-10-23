using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SK.Handler;

namespace SK.Admin.Controllers
{
    public class Global : BasePage
    {
        public void init()
        {
            //var enums = new { 
            //    dd = 0
            //};

            Assembly assm = Assembly.Load("SK.Entities");
            Dictionary<string,object> dict = new Dictionary<string,object>();
            var enums = assm.GetTypes().Where(p=>p.IsEnum);
            foreach (var emType in enums)
            {
                var names = Enum.GetNames(emType);
                Dictionary<string, string> subDict = new Dictionary<string, string>();

                foreach (var name in names)
                {
                    var field = emType.GetField(name);
                    if (field == null) {

                        dict.Add(emType.Name, string.Empty);
                        break;
                    }

                    var attrs = field.GetCustomAttributesData();
                    var description = "";

                    foreach (var item in attrs)
                    {
                        if (item.AttributeType == typeof(DescriptionAttribute))
                        {
                            if (item.ConstructorArguments.Count > 0)
                            {
                                description = (string)item.ConstructorArguments[0].Value;
                            }
                            break;
                        }
                    }

                    
                    subDict.Add(name, description);
                }

                dict.Add(emType.FullName, subDict);
            }


            string json = JsonConvert.SerializeObject(dict);

            this.Response.Write(json);
            this.Response.End();
        }
    }
}
