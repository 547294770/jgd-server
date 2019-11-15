using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SK.Common.Caches
{
    public class UserCache
    {
        
        public static void AddUser(int userId, object userInfo)
        {
            string key = string.Concat(Consts.USER_INFO, "_", userId);
            HttpContextCache.Add(key, userInfo);
        }

        public static void AddUser(string token, object userInfo)
        {
            string key = string.Concat(Consts.USER_INFO, "_", token);
            HttpContextCache.Add(key, userInfo);
        }

        public static object GetUser(int userId)
        {
            string key = string.Concat(Consts.USER_INFO, "_", userId);
            return  HttpRuntime.Cache.Get(key);
        }

        public static object GetUser(string token)
        {
            string key = string.Concat(Consts.USER_INFO, "_", token);
            return HttpRuntime.Cache.Get(key);
        }

        public static void RemoveUser(int userId)
        {
            string key = string.Concat(Consts.USER_INFO, "_", userId);
            HttpContextCache.Remove(key);
        }

        public static void RemoveUser(string token)
        {
            string key = string.Concat(Consts.USER_INFO, "_", token);
            HttpContextCache.Remove(key);
        }
    }
}
