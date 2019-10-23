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

    partial class PickUpOrder
    {
         public enum OrderType : byte
         { 
             /// <summary>
             /// 客户提货
             /// </summary>
            [Description("客户提货")]
            Self,
             /// <summary>
            /// 利迅达送货
             /// </summary>
             [Description("利迅达送货")]
             LXD
         }
    }
}
