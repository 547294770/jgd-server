using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Entities.Enums
{
    /// <summary>
    /// 平台类型
    /// </summary>
    public enum PlatformType
    {
        /// <summary>
        /// 淘宝天猫
        /// </summary>
        [Description("淘宝天猫")]
        TaobaoTmall,

        /// <summary>
        /// 阿里巴巴
        /// </summary>
        [Description("阿里巴巴")]
        Alibaba,

        /// <summary>
        /// 京东
        /// </summary>
        [Description("京东")]
        JD,

        /// <summary>
        /// 蘑菇街
        /// </summary>
        [Description("蘑菇街")]
        MOGU,

        /// <summary>
        /// 拼多多
        /// </summary>
        [Description("拼多多")]
        PDD
    }

    /// <summary>
    /// 试用类型
    /// </summary>
    public enum TryType
    {
        /// <summary>
        /// 正品试用
        /// </summary>
        Authentic,

        /// <summary>
        /// 拍正品发赠品
        /// </summary>
        Tester
    }

    /// <summary>
    /// 下单方式
    /// </summary>
    public enum OrderMethod
    {
        /// <summary>
        /// 电脑
        /// </summary>
        PC,

        /// <summary>
        /// 手机
        /// </summary>
        Mobile
    }

    /// <summary>
    /// 进店下单方式
    /// </summary>
    public enum EnterMethod
    {
        /// <summary>
        /// 搜索宝贝进店
        /// </summary>
        SearchProduct,

        /// <summary>
        /// 搜索店铺进店
        /// </summary>
        SearchShop,

        /// <summary>
        /// 直通车下单
        /// </summary>
        ZTC,

        /// <summary>
        /// 指定网站来路
        /// </summary>
        SpecailUrl,

        /// <summary>
        /// 直接打开下单
        /// </summary>
        Direct,

        /// <summary>
        /// 淘口令下单
        /// </summary>
        TPassword,

        /// <summary>
        /// 扫二维码下单
        /// </summary>
        ScanQRcode
    }
}
