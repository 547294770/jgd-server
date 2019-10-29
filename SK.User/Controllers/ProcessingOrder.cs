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

namespace SK.User.Controllers
{
    public class ProcessingOrder : BasePage
    {
        public void list()
        {
            //AttachmentDataContext dd = new AttachmentDataContext();
            //dd.Attachment.InsertOnSubmit(
           
            SK.Entities.ProcessingOrder.OrderStatus[] status = new SK.Entities.ProcessingOrder.OrderStatus[] { 
                SK.Entities.ProcessingOrder.OrderStatus.Uploaded,
                SK.Entities.ProcessingOrder.OrderStatus.Print,
                SK.Entities.ProcessingOrder.OrderStatus.NoticePickUp,
                SK.Entities.ProcessingOrder.OrderStatus.AlreadyGoods,
                SK.Entities.ProcessingOrder.OrderStatus.Shipped,
                SK.Entities.ProcessingOrder.OrderStatus.None
            };

            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            var list = dc.ProcessingOrder.Where(p => p.UserID == "4A355901-3556-4B7D-9E54-9FE03C1B99F8");
            list = list.Where(p =>status.Contains(p.Status) || p.Status == Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod && p.DelType == Entities.ProcessingOrder.DeliveryType.Self).OrderByDescending(p => p.UpdateAt);
            
            var data = list.OrderByDescending(p=>p.UpdateAt).Select(p => new
            {
                p.Content,
                p.CreateAt,
                p.ID,
                p.OrderNo,
                Processing = status.Contains(p.Status),
                Status = Enum.GetName(typeof(SK.Entities.ProcessingOrder.OrderStatus), p.Status),
                p.UserID
            }).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void unprocessed()
        {
            //SK.Entities.ProcessingOrder.OrderStatus[] status = new SK.Entities.ProcessingOrder.OrderStatus[] { 
            //    SK.Entities.ProcessingOrder.OrderStatus.Processing,
            //    SK.Entities.ProcessingOrder.OrderStatus.InputDelivery,
            //    SK.Entities.ProcessingOrder.OrderStatus.Warehousing,
            //    SK.Entities.ProcessingOrder.OrderStatus.Producing,
            //    SK.Entities.ProcessingOrder.OrderStatus.Produced,
            //    SK.Entities.ProcessingOrder.OrderStatus.InputPickUpContact,
            //    SK.Entities.ProcessingOrder.OrderStatus.ConfirmationFee,
            //    SK.Entities.ProcessingOrder.OrderStatus.Shipped
            //};

            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
        
            var list = dc.ProcessingOrder.AsQueryable(); //.Where(p => status.Contains(p.Status));
            var data = list.OrderByDescending(p=>p.UpdateAt).Select(p => new
            {
                p.Content,
                p.CreateAt,
                p.ID,
                p.OrderNo,
                Status = Enum.GetName(typeof(SK.Entities.ProcessingOrder.OrderStatus), p.Status),
                p.UserID
            }).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void add()
        {
            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();

            var orderId = QF("OrderID");
            var order = dc.ProcessingOrder.FirstOrDefault(p => p.ID == orderId);
            if (order == null)
            {
                order = new Entities.ProcessingOrder();
            }

            order =  this.Request.Form.Fill<Entities.ProcessingOrder>(order);

            if (string.IsNullOrWhiteSpace(order.Content))
            {
                this.ShowResult(false, "加工需求内容不能为空");
                return;
            }

            

            if (string.IsNullOrWhiteSpace(order.ID))
            {
                order.ID = Guid.NewGuid().ToString();
                order.CreateAt = DateTime.Now;
                order.UpdateAt = order.CreateAt;
                order.Status = Entities.ProcessingOrder.OrderStatus.None;
                order.UserID = "4A355901-3556-4B7D-9E54-9FE03C1B99F8";
                order.UserName = "test2";
                order.OrderNo = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));

                dc.ProcessingOrder.InsertOnSubmit(order);
            }
            else {
                order.UpdateAt = DateTime.Now;
            }

            dc.SubmitChanges();

            this.ShowResult(true, "保存成功");

            //switch (order.Status)
            //{
            //    case SK.Entities.ProcessingOrder.OrderStatus.None:
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.Processing:
            //        DoUploaded(order);
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.Uploaded:
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.Print:
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.InputDelivery:
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.Warehousing:
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.Producing:
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.Produced:
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.NoticeDelivery:
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.InputPickUpContact:
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.ConfirmationFee:
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.Shipped:
            //        break;
            //    case SK.Entities.ProcessingOrder.OrderStatus.Finished:
            //        break;
            //    default:
            //        break;
            //}
        }

