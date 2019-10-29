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
    public class ProcessingFee : BasePage
    {
        public void list()
        {
            ProcessingFeeDataContext dc = new ProcessingFeeDataContext();
            var list = dc.ProcessingFee.Where(p=>p.SourceID == QF("OrderID"));

            if (!string.IsNullOrEmpty(QF("FeeNo"))) list = list.Where(p => p.FeeNo == QF("FeeNo"));
            if (!string.IsNullOrEmpty(QF("ProcessingNo"))) list = list.Where(p => p.ProcessingNo == QF("ProcessingNo"));
            if (!string.IsNullOrEmpty(QF("StartAt"))) list = list.Where(p => p.CreateAt >= QF("StartAt", DateTime.Now.Date));
            if (!string.IsNullOrEmpty(QF("EndAt"))) list = list.Where(p => p.CreateAt <= QF("EndAt", DateTime.Now.Date.AddDays(1)).AddDays(1));
            if (!string.IsNullOrEmpty(QF("Type"))) list = list.Where(p => p.Type == QF("Type").ToEnum<SK.Entities.ProcessingFee.BillType>());

            var data = list.Select(a => new
            {
                a.ID,
                a.SourceID,
                a.FeeNo,
                a.ProcessingNo,
                a.Content,
                a.CreateAt,
                TypeName =a.Type.GetDescription()
            }).ToList();

            this.ShowResult(true, "成功", data);
        }
    }
}
