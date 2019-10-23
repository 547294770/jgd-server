using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities.Enums;

namespace SK.Entities
{
    [Table(Name = "PickUpOrder")]
    public partial class PickUpOrder
    {
        [Column(Name = "ID", IsPrimaryKey = true)]
        public string ID { get; set; }

        [Column(Name = "SourceID")]
        public string SourceID { get; set; }

        [Column(Name = "Type")]
        public OrderType Type { get; set; }

        [Column(Name = "Content")]
        public string Content { get; set; }

        [Column(Name = "PickUpAt")]
        public DateTime PickUpAt { get; set; }

        [Column(Name = "VehicleInfo")]
        public string VehicleInfo { get; set; }

        [Column(Name = "UserID")]
        public string UserID { get; set; }

        [Column(Name = "UserName")]
        public string UserName { get; set; }

        [Column(Name = "OrderNo")]
        public string OrderNo { get; set; }

        [Column(Name = "ProcessingNo")]
        public string ProcessingNo { get; set; }

        [Column(Name = "CreateAt")]
        public DateTime CreateAt { get; set; }
    }
}
