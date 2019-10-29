using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SK.BL;
using SK.Common;
using SK.Common.Caches;
using SK.Entities;

namespace SK.Handler
{
    public class BasePage
    {
        //protected DBContext DBC = SK.Common.DBC.Context;
        
        protected HttpRequest Request { get; set; }
        protected HttpResponse Response { get; set; }
        protected HttpContext Context { get; set; }

        /// <summary>
        /// 获取当前登录的用户信息
        /// </summary>
        protected WXUser UserInfo
        {
            get
            {
                var cookie = Request.Cookies[Consts.USER_INFO];
                if (cookie == null || string.IsNullOrWhiteSpace(cookie.Value))
                {
                    return null;
                }

                var obj =    UserCache.GetUser(cookie.Value);
                if (obj != null)
                {
                    return (WXUser)obj;
                }

                return null;
            }
        }

        /// <summary>
        /// 获取当前登录的管理员信息
        /// </summary>
        protected Admin AdminInfo
        {
            get
            {
                if (Context.Items[Consts.ADMIN_INFO] != null) {
                    return (Admin)Context.Items[Consts.ADMIN_INFO];
                }

                return null;
            }
        }

        public BasePage()
        {
            Request = HttpContext.Current.Request;
            Response = HttpContext.Current.Response;
            Context = HttpContext.Current;

            Response.ContentType = "application/json";
        }

        protected string QF(string name)
        {
            return Request.Form[name];
        }

        protected T QF<T>(string name, T defaultValue)
        {
            string val = Request.Form[name];
            return (T)Convert.ChangeType(val, typeof(T));
        }

        protected void ShowResult(bool success, string msg)
        {
            this.ShowResult(success, msg, null);
        }

        protected bool FailMessage(string msg)
        {
            this.ShowResult(false, msg, null);
            return false;
        }

        protected void ShowResult(bool success, string msg, object info)
        {
            var returnObj = new
            {
                code = success ? 0 : 1,
                msg = msg,
                count = 1,
                data = info
            };
            string json = JsonConvert.SerializeObject(returnObj);
           
            Response.Write(json);
            Response.End();
        }

        protected bool Login(string openid)
        {
            WXUserDataContext cxt = new WXUserDataContext();
            var userInfo = cxt.WXUser.FirstOrDefault(p => p.openid == openid);
            //var userInfo = UserBL.Instance.Login(userName, passWord);
            if (userInfo != null)
            {
                var cookie = new System.Web.HttpCookie(Consts.USER_INFO);
                cookie.Value = userInfo.openid; //UserBL.Instance.GetUserToken(userInfo.openid);
                UserCache.AddUser(cookie.Value, userInfo);
                Response.Cookies.Add(cookie);

                return true;
            }

            return false;
        }

        protected void Logout()
        {
            if (UserInfo != null)
            {
                HttpCookie cookie = new HttpCookie(Consts.USER_INFO);
                cookie.Expires = DateTime.Now.AddDays(-1);

                UserCache.RemoveUser(cookie.Value);

                Response.Cookies.Add(cookie);
            }

            this.ShowResult(true, "退出成功");
        }
    }
}
