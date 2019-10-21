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
using System.Data.Linq.Mapping;

namespace SK.User.Controllers
{
    public class UserShop : BasePage
    {
        public void info()
        {

            UserProduct product = new UserProduct();

            var desc = product.PlatformType.GetDescription();

            var returnObj = new
            {
                code = 0,
                msg = "成功",
                desc = desc,
                count = 1,
                data = UserInfo
            };

            string json = JsonConvert.SerializeObject(returnObj);

            this.Response.Write(json);
        }

        public void login()
        {
            string userName = QF("UserName");
            string passWord = QF("PassWord");

            var result = Login(userName, passWord);
            if (result)
            {
                this.ShowResult(true, "登录成功");
                return;
            }

            this.ShowResult(false, "登录失败");
        }

        public void logout()
        {
            Logout();
        }

        public void add()
        {
            Entities.UserShop model = new Entities.UserShop();
            model.CreateAt = DateTime.Now;
            model.PlatformType = PlatformType.Alibaba;
            model.ShopName = QF("ShopName");
            model.Status = true;
            model.UserID = 3;// UserInfo.ID;
            model.WangWangAccount = string.Empty;

            model.Insert();

            this.ShowResult(true, "添加成功");

        }
    }
}
