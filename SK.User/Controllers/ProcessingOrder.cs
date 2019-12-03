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

namespace SK.User.Controllers
{
    /// <summary>
    /// 加工单信息
    /// </summary>
    public class ProcessingOrder : BasePage
    {
        SK.Entities.ProcessingOrder.OrderStatus[] status = new SK.Entities.ProcessingOrder.OrderStatus[] { 
                SK.Entities.ProcessingOrder.OrderStatus.Uploaded,
                SK.Entities.ProcessingOrder.OrderStatus.Print,
                SK.Entities.ProcessingOrder.OrderStatus.NoticePickUp,
                SK.Entities.ProcessingOrder.OrderStatus.AlreadyGoods,
                SK.Entities.ProcessingOrder.OrderStatus.Shipped,
                SK.Entities.ProcessingOrder.OrderStatus.None
            };

        public void todocount()
        {
            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            var list = dc.ProcessingOrder.Where(p => p.UserID == UserInfo.openid);

            list = list.Where(p => status.Contains(p.Status) ||
                (p.Status == Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod && p.DelType == Entities.ProcessingOrder.DeliveryType.Self)
                || (p.Status == Entities.ProcessingOrder.OrderStatus.ConfirmPickUpMethod && p.PickType == Entities.ProcessingOrder.PickUpType.Self)
                );

            var returnObj = new
            {
                ProcessingOrderCount = list.Count()
            };

            this.ShowResult(true, "成功", returnObj);
        }

        public void todolist()
        {
            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            var list = dc.ProcessingOrder.Where(p => p.UserID == UserInfo.openid);
            list = list.Where(p =>status.Contains(p.Status) || 
                (p.Status == Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod && p.DelType == Entities.ProcessingOrder.DeliveryType.Self)
                ||(p.Status == Entities.ProcessingOrder.OrderStatus.ConfirmPickUpMethod && p.PickType == Entities.ProcessingOrder.PickUpType.Self)
                );

            var page = 1;
            var pageSize = 10;

            int.TryParse(Request["page"], out page);
            int.TryParse(Request["size"], out pageSize);

            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            int count = list.Count();
            int skip = (page - 1) * pageSize;
            int pageCount = 0;
            var lastPage = false;
            if (count % pageSize > 0) pageCount = count / pageSize + 1;
            else pageCount = count / pageSize;

            if (page >= pageCount) lastPage = true;
            else lastPage = false;

            list = list.OrderByDescending(p => p.UpdateAt).Skip(skip).Take(pageSize);

            var data = list.Select(p =>
                 new
                 {
                     p.Content,
                     p.CreateAt,
                     p.ID,
                     p.OrderNo,
                     Processing = status.Contains(p.Status),
                     Status = Enum.GetName(typeof(SK.Entities.ProcessingOrder.OrderStatus), p.Status),
                     StatusName = p.Status.GetDescription(),
                     p.UserID,
                     p.UserName
                 }
            ).ToList();

            this.ShowResult(true, "成功", new
            {
                lastPage = lastPage,
                data = data
            });
        }

        public void list()
        {
            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            var list = dc.ProcessingOrder.Where(p => p.UserID == UserInfo.openid);

            var page = 1;
            var pageSize = 10;

            int.TryParse(Request["page"], out page);
            int.TryParse(Request["size"], out pageSize);

            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;

            int count = list.Count();
            int skip = (page - 1) * pageSize;
            int pageCount = 0;
            var lastPage = false;
            if (count % pageSize > 0) pageCount = count / pageSize + 1;
            else pageCount = count / pageSize;

            if (page >= pageCount) lastPage = true;
            else lastPage = false;

            list = list.OrderByDescending(p=>p.UpdateAt).Skip(skip).Take(pageSize);
            var data = list.OrderByDescending(p => p.UpdateAt).Select(p =>
                 new
                 {
                     p.Content,
                     p.CreateAt,
                     p.ID,
                     p.OrderNo,
                     Status = Enum.GetName(typeof(SK.Entities.ProcessingOrder.OrderStatus), p.Status),
                     StatusName = p.Status.GetDescription(),
                     p.UserID,
                     p.IsReject,
                     p.UserName
                 }
            ).ToList();

            this.ShowResult(true, "成功", new
                 {
                     lastPage = lastPage,
                     data = data
                 });
        }