        private void DoUploaded(SK.Entities.ProcessingOrder order)
        {
            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            var enitity = dc.ProcessingOrder.FirstOrDefault(p => p.ID == order.ID);
            enitity.Status = Entities.ProcessingOrder.OrderStatus.Uploaded;
            dc.SubmitChanges();

            this.ShowResult(true, "保存成功");
        }

        public void info()
        {
            string orderId = QF("OrderID");

            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            var order = dc.ProcessingOrder.FirstOrDefault(p => p.ID == orderId);

            this.ShowResult(true, "成功",
                new { 
                order.Content,
                CreateAt = order.CreateAt.ToString("yyyy-MM-dd HH:mm:ss"),
                order.ID,
                order.OrderNo,
                Status = Enum.GetName(typeof(SK.Entities.ProcessingOrder.OrderStatus), order.Status),
                DelType = Enum.GetName(typeof(SK.Entities.ProcessingOrder.DeliveryType), order.DelType),
                PickType = Enum.GetName(typeof(SK.Entities.ProcessingOrder.PickUpType), order.PickType),
                order.UpdateAt,
                order.UserID,
                order.UserName
                });
        }

        /// <summary>
        /// 处理订单
        /// </summary>
        public void exeorder()
        {
            string orderId = QF("OrderID");

            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            var order = dc.ProcessingOrder.FirstOrDefault(p => p.ID == orderId);
            
            if (order == null)
            {
                this.FailMessage("订单不存在");
                return;
            }

            switch (order.Status)
            {
                case SK.Entities.ProcessingOrder.OrderStatus.None://
                    order.Status = Entities.ProcessingOrder.OrderStatus.Processing;
                    SendMessageForNewOrder(order);
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.Uploaded://
                    DoAttachment(order);
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.Print://
                    order.DelType = QF("DelType").ToEnum<Entities.ProcessingOrder.DeliveryType>();
                    order.Status = Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod;
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod://
                    DoDelivery(order);
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.NoticePickUp://
                    DoPickup(order);
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.AlreadyGoods://
                    order.Status = Entities.ProcessingOrder.OrderStatus.ConfirmationFee;
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.Shipped://
                    order.Status = Entities.ProcessingOrder.OrderStatus.Finished;
                    break;
                default:
                    break;
            }

            order.UpdateAt = DateTime.Now;
            dc.SubmitChanges();

            this.ShowResult(true, "操作成功");
        }

        private void DoAttachment(Entities.ProcessingOrder order)
        {
            order.Status = Entities.ProcessingOrder.OrderStatus.Print;
            var form = this.Context.Request.Form;
            var attachLength = QF("AttachmentLength", 0);
            if (attachLength > 0)
            {
                AttachmentDataContext attach = new AttachmentDataContext();
                List<Attachment> list = new List<Attachment>();

                for (int i = 0; i < attachLength; i++)
                {
                    var Name = QF(string.Format("Attachment[{0}][Name]", i));
                    var FilePath = QF(string.Format("Attachment[{0}][FilePath]", i));
                    var FileName = QF(string.Format("Attachment[{0}][FileName]", i));
                    var FileSize = QF(string.Format("Attachment[{0}][FileSize]", i), 0);

                    var ent = new Entities.Attachment();
                    ent.ID = Guid.NewGuid().ToString("N");
                    ent.CreateAt = DateTime.Now;
                    ent.FileName = FileName;
                    ent.FilePath = FilePath;
                    ent.FileSize = FileSize;
                    ent.Name = Name;
                    ent.SourceID = order.ID;
                    ent.UpdateAt = DateTime.Now;

                    list.Add(ent);

                }

                var all = attach.Attachment.AsEnumerable();
                attach.Attachment.DeleteAllOnSubmit(all);
                attach.Attachment.InsertAllOnSubmit(list);

                attach.SubmitChanges();
            }
        }

