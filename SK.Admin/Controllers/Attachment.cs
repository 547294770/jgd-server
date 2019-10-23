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

namespace SK.Admin.Controllers
{
    public class Attachment : BasePage
    {
        public void list()
        {
            if (string.IsNullOrWhiteSpace(QF("OrderID")))
            {
                this.FailMessage("订单为空");
                return;
            }

            AttachmentDataContext dc = new AttachmentDataContext();

            var list = dc.Attachment.Where(p => p.SourceID == QF("OrderID"));
            var data = list.OrderByDescending(p => p.UpdateAt).Select(p => p).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void save()
        {
            var isTrue = string.IsNullOrWhiteSpace(QF("data"));
            var orderId = QF("OrderID");
            if (string.IsNullOrWhiteSpace(orderId))
            {
                this.FailMessage("订单为空");
                return;
            }

            ProcessingOrderDataContext dc = new ProcessingOrderDataContext();
            AttachmentDataContext dcAttachment = new AttachmentDataContext();

            var order = dc.ProcessingOrder.FirstOrDefault(p => p.ID == orderId);
            if (order == null) {
                this.FailMessage("订单不存在");
                return;
            }

            var attachs = dcAttachment.Attachment.Where(p => p.SourceID == order.ID);
            dcAttachment.Attachment.DeleteAllOnSubmit(attachs);

            List<Entities.Attachment> aaaa = new List<Entities.Attachment>();
            if (!isTrue) {

                string data = QF("data");
                List<SK.Entities.Attachment> json = JsonConvert.DeserializeObject<List<SK.Entities.Attachment>>(data);

                foreach (var item in json)
                {
                    item.SourceID = order.ID;
                    var ent = new Entities.Attachment();
                    ent.ID = Guid.NewGuid().ToString("N");
                    ent.CreateAt = DateTime.Now;
                    ent.FileName = item.FileName;
                    ent.FilePath = "/upload/" + ent.FileName;
                    ent.FileSize = item.FileSize;
                    ent.Name = item.Name;
                    ent.SourceID = item.SourceID;
                    ent.UpdateAt = DateTime.Now;

                    aaaa.Add(ent);
                }
            }

            dcAttachment.Attachment.InsertAllOnSubmit(aaaa);
            dcAttachment.SubmitChanges();

            this.ShowResult(true, "提交成功");
        }
    }
}
