using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace SK.Common
{
    public class HttpContextCache
    {
        public static void Add(string key, object value)
        {
            string webconfigPath = HttpContext.Current.Server.MapPath("~/bin/cachelisten.txt");
            if (!System.IO.File.Exists(webconfigPath))
            {
                webconfigPath = HttpContext.Current.Server.MapPath("~/web.config");
            }

            HttpRuntime.Cache.Add(key, value, new System.Web.Caching.CacheDependency(webconfigPath),
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                //TimeSpan.FromMinutes(20),
                System.Web.Caching.Cache.NoSlidingExpiration,
                System.Web.Caching.CacheItemPriority.Default,
                CacheItemRemovedCallback);

           
            //HttpRuntime.Cache.Add([key] = value;
        }

        public static void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public static void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        { 
            
        }
    }
}
