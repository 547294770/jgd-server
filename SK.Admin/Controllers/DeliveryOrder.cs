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
    public class DeliveryOrder : BasePage
    {
        public void list()
        {
            DeliveryOrderDataContext dc = new DeliveryOrderDataContext();

            var list = dc.DeliveryOrder.AsQueryable();

            if (!string.IsNullOrEmpty(QF("OrderNo"))) list = list.Where(p => p.OrderNo == QF("OrderNo"));
            if (!string.IsNullOrEmpty(QF("ProcessingNo"))) list = list.Where(p => p.ProcessingNo == QF("ProcessingNo"));
            if (!string.IsNullOrEmpty(QF("StartAt"))) list = list.Where(p => p.DeliveryAt >= QF("StartAt", DateTime.Now.Date));
            if (!string.IsNullOrEmpty(QF("EndAt"))) list = list.Where(p => p.DeliveryAt <= QF("EndAt", DateTime.Now.Date.AddDays(1)).AddDays(1));
            if (!string.IsNullOrEmpty(QF("Type"))) list = list.Where(p => p.Type == QF("Type").ToEnum<SK.Entities.DeliveryOrder.OrderType>());

            var data = list.Select(a => new
            {
                a.ID,
                a.SourceID,
                a.OrderNo,
                a.ProcessingNo,
                a.VehicleInfo,
                a.Content,
                a.DeliveryAt,
                a.CreateAt,
                a.TimeSection,
                a.Time1,
                a.Time2,
                TypeName = Enum.GetName(typeof(SK.Entities.DeliveryOrder.OrderType), a.Type),
                a.UserID
            }).ToList();

            this.ShowResult(true, "成功", data);
        }

        public void list2()
        {
            DeliveryOrderDataContext dc = new DeliveryOrderDataContext();
            var list = dc.DeliveryOrder.Where(p=>p.SourceID == QF("OrderID"));

            var data = list.Select(a => new
            {
                a.ID,
                a.SourceID,
                a.OrderNo,
                a.ProcessingNo,
                a.VehicleInfo,
                a.Content,
                a.DeliveryAt,
                a.CreateAt,
                TypeName = Enum.GetName(typeof(SK.Entities.DeliveryOrder.OrderType), a.Type),
                a.UserID
            }).ToList();

            this.ShowResult(true, "成功", data);
        }

        public void info()
        {
            DeliveryOrderDataContext cxt = new DeliveryOrderDataContext();
            var entity = cxt.DeliveryOrder.FirstOrDefault(p => p.ID == Request["ID"]);
            this.ShowResult(true, "成功", new {
                entity.ID,
                entity.Content,
                entity.CreateAt,
                DeliveryAt = entity.DeliveryAt.ToString("yyyy-MM-dd"),
                entity.TimeSection,
                entity.OrderNo,
                entity.ProcessingNo,
                entity.SourceID,
                entity.Type,
                entity.Time1,
                entity.Time2,
                TypeName = entity.Type.GetDescription(),// Enum.GetName(typeof(SK.Entities.DeliveryOrder.OrderType), entity.Type),
                entity.VehicleInfo
            });
        }

        public void save()
        {
            DeliveryOrderDataContext cxt = new DeliveryOrderDataContext();

            var entity = cxt.DeliveryOrder.FirstOrDefault(p => p.ID == QF("ID"));
            if (entity != null)
            {
                entity = Request.Form.Fill(entity);
            }
            else {
                entity = new Entities.DeliveryOrder();
                entity = Request.Form.Fill(entity);

                entity.ID = Guid.NewGuid().ToString();
                entity.CreateAt = DateTime.Now;
                entity.OrderNo = string.Format("yyyyMMddHHmmsss");
                //entity.ProcessingNo = order.OrderNo;
                //entity.SourceID = order.ID;
                entity.UserID = "";
                entity.UserName = "";

                cxt.DeliveryOrder.InsertOnSubmit(entity);
            }

            cxt.SubmitChanges();

            this.ShowResult(true, "成功");
        }
    }
}
