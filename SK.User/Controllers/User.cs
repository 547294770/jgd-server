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
                string appid = System.Configuration.ConfigurationManager.AppSettings["appid"];
                string host = System.Configuration.ConfigurationManager.AppSettings["WXWebHost"];
                var redirect_uri = context.Server.UrlEncode(host + "/handler/user/oauth/userinfo");

                var obj = new
                {
                    code = "2222",
                    url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect",appid,redirect_uri)
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

            CompanyTaskDataContext cxt = new CompanyTaskDataContext();
            CompanyDataContext cxtCompany = new CompanyDataContext();

            var companyInfo = cxtCompany.Company.FirstOrDefault(p => p.UserID == UserInfo.openid);
            var entity = cxt.CompanyTask.FirstOrDefault(p => p.UserID == UserInfo.openid && !p.IsPass);
            if (entity == null)
            {
                entity = new CompanyTask();
                entity.ID = Guid.NewGuid().ToString();
                entity.CreateAt = DateTime.Now;
                entity.UpdateAt = entity.CreateAt;
            }
            else
            {
                this.FailMessage("公司信息审核中，不能再次修改。");
                return;
            }

            if (string.IsNullOrWhiteSpace(QF("CompanyName"))) { this.FailMessage("公司名称不能为空"); return; }
            if (string.IsNullOrWhiteSpace(QF("Address"))) { this.FailMessage("地址不能为空"); return; }
            if (string.IsNullOrWhiteSpace(QF("Contact"))) { this.FailMessage("联系人不能为空"); return; }
            if (string.IsNullOrWhiteSpace(QF("Tel"))) { this.FailMessage("固定电话不能为空"); return; }
            if (string.IsNullOrWhiteSpace(QF("Mobile"))) { this.FailMessage("联系手机不能为空"); return; }

            entity.IsPass = false;
            entity.UserID = UserInfo.openid;
            entity.Address = QF("Address");
            entity.CompanyName = QF("CompanyName");
            entity.Contact = QF("Contact");
            entity.Tel = QF("Tel");
            entity.Mobile = QF("Mobile");
            entity.Password = "";
            entity.Pic = QF("Pic");

            cxt.CompanyTask.InsertOnSubmit(entity);
            cxt.SubmitChanges();

            if (companyInfo != null)
            {
                companyInfo.IsPass = false;
            }

            cxtCompany.SubmitChanges();

            this.ShowResult(true, "保存成功", entity);
        }

        public void updatepassword()
        {
            if (UserInfo == null) { this.FailMessage("未登录"); return; }

            CompanyDataContext cxt = new CompanyDataContext();

            var entity = cxt.Company.FirstOrDefault(p => p.UserID == UserInfo.openid);
            if (entity == null)
            {
                this.FailMessage("请先完善公司信息"); 
                return;
            }

            if (string.IsNullOrEmpty(entity.Password))
            {
                //新设置
                var password = QF("Password");
                var password2 = QF("Password2");

                if (string.IsNullOrWhiteSpace(password)) { this.FailMessage("密码不能为空"); return; }
                if (password.Length < 6 || password.Length > 20) { this.FailMessage("密码长度为6-20位"); return; }
                if (password != password2) { this.FailMessage("两次密码不一致"); return; }
                

                entity.Password = password;
            }
            else
            {
                var password = QF("Password");
                var password2 = QF("Password2");
                var passwordOld = QF("PasswordOld");

                if (string.IsNullOrWhiteSpace(passwordOld)) { this.FailMessage("密码不能为空"); return; }
                if (passwordOld.Length < 6 || passwordOld.Length > 20) { this.FailMessage("密码长度为6-20位"); return; }
                if (passwordOld != entity.Password) { this.FailMessage("原密码不正确"); return; }

                if (string.IsNullOrWhiteSpace(password)) { this.FailMessage("新密码不能为空"); return; }
                if (password.Length < 6 || password.Length > 20) { this.FailMessage("新密码长度为6-20位"); return; }
                if (password != password2) { this.FailMessage("两次密码不一致"); return; }

                entity.Password = password2;
            }

            cxt.SubmitChanges();

            this.ShowResult(true, "设置成功", entity);
        }
    }
}
