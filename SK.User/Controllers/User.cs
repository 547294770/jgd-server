using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SK.Handler;
using SK.Common;
using SK.BL;
using System.Web;
using SK.Common.Extentions;
using SK.Entities.Enums;
using System.ComponentModel;
using SK.Entities;
using SK.Common.Caches;

namespace SK.User.Controllers
{
    public class User : BasePage
    {
        public void init()
        {
            OnBeforeInit();
        }

        private void OnBeforeInit()
        {
            

            HttpContext context = this.Context;
            context.Response.ContentType = "application/json";

            string[] hosts = new string[] { "localhost", "127.0.0.1" };
            if (hosts.Contains(context.Request.Url.Host)) return;

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
                            context.Response.Write(JsonConvert.SerializeObject(userInfo));
                            context.Response.End();
                        }
                    }
                    else
                    {
                        UserCache.AddUser(token, obj);
                        context.Items[Consts.USER_INFO] = obj;
                        context.Response.Write(JsonConvert.SerializeObject(obj));
                        context.Response.End();
                    }
                }
            }
            else
            {
                var obj = new
                {
                    code = "2222",
                    url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxcee2bf962b1ef8f3&redirect_uri=https%3A%2F%2Ftest.alry.cn%2Fhandler%2Fuser%2Foauth%2Fuserinfo&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect"
                };


                context.Response.Write(JsonConvert.SerializeObject(obj));
                context.Response.End();
                //context.Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxcee2bf962b1ef8f3&redirect_uri=https%3A%2F%2Ftest.alry.cn%2Fhandler%2Fuser%2Foauth%2Fuserinfo&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect");
            }
        }

        public void info()
        {
            if (UserInfo != null)
            {
                this.ShowResult(true, "成功", UserInfo);
            }
            else
            {
                this.ShowResult(false, "失败", null);
            }

            //UserProduct product = new UserProduct();


            //var desc = product.PlatformType.GetDescription();

            //var returnObj = new
            //{
            //    code = 0,
            //    msg = "成功",
            //    desc = desc,
            //    count = 1,
            //    data = UserInfo
            //};

            //string json = JsonConvert.SerializeObject(returnObj);

            //this.Response.Write(json);
        }



        //public void login()
        //{
        //    string userName = QF("UserName");
        //    string passWord = QF("PassWord");

        //    var result = Login(userName, passWord);
        //    if (result)
        //    {
        //        this.ShowResult(true, "登录成功");
        //        return;
        //    }

        //    this.ShowResult(false, "登录失败");
        //}


        //public void add()
        //{
        //    var userInfo = UserInfo;
        //    var usrName = "";
        //    if (userInfo != null) {
        //        usrName = userInfo.UserName;
        //    }
            
        //    this.ShowResult(false, usrName);

            
        //}

        public void companyinfo()
        {
            if (UserInfo == null) { this.FailMessage("未登录"); return; }

            CompanyDataContext cxt = new CompanyDataContext();

            var entity = cxt.Company.FirstOrDefault(p => p.UserID == UserInfo.openid);
            this.ShowResult(true, "成功", entity);
        }

        public void updatecompany()
        {
            if (UserInfo == null) { this.FailMessage("未登录"); return; }

            if (string.IsNullOrWhiteSpace(QF("CompanyName"))) { this.FailMessage("公司名称不能为空"); return; }
            if (string.IsNullOrWhiteSpace(QF("Address"))) { this.FailMessage("地址不能为空"); return; }
            if (string.IsNullOrWhiteSpace(QF("Contact"))) { this.FailMessage("联系人不能为空"); return; }
            if (string.IsNullOrWhiteSpace(QF("Tel"))) { this.FailMessage("电话不能为空"); return; }

            CompanyDataContext cxt = new CompanyDataContext();

            var entity = cxt.Company.FirstOrDefault(p => p.UserID == UserInfo.openid);
            if (entity == null)
            {
                entity = new Company();
                entity.ID = Guid.NewGuid().ToString();

                entity.UserID = UserInfo.openid;
                entity.Address = QF("Address");
                entity.CompanyName = QF("CompanyName");
                entity.Contact = QF("Contact");
                entity.Tel = QF("Tel");

                cxt.Company.InsertOnSubmit(entity);
            }
            else {
                entity.UserID = UserInfo.openid;
                entity.Address = QF("Address");
                entity.CompanyName = QF("CompanyName");
                entity.Contact = QF("Contact");
                entity.Tel = QF("Tel");
            }

            cxt.SubmitChanges();

            this.ShowResult(true, "保存成功", entity);
        }
    }
}
