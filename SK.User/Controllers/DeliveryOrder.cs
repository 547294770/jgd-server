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
    /// 送货单信息
    /// </summary>
    public class DeliveryOrder : BasePage
    {
        public void list()
        {
            DeliveryOrderDataContext dc = new DeliveryOrderDataContext();
            var list = dc.DeliveryOrder.Where(p => p.UserID == UserInfo.openid);

            var data = list.OrderByDescending(p => p.CreateAt).Select(p => p).ToList();
            this.ShowResult(true, "成功", data);
        }

        public void info()
        {
            string id = QF("ID");

            DeliveryOrderDataContext dc = new DeliveryOrderDataContext();
            var entity = dc.DeliveryOrder.FirstOrDefault(p => p.ID == id);

            this.ShowResult(true, "成功",
                new
                {
                    entity.Content,
                    CreateAt = entity.CreateAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    entity.ID,
                    entity.OrderNo,
                    entity.DeliveryAt,
                    entity.ProcessingNo,
                    entity.SourceID,
                    TypeName = Enum.GetName(typeof(Entities.DeliveryOrder.OrderType), entity.Type),
                    entity.VehicleInfo
                });
        }
    }
}
