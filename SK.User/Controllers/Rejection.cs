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
    /// 错误驳回信息
    /// </summary>
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

        public void add()
        {
            var orderId = QF("OrderID");
            var reason = QF("Reason");

            ProcessingOrderDataContext proCxt = new ProcessingOrderDataContext();
            var orderInfo = proCxt.ProcessingOrder.FirstOrDefault(p => p.ID == orderId);
            if (orderInfo == null) {
                this.ShowResult(false, "订单不存在");
                return;
            }

            if (orderInfo.IsReject)
            {
                this.ShowResult(false, "该订单正处于驳回状态，不能再再次驳回。");
                return;
            }

            if (string.IsNullOrEmpty(reason))
            {
                this.ShowResult(false, "请填写驳回原因。");
                return;
            }

            RejectionDataContext cxt = new RejectionDataContext();

            SK.Entities.Rejection entity = new Entities.Rejection();
        
            entity.ID = Guid.NewGuid().ToString();
            entity.Reason = reason;
            entity.CreateAt = DateTime.Now;
            entity.SourceID = orderInfo.ID;
            entity.UserID = UserInfo.openid;
            entity.UserName = UserInfo.nickname;

            cxt.Rejection.InsertOnSubmit(entity);
            cxt.SubmitChanges();

            StatusLogDataContext statusCxt = new StatusLogDataContext();
            var statusLog =  statusCxt.StatusLog.FirstOrDefault(p=>p.ID == orderInfo.StatusID);
            if (statusLog != null)
            {
                var preStatusLog = statusCxt.StatusLog.FirstOrDefault(p => p.ID == statusLog.PreID);
                if (preStatusLog != null)
                {
                    StatusLog newStatues = new StatusLog();
                    newStatues.ID = Guid.NewGuid().ToString();
                    newStatues.CreateAt = DateTime.Now;
                    newStatues.OrderID = orderInfo.ID;
                    newStatues.PreID = statusLog.ID;
                    newStatues.Status = preStatusLog.Status;

                    statusCxt.StatusLog.InsertOnSubmit(newStatues);
                    statusCxt.SubmitChanges();

                    orderInfo.Status = preStatusLog.Status;
                    orderInfo.IsReject = true;
                    orderInfo.StatusID = newStatues.ID;

                    proCxt.SubmitChanges();
                }
            }

            this.ShowResult(true, "保存成功");
        }
    }
}
