using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SK.Handler
{
    public class BaseHandler : IHttpHandler
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        public virtual bool IsReusable
        {
            get { return true; }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            string[] paths = context.Request.Url.AbsolutePath.Substring(1).Split('/');
            if (paths.Length < 3)
            {
                throw new Exception("请求路径有误");
            }

            string area = paths[0];
            string controller = paths[paths.Length - 2];
            string action = paths[paths.Length - 1];

            Controller = controller;
            Action = action;

            this.OnInit(context);
        }

        protected virtual void OnInit(HttpContext context)
        {
        }
    }
}
