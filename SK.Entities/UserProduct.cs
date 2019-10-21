using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities.Enums;

namespace SK.Entities
{
    [Table(Name = "UserProduct")]
    public partial class UserProduct
    {
        [Column(Name = "ID",IsPrimaryKey = true ,IsDbGenerated = true,IsVersion = true, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        [Column(Name = "PlatformType")]
        public PlatformType PlatformType { get; set; }

        [Column(Name = "ShopID")]
        public int ShopID { get; set; }

        [Column(Name = "TryType")]
        public TryType TryType { get; set; }

        [Column(Name = "OrderMethod")]
        public OrderMethod OrderMethod { get; set; }

        [Column(Name = "Title")]
        public string Title { get; set; }

        [Column(Name = "ProductUrl")]
        public string ProductUrl { get; set; }

        [Column(Name = "MainPic")]
        public string MainPic { get; set; }

        [Column(Name = "Details")]
        public string Details { get; set; }

        [Column(Name = "EnterMethod")]
        public EnterMethod EnterMethod { get; set; }

        [Column(Name = "ProductType")]
        public int ProductType { get; set; }

        [Column(Name = "ProductPrice")]
        public decimal ProductPrice { get; set; }

        [Column(Name = "ProductCount")]
        public int ProductCount { get; set; }

        [Column(Name = "ProductAttr")]
        public string ProductAttr { get; set; }

        [Column(Name = "StartAt")]
        public DateTime StartAt { get; set; }

        [Column(Name = "EndAt")]
        public DateTime EndAt { get; set; }

        [Column(Name = "Bidding")]
        public decimal Bidding { get; set; }

        [Column(Name = "ToUserMessage")]
        public string ToUserMessage { get; set; }

        [Column(Name = "CreateAt")]
        public DateTime CreateAt { get; set; }
    }
}
