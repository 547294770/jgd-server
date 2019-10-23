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
                TypeName = Enum.GetName(typeof(SK.Entities.DeliveryOrder.OrderType), a.Type),
                a.UserID
            }).ToList();

            this.ShowResult(true, "成功", data);
        }

        public void info()
        {
            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            DeliveryOrderDataContext dcDeliveryOrder = new DeliveryOrderDataContext();

            var orderId = Request["OrderID"];
            var order = dc.ProcessingOrder.FirstOrDefault(p => p.ID == orderId);
            if (order == null)
            {
                this.FailMessage("订单不存在");
                return;
            }

            var entity = dcDeliveryOrder.DeliveryOrder.FirstOrDefault(p => p.SourceID == orderId);
            if (entity != null) {
                this.ShowResult(true, "成功", entity);
                return;
            }
            this.ShowResult(true, "成功", entity);
        }

        public void save()
        {
            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            DeliveryOrderDataContext dcDeliveryOrder = new DeliveryOrderDataContext();

            var orderId = QF("OrderID");
            var order = dc.ProcessingOrder.FirstOrDefault(p => p.ID == orderId);
            if (order == null)
            {
                this.FailMessage("订单不存在");
                return;
            }

            var entity = dcDeliveryOrder.DeliveryOrder.FirstOrDefault(p => p.SourceID == orderId);
            if (entity != null)
            {
                //this.FailMessage("车辆信息已录入");
                //return;
                entity = Request.Form.Fill(entity);
            }
            else {
                entity = new Entities.DeliveryOrder();
                entity = Request.Form.Fill(entity);

                entity.ID = Guid.NewGuid().ToString();
                entity.CreateAt = DateTime.Now;
                entity.OrderNo = string.Format("yyyyMMddHHmmsss");
                entity.ProcessingNo = order.OrderNo;
                entity.SourceID = order.ID;
                entity.UserID = "";
                entity.UserName = "";

                dcDeliveryOrder.DeliveryOrder.InsertOnSubmit(entity);
            }

            dcDeliveryOrder.SubmitChanges();

            this.ShowResult(true, "成功");
        }
    }
}
