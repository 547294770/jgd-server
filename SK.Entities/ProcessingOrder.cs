using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities.Enums;

namespace SK.Entities
{
    [Table(Name = "ProcessingOrder")]
    public partial class ProcessingOrder
    {
        [Column(Name = "ID", IsPrimaryKey = true)]
        public string ID { get; set; }

        [Column(Name = "OrderNo")]
        public string OrderNo { get; set; }

        [Column(Name = "Content")]
        public string Content { get; set; }

        [Column(Name = "CreateAt")]
        public DateTime CreateAt { get; set; }

        [Column(Name = "DelType")]
        public DeliveryType DelType { get; set; }

        [Column(Name = "PickType")]
        public PickUpType PickType { get; set; }

        [Column(Name = "Status")]
        public OrderStatus Status { get; set; }

        [Column(Name = "UserID")]
        public string UserID { get; set; }

        [Column(Name = "UserName")]
        public string UserName { get; set; }

        [Column(Name = "UpdateAt")]
        public DateTime UpdateAt { get; set; }
    }
}
