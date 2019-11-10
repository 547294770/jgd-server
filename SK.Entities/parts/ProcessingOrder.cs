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
            Processing = 10,
            [Description("已上传加工单截图")]
            Uploaded = 20,
            [Description("已确认加工内容")]
            Print = 30,
            [Description("已确认送货方式")]
            ConfirmDeliveryMethod = 40,
            [Description("已录入送货资料")]
            InputDelivery = 50,
            [Description("材料已入库")]
            Warehousing = 60,
            [Description("已安排生产")]
            Producing = 70,
            [Description("生产完已入库")]
            Produced = 80,
            [Description("已通知客户提货")]
            NoticePickUp = 90,
            [Description("已确认提货方式")]
            ConfirmPickUpMethod = 91,
            [Description("已录入提货资料")]
            InputPickUpContact = 100,
            [Description("已备货")]
            AlreadyGoods = 110,
            [Description("已确认加工费")]
            ConfirmationFee = 120,
            [Description("已发货")]
            Shipped = 130,
            [Description("已完成")]
            Finished = 140
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
            [Description("客户自提")]
            Self = 1,
            /// <summary>
            /// 利迅达送货
            /// </summary>
            [Description("利迅达送货")]
            LXD = 2
        }
        
    }
}
