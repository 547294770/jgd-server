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

namespace SK.Admin.Controllers
{
    public class Admin : BasePage
    {
        public void init()
        {
            if (AdminInfo == null)
            {
                this.ShowResult(false, "未登陆", null);
                return;
            }

            this.ShowResult(true, "成功", AdminInfo);
        }

        public void info()
        {
            this.ShowResult(true, "获取信息",AdminInfo);
            return;
        }

        public void login()
        {
            string userName = Request["UserName"];
            string passWord = Request["PassWord"];

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(passWord))
            {
                this.ShowResult(false, "账号或密码不能为空");
                return;
            }

            var result = AdminLogin(userName, passWord);
            if (result)
            {
                this.ShowResult(true, "登录成功");
                return;
            }

            this.ShowResult(false, "账号或密码错误");
        }

        public void create1()
        {
            if (AdminInfo == null)
            {
                this.ShowResult(false, "未登陆");
                return;
            }

            string userName = Request["UserName"];
            string passWord = Request["PassWord"];

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(passWord))
            {
                this.ShowResult(false, "账号或密码不能为空");
                return;
            }

            AdminDataContext cxt = new AdminDataContext();

            var entity = new Entities.Admin();
            entity.ID = Guid.NewGuid().ToString();
            entity.CreateAt = DateTime.Now;
            entity.Name = userName;
            entity.NickName = userName;
            entity.PassWord = SK.Common.Security.MD5Encrypt(passWord,true);

            cxt.Admin.InsertOnSubmit(entity);
            cxt.SubmitChanges();

            this.ShowResult(true, "创建成功");
        }

        public void logout()
        {
            AdminLogout();
        }

        //public void add()
        //{
        //    var userInfo = UserInfo;
        //    var usrName = "";
        //    if (userInfo != null) {
        //        usrName = userInfo.UserName;
        //    }
            
        //    this.ShowResult(false, usrName);
        //}
    }
}
