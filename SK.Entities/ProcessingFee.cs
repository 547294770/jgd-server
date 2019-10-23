using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities.Enums;

namespace SK.Entities
{
    [Table(Name = "ProcessingFee")]
    public partial class ProcessingFee
    {
        [Column(Name = "ID", IsPrimaryKey = true)]
        public string ID { get; set; }

        [Column(Name = "FeeNo")]
        public string FeeNo { get; set; }

        [Column(Name = "SourceID")]
        public string SourceID { get; set; }

        [Column(Name = "Type")]
        public BillType Type { get; set; }

        [Column(Name = "Content")]
        public string Content { get; set; }

        [Column(Name = "CreateAt")]
        public DateTime CreateAt { get; set; }

        [Column(Name = "ProcessingNo")]
        public string ProcessingNo { get; set; }
    }
}
