using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                string[] hosts = new string[] { "localhost", "127.0.0.1"};
                if (hosts.Contains(Request.Url.Host)) {
                    var debugUser = new WXUser();
                    debugUser.openid = "oNJEyuF1_rkeK9RWOpOu8pmIxRPw";
                    debugUser.nickname = "小秋";
                    debugUser.headimgurl = "http://thirdwx.qlogo.cn/mmopen/vi_32/02NyUgGH75DOvlUIaR7N74Q0MlqzFzgfbMq1FqwDyVYBl5at0Zc68jPafUibrrzywpuKGQ2qAALZvRLr4qaogPg/132";
                    return debugUser;
                }

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
                var cookie = Request.Cookies[Consts.ADMIN_INFO];
                if (cookie == null || string.IsNullOrWhiteSpace(cookie.Value))
                {
                    return null;
                }

                var obj = AdminCache.GetAdmin(cookie.Value);
                if (obj != null)
                {
                    return (Admin)obj;
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

        protected bool AdminLogin(string userName, string passWord)
        {
            AdminDataContext cxt = new AdminDataContext();

            passWord = SK.Common.Security.MD5Encrypt(passWord, true);
            var admin =  cxt.Admin.Where(p => p.Name == userName && p.PassWord == passWord).FirstOrDefault();
            if (admin != null)
            {
                var cookie = new System.Web.HttpCookie(Consts.ADMIN_INFO);
                cookie.Value = admin.ID; //UserBL.Instance.GetUserToken(userInfo.openid);
                cookie.HttpOnly = false;
                AdminCache.AddAdmin(cookie.Value, admin);
                Response.Cookies.Add(cookie);
                return true;
            }

            return false;
        }

        protected void AdminLogout()
        {
            if (AdminInfo != null)
            {
                HttpCookie cookie = new HttpCookie(Consts.ADMIN_INFO);
                cookie.Expires = DateTime.Now.AddDays(-1);

                AdminCache.RemoveAdmin(cookie.Value);
                Response.Cookies.Add(cookie);
            }

            this.ShowResult(true, "退出成功");
        }
    }
}
