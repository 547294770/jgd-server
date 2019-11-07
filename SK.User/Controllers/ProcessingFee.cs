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
    /// <summary>
    /// 加工费单信息
    /// </summary>
    public class ProcessingFee : BasePage
    {
        public void list()
        {
            ProcessingFeeDataContext dc = new ProcessingFeeDataContext();
            var list = dc.ProcessingFee.Where(p => p.UserID == UserInfo.openid);

            var data = list.OrderByDescending(p => p.CreateAt).Select(p => p).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void info()
        {
            string id = QF("ID");

            ProcessingFeeDataContext dc = new ProcessingFeeDataContext();
            var entity = dc.ProcessingFee.FirstOrDefault(p => p.ID == id);

            this.ShowResult(true, "成功",
                new
                {
                    entity.Content,
                    CreateAt = entity.CreateAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    entity.FeeNo,
                    entity.ID,
                    entity.SourceID,
                    entity.ProcessingNo,
                    entity.Type,
                    TypeName = Enum.GetName(typeof(Entities.ProcessingFee.BillType), entity.Type)
                });
        }
    }
}
