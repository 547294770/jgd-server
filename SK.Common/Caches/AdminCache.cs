using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SK.Common.Caches
{
    public class AdminCache
    {
        
        public static void AddAdmin(int adminId, object adminInfo)
        {
            string key = string.Concat(Consts.ADMIN_INFO, "_", adminId);
            HttpContextCache.Add(key, adminInfo);
        }

        public static void AddAdmin(string token, object adminInfo)
        {
            string key = string.Concat(Consts.ADMIN_INFO, "_", token);
            HttpContextCache.Add(key, adminInfo);
        }

        public static object GetAdmin(int adminId)
        {
            string key = string.Concat(Consts.ADMIN_INFO, "_", adminId);
            return  HttpRuntime.Cache.Get(key);
        }

        public static object GetAdmin(string token)
        {
            string key = string.Concat(Consts.ADMIN_INFO, "_", token);
            return HttpRuntime.Cache.Get(key);
        }

        public static void RemoveAdmin(int adminId)
        {
            string key = string.Concat(Consts.ADMIN_INFO, "_", adminId);
            HttpContextCache.Remove(key);
        }

        public static void RemoveAdmin(string token)
        {
            string key = string.Concat(Consts.ADMIN_INFO, "_", token);
            HttpContextCache.Remove(key);
        }
    }
}
