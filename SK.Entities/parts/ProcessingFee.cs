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
    partial class ProcessingFee
    {
        public enum BillType : byte
        {
            /// <summary>
            /// 月结
            /// </summary>
            [Description("月结")]
            Month,
            /// <summary>
            /// 现结
            /// </summary>
            [Description("现结")]
            Now
        }
    }
}
