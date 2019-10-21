using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities.Enums;

namespace SK.Entities
{
    [Table(Name = "UserShop")]
    public partial class UserShop
    {
        [Column(Name = "ID",IsPrimaryKey = true ,IsDbGenerated = true,IsVersion = true, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        [Column(Name = "WangWangAccount")]
        public string WangWangAccount { get; set; }

        [Column(Name = "ShopName")]
        public string ShopName { get; set; }

        [Column(Name = "UserID")]
        public int UserID { get; set; }

        [Column(Name = "CreateAt")]
        public DateTime CreateAt { get; set; }

        [Column(Name = "Status")]
        public bool Status { get; set; }

        [Column(Name = "PlatformType")]
        public PlatformType PlatformType { get; set; }
    }
}
