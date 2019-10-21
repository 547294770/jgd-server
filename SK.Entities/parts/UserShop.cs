using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Entities
{
    partial class UserShop
    {
        public enum VerMethodEnum
        { 
            /// <summary>
            /// 支付宝实名认证
            /// </summary>
            AlipaySM,

            /// <summary>
            /// 消费者保障协议
            /// </summary>
            ConsumerService
        }
    }
}
