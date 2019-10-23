using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Entities
{
    partial class ProcessingOrder
    {
        public enum OrderStatus : int
        {
            [Description("未提交")]
            None = 0,
            [Description("已提交待处理")]
            Processing = 1,
            [Description("已上传加工单截图")]
            Uploaded = 2,
            [Description("已确认加工内容")]
            Print = 3,
            [Description("已确认提货方式")]
            ConfirmDeliveryMethod = 4,
            [Description("已录入送货资料")]
            InputDelivery = 5,
            [Description("材料已入库")]
            Warehousing = 6,
            [Description("已安排生产")]
            Producing = 7,
            [Description("生产完已入库")]
            Produced = 8,
            [Description("已通知客户提货")]
            NoticePickUp = 9,
            [Description("已录入提货资料")]
            InputPickUpContact = 10,
            [Description("已备货")]
            AlreadyGoods = 11,
            [Description("已确认加工费")]
            ConfirmationFee = 12,
            [Description("已发货")]
            Shipped = 13,
            [Description("已完成")]
            Finished = 14
        }

        public enum DeliveryType
        {
            /// <summary>
            /// 未指定
            /// </summary>
            [Description("未指定")]
            None = 0,
            /// <summary>
            /// 客户自送
            /// </summary>
            [Description("客户自送")]
            Self = 1,
            /// <summary>
            /// 利迅达提货
            /// </summary>
            [Description("利迅达提货")]
            LXD = 2
        }

        public enum PickUpType
        {
            /// <summary>
            /// 未指定
            /// </summary>
            [Description("未指定")]
            None = 0,
            /// <summary>
            /// 客户自送
            /// </summary>
            [Description("客户自送")]
            Self = 1,
            /// <summary>
            /// 利迅达提货
            /// </summary>
            [Description("利迅达提货")]
            LXD = 2
        }
        
    }
}
