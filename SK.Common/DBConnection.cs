using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Common
{
    public class DBConnection
    {
        public static string DefaultConnecetion
        {
            get
            {
                string connection = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                return connection;
            }
        }
    }
}
