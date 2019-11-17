using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Common
{
    public class Config
    {
        public static AppSetting Setting
        {
            get {
                return new AppSetting();
            }
        }
    }

    public class AppSetting
    {
        public string UploadPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UploadPath"];
            }
        }

        public string WXWebHost
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WXWebHost"];
            }
        }
    }
}