        private void DoDelivery(Entities.ProcessingOrder order)
        {
            DeliveryOrderDataContext dc = new DeliveryOrderDataContext();

            var ent = new Entities.DeliveryOrder();
            ent.Content = QF("Delivery[Content]");
            ent.CreateAt = DateTime.Now;
            ent.DeliveryAt = QF("Delivery[DeliveryAt]", DateTime.Now);
            ent.ID = Guid.NewGuid().ToString();
            ent.OrderNo = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
            ent.ProcessingNo = order.OrderNo;
            ent.SourceID = order.ID;
            ent.UserID =
            ent.UserID = "4A355901-3556-4B7D-9E54-9FE03C1B99F8";
            ent.UserName = "test2";
            ent.VehicleInfo = QF("Delivery[VehicleInfo]");

            switch (order.DelType)
            {
                case SK.Entities.ProcessingOrder.DeliveryType.None:
                    break;
                case SK.Entities.ProcessingOrder.DeliveryType.Self:
                    ent.Type = DeliveryOrder.OrderType.Self;
                    break;
                case SK.Entities.ProcessingOrder.DeliveryType.LXD:
                    ent.Type = DeliveryOrder.OrderType.LXD;
                    break;
                default:
                    break;
            }

            dc.DeliveryOrder.InsertOnSubmit(ent);
            dc.SubmitChanges();

            order.Status = Entities.ProcessingOrder.OrderStatus.InputDelivery;
        }

        private void DoPickup(Entities.ProcessingOrder order)
        {
            PickUpOrderDataContext dc = new PickUpOrderDataContext();

            var ent = new Entities.PickUpOrder();
            ent.Content = QF("PickUp[Content]");
            ent.CreateAt = DateTime.Now;
            ent.PickUpAt = QF("PickUp[PickUpAt]", DateTime.Now);
            ent.ID = Guid.NewGuid().ToString();
            ent.OrderNo = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
            ent.ProcessingNo = order.OrderNo;
            ent.SourceID = order.ID;
            ent.UserID =
            ent.UserID = "4A355901-3556-4B7D-9E54-9FE03C1B99F8";
            ent.UserName = "test2";
            ent.VehicleInfo = QF("PickUp[VehicleInfo]");

            switch (order.DelType)
            {
                case SK.Entities.ProcessingOrder.DeliveryType.None:
                    break;
                case SK.Entities.ProcessingOrder.DeliveryType.Self:
                    ent.Type = PickUpOrder.OrderType.Self;
                    break;
                case SK.Entities.ProcessingOrder.DeliveryType.LXD:
                    ent.Type = PickUpOrder.OrderType.LXD;
                    break;
                default:
                    break;
            }

            dc.PickUpOrder.InsertOnSubmit(ent);
            dc.SubmitChanges();

            order.Status = Entities.ProcessingOrder.OrderStatus.InputPickUpContact;
        }

        public void attachment()
        {
            AttachmentDataContext dc = new AttachmentDataContext();

            string orderID = QF("OrderID");//"10BE8BD8-7EE0-4EC3-9687-AC0CDEF1BCA0";
            if (string.IsNullOrWhiteSpace(orderID))
            {
                this.FailMessage("订单为空");
                return;
            }

            var list = dc.Attachment.Where(p => p.SourceID == orderID).ToList();
            this.ShowResult(true, "成功", list);
        }

        public void fee()
        {
            ProcessingFeeDataContext dc = new ProcessingFeeDataContext();

            string orderID = QF("OrderID");//"10BE8BD8-7EE0-4EC3-9687-AC0CDEF1BCA0";
            if (string.IsNullOrWhiteSpace(orderID))
            {
                this.FailMessage("订单为空");
                return;
            }

            var list = dc.ProcessingFee.Where(p => p.SourceID == orderID).Select(p => new
            {
                TypeName = p.Type.GetDescription(),
                p.ProcessingNo,
                p.Content,
                p.FeeNo,
                p.ID,
                p.CreateAt
            }).ToList();
            this.ShowResult(true, "成功", list);
        }

        private void SendMessageForNewOrder(Entities.ProcessingOrder order)
        { 
            string tplPath = this.Context.Server.MapPath("/content/templates/新订单通知.json");
            WXTemplateBL.SendMessageForNewOrder(tplPath,
                "",
                "新订单通知",
                "test1",
                "加工单",
               order.OrderNo,
               order.Content);
        }
    }
}
