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

namespace SK.Admin.Controllers
{
    public class ProcessingOrder : BasePage
    {
        public void list()
        {
            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            var list = dc.ProcessingOrder.Where(p => p.Status != Entities.ProcessingOrder.OrderStatus.None);

            if (!string.IsNullOrEmpty(QF("OrderNo"))) list = list.Where(p => p.OrderNo == QF("OrderNo"));
            if (!string.IsNullOrEmpty(QF("StartAt"))) list = list.Where(p => p.CreateAt >= QF("StartAt", DateTime.Now.Date));
            if (!string.IsNullOrEmpty(QF("EndAt"))) list = list.Where(p => p.CreateAt <= QF("EndAt", DateTime.Now.Date.AddDays(1)).AddDays(1));
            if (!string.IsNullOrEmpty(QF("Status"))) list = list.Where(p => p.Status == QF("Status").ToEnum<SK.Entities.ProcessingOrder.OrderStatus>());

            var data = list.OrderByDescending(p=>p.UpdateAt).Select(p => new
            {
                p.Content,
                p.CreateAt,
                p.ID,
                p.OrderNo,
                DelType = Enum.GetName(typeof(Entities.ProcessingOrder.DeliveryType), p.DelType),
                PickType = Enum.GetName(typeof( Entities.ProcessingOrder.PickUpType), p.PickType),
                Status = Enum.GetName(typeof(SK.Entities.ProcessingOrder.OrderStatus), p.Status),
                p.UserID
            }).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void unprocessed()
        {
            SK.Entities.ProcessingOrder.OrderStatus[] status = new SK.Entities.ProcessingOrder.OrderStatus[] { 
                SK.Entities.ProcessingOrder.OrderStatus.Processing,
                SK.Entities.ProcessingOrder.OrderStatus.InputDelivery,
                SK.Entities.ProcessingOrder.OrderStatus.Warehousing,
                SK.Entities.ProcessingOrder.OrderStatus.Producing,
                SK.Entities.ProcessingOrder.OrderStatus.Produced,
                SK.Entities.ProcessingOrder.OrderStatus.InputPickUpContact,
                SK.Entities.ProcessingOrder.OrderStatus.ConfirmationFee
            };

            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();

            var list = dc.ProcessingOrder.Where(p => status.Contains(p.Status) || (p.Status == Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod && p.DelType == Entities.ProcessingOrder.DeliveryType.LXD));
            var data = list.OrderByDescending(p=>p.UpdateAt).Select(p => new
            {
                p.Content,
                p.CreateAt,
                p.ID,
                p.OrderNo,
                DelType = Enum.GetName(typeof(Entities.ProcessingOrder.DeliveryType), p.DelType),
                PickType = Enum.GetName(typeof(Entities.ProcessingOrder.PickUpType), p.PickType),
                Status = Enum.GetName(typeof(SK.Entities.ProcessingOrder.OrderStatus), p.Status),
                p.UserID
            }).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void save()
        {
            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
                 
            var orderId = QF("OrderID");
            var order = dc.ProcessingOrder.FirstOrDefault(p => p.ID == orderId);
            if (order == null)
            {
                this.FailMessage("订单不存在");
                return;
            }

            //更新时间
            order.UpdateAt = DateTime.Now;

            switch (order.Status)
            {
                case SK.Entities.ProcessingOrder.OrderStatus.None:
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.Processing:
                    DoUploaded(order, dc);
                    break;
                //case SK.Entities.ProcessingOrder.OrderStatus.Uploaded:
                //    break;
                //case SK.Entities.ProcessingOrder.OrderStatus.Print:
                //    break;
                case SK.Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod:
                    {
                        if (order.DelType == Entities.ProcessingOrder.DeliveryType.LXD) {
                            DoInputDelivery(order, dc);//确认录入提货资料（利迅达方录入资料）
                        }
                    }
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.InputDelivery:
                    DoWarehousing(order,dc);//确认材料已入库
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.Warehousing:
                    DoProducing(order,dc);//已安排生产
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.Producing:
                    DoProduced(order,dc);//生产完已入库
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.Produced:
                    DoNoticeDelivery(order,dc);//已通知客户提货
                    break;
                //case SK.Entities.ProcessingOrder.OrderStatus.NoticeDelivery:
                //    break;
                case SK.Entities.ProcessingOrder.OrderStatus.InputPickUpContact:
                    DoAlreadyGoods(order,dc);//确认已备货
                    break;
                //case SK.Entities.ProcessingOrder.OrderStatus.AlreadyGoods:
                //    break;
                case SK.Entities.ProcessingOrder.OrderStatus.ConfirmationFee:
                    DoShipped(order,dc);//确认已发货
                    break;
                //case SK.Entities.ProcessingOrder.OrderStatus.Shipped:
                //    break;
                //case SK.Entities.ProcessingOrder.OrderStatus.Finished:
                //    break;
                default:
                    break;
            }
        }

        private void DoUploaded(SK.Entities.ProcessingOrder order, ProcessingOrderDataContext dc)
        {

            order.Status = Entities.ProcessingOrder.OrderStatus.Uploaded;
            dc.SubmitChanges();

            this.ShowResult(true, "保存成功");
        }

        /// <summary>
        /// 确认录入提货资料
        /// </summary>
        /// <param name="order"></param>
        private void DoInputDelivery(SK.Entities.ProcessingOrder order,ProcessingOrderDataContext dc)
        {
            if (order.Status == Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod && order.DelType == Entities.ProcessingOrder.DeliveryType.LXD)
            {
                var ent = new Entities.DeliveryOrder();
                ent.Content = QF("Delivery[Content]");
                ent.CreateAt = DateTime.Now;
                ent.DeliveryAt = QF("Delivery[DeliveryAt]", DateTime.Now);
                ent.ID = Guid.NewGuid().ToString();
                ent.OrderNo = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
                ent.ProcessingNo = order.OrderNo;
                ent.SourceID = order.ID;
                ent.UserID = "4A355901-3556-4B7D-9E54-9FE03C1B99F8";
                ent.UserName = "test2";
                ent.VehicleInfo = QF("Delivery[VehicleInfo]");

                switch (order.DelType)
                {
                    case SK.Entities.ProcessingOrder.DeliveryType.None:
                        break;
                    case SK.Entities.ProcessingOrder.DeliveryType.Self:
                        ent.Type = Entities.DeliveryOrder.OrderType.Self;
                        break;
                    case SK.Entities.ProcessingOrder.DeliveryType.LXD:
                        ent.Type = Entities.DeliveryOrder.OrderType.LXD;
                        break;
                    default:
                        break;
                }

                DeliveryOrderDataContext dcDeliveryOrder = new DeliveryOrderDataContext();
                dcDeliveryOrder.DeliveryOrder.InsertOnSubmit(ent);
                dcDeliveryOrder.SubmitChanges();

                order.Status = Entities.ProcessingOrder.OrderStatus.InputDelivery;

                this.ShowResult(true, "保存成功");
            }
            else
            {
                this.FailMessage("状态错误");
            }
        }

        /// <summary>
        /// 确认材料已入库
        /// </summary>
        /// <param name="order"></param>
        private void DoWarehousing(SK.Entities.ProcessingOrder order, ProcessingOrderDataContext dc)
        {
            if (order.Status != Entities.ProcessingOrder.OrderStatus.InputDelivery)
            {
                this.FailMessage("状态错误");
                return;
            }
            order.Status = Entities.ProcessingOrder.OrderStatus.Warehousing;
            dc.SubmitChanges();

            this.ShowResult(true, "保存成功");
        }
        /// <summary>
        /// 已安排生产
        /// </summary>
        /// <param name="order"></param>
        private void DoProducing(SK.Entities.ProcessingOrder order, ProcessingOrderDataContext dc)
        {
            if (order.Status != Entities.ProcessingOrder.OrderStatus.Warehousing)
            {
                this.FailMessage("状态错误");
                return;
            }

            order.Status = Entities.ProcessingOrder.OrderStatus.Producing;
            dc.SubmitChanges();

            this.ShowResult(true, "保存成功");
        }

        /// <summary>
        /// 生产完已入库
        /// </summary>
        /// <param name="order"></param>
        private void DoProduced(SK.Entities.ProcessingOrder order, ProcessingOrderDataContext dc)
        {
            if (order.Status != Entities.ProcessingOrder.OrderStatus.Producing)
            {
                this.FailMessage("状态错误");
                return;
            }

            order.Status = Entities.ProcessingOrder.OrderStatus.Produced;
            dc.SubmitChanges();

            this.ShowResult(true, "保存成功");
        }

        /// <summary>
        /// 已通知客户提货
        /// </summary>
        /// <param name="order"></param>
        private void DoNoticeDelivery(SK.Entities.ProcessingOrder order, ProcessingOrderDataContext dc)
        {
            if (order.Status != Entities.ProcessingOrder.OrderStatus.Produced)
            {
                this.FailMessage("状态错误");
                return;
            }

            order.Status = Entities.ProcessingOrder.OrderStatus.NoticePickUp;
            dc.SubmitChanges();

            //通知提货
            this.ShowResult(true, "保存成功");
        }

        /// <summary>
        /// 已备货
        /// </summary>
        /// <param name="order"></param>
        private void DoAlreadyGoods(SK.Entities.ProcessingOrder order, ProcessingOrderDataContext dc)
        {
            if (order.Status != Entities.ProcessingOrder.OrderStatus.InputPickUpContact)
            {
                this.FailMessage("状态错误");
                return;
            }

            var entt = new Entities.ProcessingFee();
            entt.Content = QF("Fee[Content]");
            entt.CreateAt = DateTime.Now;
            entt.FeeNo = DateTime.Now.ToString("yyyyMMddHHmmsss");
            entt.ID = Guid.NewGuid().ToString();
            entt.ProcessingNo = order.OrderNo;
            entt.SourceID = order.ID;
            entt.Type = QF("Fee[Type]").ToEnum<Entities.ProcessingFee.BillType>();

            ProcessingFeeDataContext dcProcessingFee = new ProcessingFeeDataContext();
            dcProcessingFee.ProcessingFee.InsertOnSubmit(entt);
            dcProcessingFee.SubmitChanges();

            order.Status = Entities.ProcessingOrder.OrderStatus.AlreadyGoods;
            dc.SubmitChanges();

            this.ShowResult(true, "保存成功");
        }

        /// <summary>
        /// 确认已发货
        /// </summary>
        /// <param name="order"></param>
        private void DoShipped(SK.Entities.ProcessingOrder order, ProcessingOrderDataContext dc)
        {
            if (order.Status != Entities.ProcessingOrder.OrderStatus.ConfirmationFee)
            {
                this.FailMessage("状态错误");
                return;
            }

            order.Status = Entities.ProcessingOrder.OrderStatus.Shipped;
            dc.SubmitChanges();

            this.ShowResult(true, "保存成功");
        }

        
        public void attachment()
        {
            if (string.IsNullOrWhiteSpace(QF("OrderID")))
            {
                this.FailMessage("订单为空");
                return;
            }


            AttachmentDataContext dcAttachment = new AttachmentDataContext();
        }


        private void SendMessageForPickUp(Entities.ProcessingOrder order)
        {
            string tplPath = this.Context.Server.MapPath("/content/templates/提货通知.json");
            WXTemplateBL.SendMessageForDelivery(tplPath,
                "",
                "您有一个提货信息",
                "20191028001",
                "邱先生",
               "13987654321",
               "粤B.394900",
               "20191029",
               "提货测试");
        }
    }
}
