using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities.Enums;

namespace SK.Entities
{
    
     partial class DeliveryOrder
    {
         public enum OrderType : byte
         { 
             /// <summary>
             /// 客户自送
             /// </summary>
            [Description("客户自送")]
            Self,
             /// <summary>
             /// 利迅达提货
             /// </summary>
             [Description("利迅达提货")]
             LXD
         }
    }
}
