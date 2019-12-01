using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities;
using SK.Handler;

namespace SK.Admin.Controllers
{
    public class Company : BasePage
    {
        public void list()
        {
            CompanyDataContext companyCxt = new CompanyDataContext();
            var list = companyCxt.Company.Where(p => !p.IsPass);
            var data = list.OrderByDescending(p => p.UpdateAt).Select(a => a).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void tasklist()
        {
            CompanyTaskDataContext companyCxt = new CompanyTaskDataContext();
            var list = companyCxt.CompanyTask.Where(p => !p.IsPass);
            var data = list.OrderByDescending(p => p.UpdateAt).Select(a => a).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void check()
        {
            CompanyTaskDataContext companyCxt = new CompanyTaskDataContext();
            CompanyDataContext cxt = new CompanyDataContext();

            var id = Request["ID"];

            var entity = companyCxt.CompanyTask.Where(p => p.ID == id).FirstOrDefault();
            if (entity == null)
            {
                this.FailMessage("信息不存在");
                return;
            }

            var oldCompany = cxt.Company.FirstOrDefault(p => p.UserID == entity.UserID);
            var isNew = false;
            if (oldCompany == null) {

                isNew = true;
                oldCompany = new Entities.Company();

                oldCompany.ID = Guid.NewGuid().ToString();
                oldCompany.CreateAt = DateTime.Now;
                oldCompany.UpdateAt = entity.CreateAt;
            }

            oldCompany.IsPass = true;
            oldCompany.UserID = entity.UserID;
            oldCompany.Address = entity.Address;
            oldCompany.CompanyName = entity.CompanyName;
            oldCompany.Contact = entity.Contact;
            oldCompany.Tel = entity.Tel;
            oldCompany.Mobile = entity.Mobile;
            oldCompany.Password = entity.Password;
            oldCompany.UpdateAt = DateTime.Now;

            if (isNew) {
                cxt.Company.InsertOnSubmit(oldCompany);
            }

            entity.IsPass = true;
            entity.UpdateAt = DateTime.Now;

            cxt.SubmitChanges();
            companyCxt.SubmitChanges();

           
            this.ShowResult(true, "成功");
        }
    }
}
