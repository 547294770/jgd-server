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
                p.Pic,
                p.CreateAt,
                p.ID,
                p.OrderNo,
                DelType = Enum.GetName(typeof(Entities.ProcessingOrder.DeliveryType), p.DelType),
                PickType = Enum.GetName(typeof( Entities.ProcessingOrder.PickUpType), p.PickType),
                Status = Enum.GetName(typeof(SK.Entities.ProcessingOrder.OrderStatus), p.Status),
                p.UserID,
                p.UserName,
                p.IsReject,
                p.StatusID
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

            var list = dc.ProcessingOrder.Where(p => status.Contains(p.Status) 
                || (p.Status == Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod && p.DelType == Entities.ProcessingOrder.DeliveryType.LXD)
                || (p.Status == Entities.ProcessingOrder.OrderStatus.ConfirmPickUpMethod && p.PickType == Entities.ProcessingOrder.PickUpType.LXD));
            var data = list.OrderByDescending(p=>p.UpdateAt).Select(p => new
            {
                p.Content,
                p.Pic,
                p.CreateAt,
                p.ID,
                p.OrderNo,
                DelType = Enum.GetName(typeof(Entities.ProcessingOrder.DeliveryType), p.DelType),
                PickType = Enum.GetName(typeof(Entities.ProcessingOrder.PickUpType), p.PickType),
                Status = Enum.GetName(typeof(SK.Entities.ProcessingOrder.OrderStatus), p.Status),
                p.UserID,
                p.UserName,
                p.IsReject,
                p.StatusID
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
            order.IsReject = false;

            switch (order.Status)
            {
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
                    DoNoticePickUp(order,dc);//已通知客户提货
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.ConfirmPickUpMethod:
                    {
                        if (order.PickType == Entities.ProcessingOrder.PickUpType.LXD)
                        {
                            DoInputPickUp(order, dc);//确认录入送货资料（利迅达方录入资料）
                        }
                    }
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
                    {
                        this.ShowResult(true, "订单状态不允许操作");
                        return;
                    }
            }
        }

        public void uploadpic()
        {
            try
            {
                if (Request.Files.Count < 1)
                {
                    ShowResult(false, "请选择上传文件");
                    return;
                }

                var orderId = Request["orderid"];
                var fileUploaded = Request.Files[0];
                string[] allows = new string[] { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
                string path = ConfigurationManager.AppSettings["UploadPath"];
                string extend = System.IO.Path.GetExtension(fileUploaded.FileName);
                string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), extend);
                string file = System.IO.Path.Combine(path, fileName);
                var size = fileUploaded.ContentLength / (1024 * 1024);
                if (!allows.Contains(extend.ToLower()))
                {
                    ShowResult(false, "上传文件格式不正确");
                    return;
                }

                if (size > 2)
                {
                    ShowResult(false, "上传文件不能超过2M");
                    return;
                }

                fileUploaded.SaveAs(file);

                //保存成功后
                if (string.IsNullOrWhiteSpace(orderId))
                {
                    this.FailMessage("订单为空");
                    return;
                }

                ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
                AttachmentDataContext dcAttachment = new AttachmentDataContext();

                var order = dc.ProcessingOrder.FirstOrDefault(p => p.ID == orderId);
                if (order == null)
                {
                    this.FailMessage("订单不存在");
                    return;
                }

                var objReturn = new
                {
                    src = "/upload/" + fileName,
                    size = fileUploaded.ContentLength,
                    name = fileUploaded.FileName,
                    file = fileName,
                    createat = DateTime.Now,
                    updateat = DateTime.Now
                };
                       
                var ent = new Entities.Attachment();
                ent.ID = Guid.NewGuid().ToString("N");
                ent.CreateAt = DateTime.Now;
                ent.FileName = objReturn.file;
                ent.FilePath = objReturn.src;
                ent.FileSize = objReturn.size;
                ent.Name = objReturn.name;
                ent.SourceID = orderId;
                ent.UpdateAt = DateTime.Now;

                dcAttachment.Attachment.InsertOnSubmit(ent);
                dcAttachment.SubmitChanges();

                var returnObj = new
                {
                    code = 0,
                    msg = "成功",
                    data = new
                    {
                        src = "/upload/" + fileName,
                        size = fileUploaded.ContentLength,
                        name = fileUploaded.FileName,
                        file = fileName,
                        createat = DateTime.Now,
                        updateat = DateTime.Now
                    }
                };

                string json = JsonConvert.SerializeObject(returnObj);
                this.Response.Write(json);
            }
            catch (Exception ex)
            {
                ShowResult(false, ex.Message);
            }
        }

        public void deletepic()
        {
            //try
            //{
                var fileName = Request["FileName"];
                AttachmentDataContext dcAttachment = new AttachmentDataContext();
                var entity = dcAttachment.Attachment.Where(p => p.FileName == fileName).FirstOrDefault();
                if (entity != null)
                {
                    dcAttachment.Attachment.DeleteOnSubmit(entity);
                    dcAttachment.SubmitChanges();
                }

                ShowResult(true, "成功");
            //}
            //catch (Exception ex)
            //{
            //    ShowResult(false, ex.Message);
            //}
        }

        private void DoUploaded(SK.Entities.ProcessingOrder order, ProcessingOrderDataContext dc)
        {
            //
            SaveStatusLog(order, order.StatusID, order.Status, Entities.ProcessingOrder.OrderStatus.Uploaded);

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
                var Delivery_Content = QF("Delivery[Content]");
                var Delivery_DeliveryAt = QF("Delivery[DeliveryAt]");
                var Delivery_Time1 = QF("Delivery[Time1]");
                var Delivery_Time2 = QF("Delivery[Time2]");
                var Delivery_VehicleInfo = QF("Delivery[VehicleInfo]");

                if (string.IsNullOrEmpty(Delivery_Content)
                    || string.IsNullOrEmpty(Delivery_DeliveryAt)
                    || string.IsNullOrEmpty(Delivery_Time1)
                    || string.IsNullOrEmpty(Delivery_Time2)
                    || string.IsNullOrEmpty(Delivery_VehicleInfo))
                {
                    this.ShowResult(false, "提货信息不能为空");
                    return;
                }

                var ent = new Entities.DeliveryOrder();
                ent.Content = Delivery_Content;
                ent.CreateAt = DateTime.Now;
                ent.DeliveryAt = QF("Delivery[DeliveryAt]", DateTime.Now);
                ent.Time1 = Delivery_Time1;
                ent.Time2 = Delivery_Time2;
                ent.TimeSection = string.Format("{0}-{1}", ent.Time1, ent.Time2);
                ent.ID = Guid.NewGuid().ToString();
                ent.OrderNo = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
                ent.ProcessingNo = order.OrderNo;
                ent.SourceID = order.ID;
                ent.UserID = order.UserID;
                ent.UserName = order.UserName;
                ent.VehicleInfo = Delivery_VehicleInfo;

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

                //
                SaveStatusLog(order, order.StatusID, order.Status, Entities.ProcessingOrder.OrderStatus.InputDelivery);

                order.Status = Entities.ProcessingOrder.OrderStatus.InputDelivery;
                dc.SubmitChanges();

                //利迅达到客户那边提材料
                SendMessageForPickUp2(ent);//

                this.ShowResult(true, "保存成功");
            }
            else
            {
                this.ShowResult(false, "状态错误");
            }
        }

        private void DoInputPickUp(SK.Entities.ProcessingOrder order, ProcessingOrderDataContext dc)
        {
            if (order.Status == Entities.ProcessingOrder.OrderStatus.ConfirmPickUpMethod && order.PickType == Entities.ProcessingOrder.PickUpType.LXD)
            {
                var PickUp_Content = QF("PickUp[Content]");
                var PickUp_PickUpAt = QF("PickUp[PickUpAt]");
                var PickUp_Time1 = QF("PickUp[Time1]");
                var PickUp_Time2 = QF("PickUp[Time2]");
                var PickUp_VehicleInfo = QF("PickUp[VehicleInfo]");

                if (string.IsNullOrEmpty(PickUp_Content)
                    || string.IsNullOrEmpty(PickUp_PickUpAt)
                    || string.IsNullOrEmpty(PickUp_Time1)
                    || string.IsNullOrEmpty(PickUp_Time2)
                    || string.IsNullOrEmpty(PickUp_VehicleInfo))
                {
                    this.ShowResult(false, "送货信息不能为空");
                    return;
                }

                var ent = new Entities.PickUpOrder();
                ent.Content = PickUp_Content;
                ent.CreateAt = DateTime.Now;
                ent.PickUpAt = QF("PickUp[PickUpAt]", DateTime.Now);
                ent.Time1 = PickUp_Time1;
                ent.Time2 = PickUp_Time2;
                ent.TimeSection = string.Format("{0}-{1}", ent.Time1, ent.Time2);
                ent.ID = Guid.NewGuid().ToString();
                ent.OrderNo = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
                ent.ProcessingNo = order.OrderNo;
                ent.SourceID = order.ID;
                ent.UserID = order.UserID;
                ent.UserName = order.UserName;
                ent.VehicleInfo = PickUp_VehicleInfo;

                switch (order.PickType)
                {
                    case SK.Entities.ProcessingOrder.PickUpType.None:
                        break;
                    case SK.Entities.ProcessingOrder.PickUpType.Self:
                        ent.Type = Entities.PickUpOrder.OrderType.Self;
                        break;
                    case SK.Entities.ProcessingOrder.PickUpType.LXD:
                        ent.Type = Entities.PickUpOrder.OrderType.LXD;
                        break;
                    default:
                        break;
                }

                PickUpOrderDataContext dcPickUpOrder = new PickUpOrderDataContext();
                dcPickUpOrder.PickUpOrder.InsertOnSubmit(ent);
                dcPickUpOrder.SubmitChanges();

                //
                SaveStatusLog(order, order.StatusID, order.Status, Entities.ProcessingOrder.OrderStatus.InputPickUpContact);


                order.Status = Entities.ProcessingOrder.OrderStatus.InputPickUpContact;
                dc.SubmitChanges();

                this.ShowResult(true, "保存成功");
            }
            else
            {
                this.ShowResult(false, "状态错误");
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
            //
            SaveStatusLog(order, order.StatusID, order.Status, Entities.ProcessingOrder.OrderStatus.Warehousing);

            order.Status = Entities.ProcessingOrder.OrderStatus.Warehousing;
            dc.SubmitChanges();

            //材料已入库
            SendMessageForInLib(order, 1);

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

            //
            SaveStatusLog(order, order.StatusID, order.Status, Entities.ProcessingOrder.OrderStatus.Producing);


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

            //
            SaveStatusLog(order, order.StatusID, order.Status, Entities.ProcessingOrder.OrderStatus.Produced);


            order.Status = Entities.ProcessingOrder.OrderStatus.Produced;
            dc.SubmitChanges();

            //产品已入库
            SendMessageForInLib(order, 2);

            this.ShowResult(true, "保存成功");
        }

        /// <summary>
        /// 已通知客户提货
        /// </summary>
        /// <param name="order"></param>
        private void DoNoticePickUp(SK.Entities.ProcessingOrder order, ProcessingOrderDataContext dc)
        {
            if (order.Status != Entities.ProcessingOrder.OrderStatus.Produced)
            {
                this.FailMessage("状态错误");
                return;
            }

            //
            SaveStatusLog(order, order.StatusID, order.Status, Entities.ProcessingOrder.OrderStatus.NoticePickUp);


            order.Status = Entities.ProcessingOrder.OrderStatus.NoticePickUp;
            order.PickType = Entities.ProcessingOrder.PickUpType.None;
            dc.SubmitChanges();

            //通知提货
            SendMessageForPickUp(order);
            
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
            entt.UserID = order.UserID;
            entt.UserName = order.UserName;
            entt.Pic = QF("Fee[Pic]");
            entt.Type = QF("Fee[Type]").ToEnum<Entities.ProcessingFee.BillType>();

            ProcessingFeeDataContext dcProcessingFee = new ProcessingFeeDataContext();
            dcProcessingFee.ProcessingFee.InsertOnSubmit(entt);
            dcProcessingFee.SubmitChanges();

            //
            SaveStatusLog(order, order.StatusID, order.Status, Entities.ProcessingOrder.OrderStatus.AlreadyGoods);


            order.Status = Entities.ProcessingOrder.OrderStatus.AlreadyGoods;
            dc.SubmitChanges();

            //出库提醒
            SendMessageForOutLib(order);

            //加工费生成通知
            SendMessageForFee(entt);

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

            //
            SaveStatusLog(order, order.StatusID, order.Status, Entities.ProcessingOrder.OrderStatus.Shipped);

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

        /// <summary>
        /// 提货通知
        /// </summary>
        /// <param name="order"></param>
        private void SendMessageForPickUp(Entities.ProcessingOrder order)
        {
            string title = string.Format("{0}，您有一个提货信息", order.UserName);
            string tplPath = this.Context.Server.MapPath("/content/templates/提货通知.json");
            WXTemplateBL.SendMessageForPickUp(
                order.UserID,
                tplPath,
                Config.Setting.WXWebHost + "/dist/#/Pages/JgdInfo?ID=" + order.ID,
                title,
                "",
                "",
               "",
               "",
               "",
               string.Format("加工单：{0}已加工完毕，请贵司安排提货。", order.OrderNo));
        }

        /// <summary>
        /// 利迅达到客户那里提材料，需要提前通知
        /// </summary>
        /// <param name="order"></param>
        private void SendMessageForPickUp2(Entities.DeliveryOrder order)
        {
            string title = string.Format("{0}，您有一个提货通知", order.UserName);
            string tplPath = this.Context.Server.MapPath("/content/templates/提货通知.json");
            WXTemplateBL.SendMessageForPickUp(
                order.UserID,
                tplPath,
                Config.Setting.WXWebHost + "/dist/#/Pages/JgdDetail?ID=" + order.ID,
                title,
                "",
                "",
               "",
               "",
               "",
               string.Format("我司将会到贵司提货，请贵司提前准备材料，预计时间：{0}，{1}", order.TimeSection,order.VehicleInfo));
        }

        /// <summary>
        /// 出库提醒
        /// </summary>
        /// <param name="order"></param>
        private void SendMessageForOutLib(Entities.ProcessingOrder order)
        {
            string title = string.Format("{0}，您有产品可提货", order.UserName);
            string tplPath = this.Context.Server.MapPath("/content/templates/提货通知.json");
            WXTemplateBL.SendMessageForOutLib(
                order.UserID,
                tplPath,
                "",
                title,
                order.OrderNo,
                DateTime.Now.ToString("yyyy-MM-dd"),
               order.UserName,
               "",
               order.Content);
        }

        /// <summary>
        /// 材料入库通知
        /// </summary>
        /// <param name="order"></param>
        /// <param name="type">1:材料，2：产品</param>
        private void SendMessageForInLib(Entities.ProcessingOrder order,int type)
        {
            string title = string.Format("{0}，您的材料已入库", order.UserName);
            if (type == 2) {
                title = string.Format("{0}，您的产品加工完已入库", order.UserName);
            }
            string tplPath = this.Context.Server.MapPath("/content/templates/商品入库通知.json");
            WXTemplateBL.SendMessageForInLib(
                order.UserID,
                tplPath,
                Config.Setting.WXWebHost + "/dist/#/Pages/JgdDetail?ID=" + order.ID,
                title,
                order.OrderNo,
                "详细见单",
               DateTime.Now.ToString("yyyy-MM-dd"),
               order.Content);
        }

        /// <summary>
        /// 加工费生成通知
        /// </summary>
        /// <param name="order"></param>
        private void SendMessageForFee(Entities.ProcessingFee order)
        {
            string title = string.Format("{0}，您的加工费已生成，请查看", order.UserName);

            string tplPath = this.Context.Server.MapPath("/content/templates/加工费生成通知.json");
            WXTemplateBL.SendMessageForFee(
                order.UserID,
                tplPath,
                Config.Setting.WXWebHost + "/dist/#/Pages/FeeInfo?ID=" + order.ID,
                title,
                order.FeeNo,
                DateTime.Now.ToString("yyyy-MM-dd"),
               "见明细",
               order.Content);
        }

        private void SaveStatusLog(SK.Entities.ProcessingOrder order,
            string oldStatusID,
            SK.Entities.ProcessingOrder.OrderStatus oldStatus, 
            SK.Entities.ProcessingOrder.OrderStatus newStatus)
        {
            var statusId = Guid.NewGuid().ToString();

            //如果状态有变化则新增状态
            if (oldStatus != newStatus)
            {
                StatusLogDataContext statusCxt = new StatusLogDataContext();

                StatusLog log = new StatusLog();
                log.ID = statusId;
                log.Status = order.Status;
                log.CreateAt = DateTime.Now;
                log.PreID = oldStatusID;
                log.OrderID = order.ID;
                
                statusCxt.StatusLog.InsertOnSubmit(log);
                statusCxt.SubmitChanges();
            }

            order.StatusID = statusId;
        }
    }
}
