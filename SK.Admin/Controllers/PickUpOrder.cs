using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SK.Handler;
using SK.Common.Extentions;
using SK.Entities;

namespace SK.Admin.Controllers
{
    public class PickUpOrder : BasePage
    {
        public void list()
        {
            PickUpOrderDataContext dc = new PickUpOrderDataContext();

            var list = dc.PickUpOrder.AsQueryable();

            if (!string.IsNullOrEmpty(QF("OrderNo"))) list = list.Where(p => p.OrderNo == QF("OrderNo"));
            if (!string.IsNullOrEmpty(QF("ProcessingNo"))) list = list.Where(p => p.ProcessingNo == QF("ProcessingNo"));
            if (!string.IsNullOrEmpty(QF("StartAt"))) list = list.Where(p => p.PickUpAt >= QF("StartAt", DateTime.Now.Date));
            if (!string.IsNullOrEmpty(QF("EndAt"))) list = list.Where(p => p.PickUpAt <= QF("EndAt", DateTime.Now.Date.AddDays(1)).AddDays(1));
            if (!string.IsNullOrEmpty(QF("Type"))) list = list.Where(p => p.Type == QF("Type").ToEnum<SK.Entities.PickUpOrder.OrderType>());

            var data = list.Select(a => new
            {
                a.ID,
                a.SourceID,
                a.OrderNo,
                a.ProcessingNo,
                a.VehicleInfo,
                a.Content,
                a.PickUpAt,
                a.CreateAt,
                TypeName = a.Type.GetDescription(),
                //TypeName = Enum.GetName(typeof(SK.Entities.DeliveryOrder.OrderType), a.Type),
                a.UserID
            }).ToList();

            this.ShowResult(true, "成功", data);
        }
    }
}
