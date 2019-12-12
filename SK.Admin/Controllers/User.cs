using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SK.BL;
using SK.Common;
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
            CompanyDataContext cp = new CompanyDataContext();
            var userlist = cxt.WXUser.AsEnumerable();
            var comylist = cp.Company.AsEnumerable();
            var alllist = from a in userlist
                       join b in comylist
                       on a.openid equals b.UserID into c
                       from b in c.DefaultIfEmpty()
                       select new {
                           a.nickname,
                           a.openid,
                           a.ispass,
                           a.province,
                           a.city,
                           a.country,
                           a.createtime,
                           CompanyName = b == null ? "":  b.CompanyName,
                           Address = b == null ? "" : b.Address,
                           Contact = b == null ? "" : b.Contact,
                           Mobile = b == null ? "" : b.Mobile,
                           Tel = b == null ? "" : b.Tel,
                           Pic = b == null ? "" :b.Pic
                       };

            if (!string.IsNullOrEmpty(QF("OpenID"))) alllist = alllist.Where(p => p.openid == QF("OpenID"));
            if (!string.IsNullOrEmpty(QF("StartAt"))) alllist = alllist.Where(p => p.createtime >= QF("StartAt", DateTime.Now.Date));
            if (!string.IsNullOrEmpty(QF("EndAt"))) alllist = alllist.Where(p => p.createtime <= QF("EndAt", DateTime.Now.Date.AddDays(1)).AddDays(1));
            if (!string.IsNullOrEmpty(QF("NickName"))) alllist = alllist.Where(p => p.nickname.Contains(QF("NickName")));

            var data = alllist.OrderByDescending(p => p.createtime).Select(a => a).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void tasklist()
        {
            WXUserDataContext cxt = new WXUserDataContext();
            CompanyDataContext cp = new CompanyDataContext();

            var userlist = cxt.WXUser.AsEnumerable();
            var comylist = cp.Company.AsEnumerable();

            var alllist = from a in userlist
                          join b in comylist
                          on a.openid equals b.UserID into c
                          from b in c.DefaultIfEmpty()
                          select new
                          {
                              a.nickname,
                              a.openid,
                              a.ispass,
                              a.province,
                              a.city,
                              a.country,
                              a.createtime,
                              CompanyName = b == null ? "" : b.CompanyName,
                              Address = b == null ? "" : b.Address,
                              Contact = b == null ? "" : b.Contact,
                              Mobile = b == null ? "" : b.Mobile,
                              Tel = b == null ? "" : b.Tel,
                              Pic = b == null ? "" : b.Pic
                          };

            alllist = alllist.Where(p => !p.ispass);

            var data = alllist.OrderByDescending(p => p.createtime).Select(a => a).ToList();
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

            if (entity.ispass)
            {
                this.FailMessage("该用户已通过审核");
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

            //审核结果通知
            SendMessageForCheckUser(entity);
           
            this.ShowResult(true, "成功");
        }

        /// <summary>
        /// 审核结果通知
        /// </summary>
        /// <param name="user"></param>
        private void SendMessageForCheckUser(Entities.WXUser user)
        {
            string title = string.Format("{0}，感谢您在我们平台注册，审核已通过", user.nickname);

            string tplPath = this.Context.Server.MapPath("/content/templates/用户审核结果通知.json");
            WXTemplateBL.SendMessageForCheckUser(
                user.openid,
                tplPath,
                "",
                title,
                "会员",
                user.nickname,
               "账号已通过审核，可以下单了。");
        }
    }
}
