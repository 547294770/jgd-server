using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SK.Common;
using SK.Common.Caches;
using SK.Entities;

namespace SK.Handler
{
    public class UserHandler : BaseHandler
    {
        protected override void OnInit(HttpContext context)
        {
            base.OnInit(context);
            try
            {
                OnBeforeInit(context);

                Assembly ass = Assembly.Load("SK.User");
                var type = ass.GetType("SK.User.Controllers." + this.Controller, true, true);
                var obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(this.Action, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

                method.Invoke(obj, null);
            }
            catch (TypeLoadException ex)
            {
                throw new Exception("请求路径不存在!");
            }
        }

        private void OnBeforeInit(HttpContext context)
        {
            var cookie = context.Request.Cookies[Consts.USER_INFO];
            if (cookie != null)
            {
                string token = cookie.Value;
                if (!string.IsNullOrEmpty(token))
                {
                    object obj = UserCache.GetUser(token);
                    if (obj == null)
                    {
                        WXUserDataContext cxt = new WXUserDataContext();
                        var userInfo = cxt.WXUser.FirstOrDefault(p => p.openid == token);
                        if (userInfo != null)
                        {
                            UserCache.AddUser(token, userInfo);
                            context.Items[Consts.USER_INFO] = userInfo;
                        }
                    }
                    else
                    {
                        UserCache.AddUser(token, obj);
                        context.Items[Consts.USER_INFO] = obj;
                    }
                }
            }
            else
            {
                context.Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxcee2bf962b1ef8f3&redirect_uri=https%3A%2F%2Ftest.alry.cn%2Fhandler%2Fuser%2Foauth%2Fuserinfo&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect");
            }
        }
    }
}
