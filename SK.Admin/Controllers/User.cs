using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SK.Common.Caches;
using SK.Entities;
using SK.Handler;

namespace SK.Admin.Controllers
{
    public class User : BasePage
    {
        public void list()
        {
            WXUserDataContext cxt = new WXUserDataContext();
            var list = cxt.WXUser.AsEnumerable();
            var data = list.OrderByDescending(p => p.createtime).Select(a => a).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void tasklist()
        {
            WXUserDataContext cxt = new WXUserDataContext();
            var list = cxt.WXUser.Where(p => !p.ispass);
            var data = list.OrderByDescending(p => p.createtime).Select(a => a).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void check()
        {
            WXUserDataContext cxt = new WXUserDataContext();

            var id = Request["ID"];
            var entity = cxt.WXUser.Where(p => p.openid == id).FirstOrDefault();
            if (entity == null)
            {
                this.FailMessage("信息不存在");
                return;
            }

            entity.ispass = true;
            //UserCache.AddUser(entity.openid, entity);
            var userinfo =  UserCache.GetUser(entity.openid);
            if (userinfo != null) {
                var wxuser = (WXUser)userinfo;
                wxuser.ispass = true;
            }

            var count = HttpRuntime.Cache.Count;
           
            cxt.SubmitChanges();
           
            this.ShowResult(true, "成功");
        }
    }
}
