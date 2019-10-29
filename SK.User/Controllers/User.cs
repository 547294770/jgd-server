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

namespace SK.User.Controllers
{
    public class User : BasePage
    {
        public void info()
        {
            this.ShowResult(true, "ok", UserInfo);

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

        public void logout()
        {
            Logout();
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