        public void delete()
        {
            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            var entity = dc.ProcessingOrder.FirstOrDefault(p => p.ID == QF("ID"));

            if (entity == null)
            {
                this.ShowResult(false, "记录不存在");
                return;
            }
         
            dc.ProcessingOrder.DeleteOnSubmit(entity);
            dc.SubmitChanges();

            this.ShowResult(true, "删除成功");
        }

        public void add()
        {
            if (UserInfo == null) { this.FailMessage("未登录"); return; }
            if (!UserInfo.ispass) { this.FailMessage("您账户未通过审核"); return; }

            CompanyDataContext cxtCompany = new CompanyDataContext();
            var entity = cxtCompany.Company.FirstOrDefault(p => p.UserID == UserInfo.openid);
            if (entity == null) { this.FailMessage("请在【设置】->【公司信息】完善公司资料再操作。"); return; }

            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();

            SK.Entities.ProcessingOrder.OrderStatus[] status = new SK.Entities.ProcessingOrder.OrderStatus[] { 
                SK.Entities.ProcessingOrder.OrderStatus.None,
                SK.Entities.ProcessingOrder.OrderStatus.Processing
            };

            var orderId = QF("ID");
            var isUpdate = false;

            var order = dc.ProcessingOrder.FirstOrDefault(p => p.ID == orderId);
            if (order == null)
            {
                order = new Entities.ProcessingOrder();
            }
            else {
                isUpdate = true;
                if (!status.Contains(order.Status))
                {
                    this.ShowResult(false, "该加工状态不允许修改");
                    return;
                }
            }

            order =  this.Request.Form.Fill<Entities.ProcessingOrder>(order);

            if (string.IsNullOrWhiteSpace(order.Content) && string.IsNullOrWhiteSpace(order.Pic))
            {
                this.ShowResult(false, "加工内容、图片不能同时为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(order.ID))
            {
                order.ID = Guid.NewGuid().ToString();
                order.CreateAt = DateTime.Now;
                order.UpdateAt = order.CreateAt;
                order.Status = Entities.ProcessingOrder.OrderStatus.None;
                order.UserID = UserInfo.openid;
                order.UserName = UserInfo.nickname;
                order.OrderNo = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));

                dc.ProcessingOrder.InsertOnSubmit(order);
            }
            else {
                order.UpdateAt = DateTime.Now;
            }

            StatusLogDataContext statusCxt = new StatusLogDataContext();
            StatusLog log = new StatusLog();
            var statusid = Guid.NewGuid().ToString();
            if (isUpdate) {
                statusid = order.StatusID;
            }

            log.CreateAt = DateTime.Now;
            log.ID = statusid;
            log.OrderID = order.ID;
            log.PreID = statusid;
            log.Status = order.Status;

            order.StatusID = log.ID;

            if (!isUpdate)
            {
                statusCxt.StatusLog.InsertOnSubmit(log);
            }

            //1：草稿，0：直接提交
            if (QF("IsDraft", 0) == 0)
            {
                var newStatusId = Guid.NewGuid().ToString();
                order.Status = Entities.ProcessingOrder.OrderStatus.Processing;
                order.StatusID = newStatusId;

                StatusLog log2 = new StatusLog();

                log2.CreateAt = DateTime.Now;
                log2.ID = newStatusId;
                log2.OrderID = order.ID;
                log2.PreID = statusid;
                log2.Status = order.Status;

                statusCxt.StatusLog.InsertOnSubmit(log2);
            }

            statusCxt.SubmitChanges();

            dc.SubmitChanges();

            //1：草稿，0：直接提交
            if (QF("IsDraft", 0) == 0)
            {
                //直接提交，就是新订单，推送通知
                SendMessageForNewOrder(order);
            }

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
            if (order == null) {
                this.ShowResult(false, "记录不存在");
                return;
            }

            AttachmentDataContext adc = new AttachmentDataContext();
            var attachments = adc.Attachment.Where(p => p.SourceID == order.ID).Select(p => new { 
                p.CreateAt,
                p.FileName,
                p.FilePath,
                p.FileSize,
                p.ID,
                p.Name,
                p.SourceID,
                p.UpdateAt
            }).ToList();

            DeliveryOrderDataContext delcxt = new DeliveryOrderDataContext();
            var deliverylist = delcxt.DeliveryOrder.Where(p => p.SourceID == order.ID).Select(p => new {
                p.Content,
                p.CreateAt,
                p.OrderNo,
                p.DeliveryAt,
                p.ProcessingNo,
                p.SourceID,
                p.TimeSection,
                p.Time1,
                p.Time2,
                TypeName = p.Type.GetDescription(),
                p.VehicleInfo
            }).ToList();

            PickUpOrderDataContext pickcxt = new PickUpOrderDataContext();
            var pickuplist = pickcxt.PickUpOrder.Where(p => p.SourceID == order.ID).Select(p => new
            {
                p.Content,
                p.CreateAt,
                p.OrderNo,
                p.PickUpAt,
                p.ProcessingNo,
                p.SourceID,
                p.TimeSection,
                p.Time1,
                p.Time2,
                TypeName = p.Type.GetDescription(),
                p.VehicleInfo
            }).ToList();

            ProcessingFeeDataContext feecxt = new ProcessingFeeDataContext();
            var feelist = feecxt.ProcessingFee.Where(p => p.SourceID == order.ID).Select(p => new {
                TypeName = p.Type.GetDescription(),
                p.FeeNo,
                p.Pic,
                p.Content
            }).ToList();

            this.ShowResult(true, "成功",
                new
                {
                    order.Content,
                    CreateAt = order.CreateAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    order.ID,
                    order.OrderNo,
                    Status = Enum.GetName(typeof(SK.Entities.ProcessingOrder.OrderStatus), order.Status),
                    StatusName = order.Status.GetDescription(),
                    DelType = Enum.GetName(typeof(SK.Entities.ProcessingOrder.DeliveryType), order.DelType),
                    DelTypeName = order.DelType.GetDescription(),
                    PickType = Enum.GetName(typeof(SK.Entities.ProcessingOrder.PickUpType), order.PickType),
                    PickTypeName = order.PickType.GetDescription(),
                    AttachmentList = attachments,
                    DeliveryList = deliverylist,
                    PickUpList = pickuplist,
                    FeeList = feelist,
                    order.UpdateAt,
                    order.UserID,
                    order.UserName,
                    order.IsReject,
                    order.Pic,
                    IsSelf = UserInfo != null ? order.UserID == UserInfo.openid : false
                });
        }

