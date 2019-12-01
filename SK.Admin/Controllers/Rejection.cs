using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SK.Handler;
using SK.Common.Extentions;
using Newtonsoft.Json;
using SK.Entities;
using SK.BL;
using SK.Common;
using System.Configuration;

namespace SK.Admin.Controllers
{
    public class Rejection : BasePage
    {
        public void list()
        {
            RejectionDataContext dc = new RejectionDataContext();

            var orderId = Request["OrderID"];
            var list = dc.Rejection.Where(p => p.SourceID == orderId);

            var data = list.OrderByDescending(p => p.CreateAt).Select(p =>
                 new
                 {
                     p.Reason,
                     p.CreateAt,
                     p.ID,
                     p.SourceID,
                     p.UserID,
                     p.UserName
                 }
            ).ToList();

            this.ShowResult(true, "成功", new
            {
                data = data
            });
        }
    }
}
