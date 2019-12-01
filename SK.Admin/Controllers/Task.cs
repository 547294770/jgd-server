using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities;
using SK.Handler;

namespace SK.Admin.Controllers
{
    public class Task : BasePage
    {
        public void list()
        {
            CompanyTaskDataContext cxt1 = new CompanyTaskDataContext();
            ProcessingOrderDataContext cxt2 = new ProcessingOrderDataContext();
            WXUserDataContext cxt3 = new WXUserDataContext();

            SK.Entities.ProcessingOrder.OrderStatus[] status = new SK.Entities.ProcessingOrder.OrderStatus[] { 
                SK.Entities.ProcessingOrder.OrderStatus.Processing,
                SK.Entities.ProcessingOrder.OrderStatus.InputDelivery,
                SK.Entities.ProcessingOrder.OrderStatus.Warehousing,
                SK.Entities.ProcessingOrder.OrderStatus.Producing,
                SK.Entities.ProcessingOrder.OrderStatus.Produced,
                SK.Entities.ProcessingOrder.OrderStatus.InputPickUpContact,
                SK.Entities.ProcessingOrder.OrderStatus.ConfirmationFee
            };
            
            var list = cxt2.ProcessingOrder.Where(p => status.Contains(p.Status) 
            || (p.Status == Entities.ProcessingOrder.OrderStatus.ConfirmDeliveryMethod && p.DelType == Entities.ProcessingOrder.DeliveryType.LXD)
            || (p.Status == Entities.ProcessingOrder.OrderStatus.ConfirmPickUpMethod && p.PickType == Entities.ProcessingOrder.PickUpType.LXD));

            var userCount = cxt3.WXUser.Where(p => !p.ispass).Count();

            var companyCount = cxt1.CompanyTask.Where(p => !p.IsPass).Count();
            var orderCount = list.Count();

             this.ShowResult(true, "成功", new {
                 CompanyCount = companyCount,
                 OrderCount = orderCount,
                 UserCount = userCount
             });
        }
    }
}