        /// <summary>
        /// 处理订单
        /// </summary>
        public void exeorder()
        {
            if (UserInfo == null) { this.FailMessage("未登录"); return; }
            if (!UserInfo.ispass) { this.FailMessage("您账户未通过审核"); return; }

            string orderId = QF("OrderID");

            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            var order = dc.ProcessingOrder.FirstOrDefault(p => p.ID == orderId);
            
            if (order == null)
            {
                this.FailMessage("订单不存在");
                return;
            }

            var oldStatus = order.Status;
            var oldStatusID = order.StatusID;

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
                    {
                        order.DelType = QF("DelType").ToEnum<Entities.ProcessingOrder.DeliveryType>();
                        order.Status = Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod;

                        if (order.DelType == Entities.ProcessingOrder.DeliveryType.IsWareHouse)
                        {
                            order.Status = Entities.ProcessingOrder.OrderStatus.Warehousing;
                        }
                        if (order.DelType == Entities.ProcessingOrder.DeliveryType.None) {
                            this.FailMessage("请选择送货类型");
                            return;
                        }
                    }
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod://
                    {
                        if (order.DelType != Entities.ProcessingOrder.DeliveryType.Self)
                        {
                            this.FailMessage("订单的送货类型不正确");
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(QF("Delivery[Content]"))
                            || string.IsNullOrWhiteSpace(QF("Delivery[DeliveryAt]"))
                            || string.IsNullOrWhiteSpace(QF("Delivery[Time1]"))
                            || string.IsNullOrWhiteSpace(QF("Delivery[Time2]"))
                            || string.IsNullOrWhiteSpace(QF("Delivery[VehicleInfo]"))) {
                                this.FailMessage("送货信息不能为空");
                                return;
                        }

                        DoDelivery(order);
                    }
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.NoticePickUp://
                    order.PickType = QF("PickType").ToEnum<Entities.ProcessingOrder.PickUpType>();
                    order.Status = Entities.ProcessingOrder.OrderStatus.ConfirmPickUpMethod;
                    if (order.PickType == Entities.ProcessingOrder.PickUpType.None)
                    {
                        this.FailMessage("请选择提货类型");
                        return;
                    }
                    break;
                case SK.Entities.ProcessingOrder.OrderStatus.ConfirmPickUpMethod://
                    {
                        if (order.PickType != Entities.ProcessingOrder.PickUpType.Self)
                        {
                            this.FailMessage("订单的提货类型不正确");
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(QF("PickUp[Content]"))
                                || string.IsNullOrWhiteSpace(QF("PickUp[PickUpAt]"))
                                || string.IsNullOrWhiteSpace(QF("PickUp[Time1]"))
                                || string.IsNullOrWhiteSpace(QF("PickUp[Time2]"))
                                || string.IsNullOrWhiteSpace(QF("PickUp[VehicleInfo]")))
                        {
                            this.FailMessage("提货信息不能为空");
                            return;
                        }
                        DoPickup(order);
                    }
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

            var statusId = Guid.NewGuid().ToString();

            //如果状态有变化则新增状态
            if (oldStatus != order.Status)
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
            order.UpdateAt = DateTime.Now;
            dc.SubmitChanges();

            var returnObj = new { 
                order.ID,
                Status = Enum.GetName(typeof(SK.Entities.ProcessingOrder.OrderStatus), order.Status),
                DelType = Enum.GetName(typeof(SK.Entities.ProcessingOrder.DeliveryType), order.DelType),
                PickType = Enum.GetName(typeof(SK.Entities.ProcessingOrder.PickUpType), order.PickType)
            };

            this.ShowResult(true, "操作成功", returnObj);
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
            ent.Time1 = QF("Delivery[Time1]");
            ent.Time2 = QF("Delivery[Time2]");
            ent.TimeSection = string.Format("{0}-{1}", ent.Time1, ent.Time2);
            ent.ID = Guid.NewGuid().ToString();
            ent.OrderNo = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
            ent.ProcessingNo = order.OrderNo;
            ent.SourceID = order.ID;
            ent.UserID = UserInfo.openid;
            ent.UserName = UserInfo.nickname;
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
            ent.Time1 = QF("PickUp[Time1]");
            ent.Time2 = QF("PickUp[Time2]");
            ent.TimeSection = string.Format("{0}-{1}", ent.Time1, ent.Time2);
            ent.ID = Guid.NewGuid().ToString();
            ent.OrderNo = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
            ent.ProcessingNo = order.OrderNo;
            ent.SourceID = order.ID;
            ent.UserID = UserInfo.openid;
            ent.UserName = UserInfo.nickname;
            ent.VehicleInfo = QF("PickUp[VehicleInfo]");

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
                Config.Setting.WXWebHost + "/dist/#/Pages/JgdDetail?ID=" + order.ID,
                "新订单通知",
                order.UserName,
                "加工单",
               order.OrderNo,
               order.Content);
        }
    }
}
