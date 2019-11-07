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
    /// 提货单信息
    /// </summary>
    public class PickUpOrder : BasePage
    {
        public void list()
        {
            PickUpOrderDataContext dc = new PickUpOrderDataContext();
            var list = dc.PickUpOrder.Where(p => p.UserID == UserInfo.openid);

            var data = list.OrderByDescending(p => p.CreateAt).Select(p => p).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void info()
        {
            string id = QF("ID");

            PickUpOrderDataContext dc = new PickUpOrderDataContext();
            var entity = dc.PickUpOrder.FirstOrDefault(p => p.ID == id);

            this.ShowResult(true, "成功",
                new
                {
                    entity.Content,
                    CreateAt = entity.CreateAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    entity.ID,
                    entity.OrderNo,
                    entity.PickUpAt,
                    entity.ProcessingNo,
                    entity.SourceID,
                    TypeName = Enum.GetName(typeof(Entities.PickUpOrder.OrderType), entity.Type),
                    entity.VehicleInfo
                });
        }
    }
}
