using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SK.Common;

namespace SK.Handler
{
    public class AdminHandler : BaseHandler
    {
        protected override void OnInit(HttpContext context)
        {
            base.OnInit(context);
            try
            {
                Assembly ass = Assembly.Load("SK.Admin");
                var type = ass.GetType("SK.Admin.Controllers." + this.Controller, true, true);
                var obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(this.Action, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

                method.Invoke(obj, null);

                
            }
            catch (TypeLoadException ex)
            {
                throw new Exception("请求路径不存在!");
            }
        }
    }
}
