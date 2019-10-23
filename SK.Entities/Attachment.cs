using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities.Enums;

namespace SK.Entities
{
    [Table(Name = "Attachment")]
    public partial class Attachment
    {
        [Column(Name = "ID",IsPrimaryKey = true)]
        public string ID { get; set; }

        [Column(Name = "SourceID")]
        public string SourceID { get; set; }

        [Column(Name = "FilePath")]
        public string FilePath { get; set; }

        [Column(Name = "FileName")]
        public string FileName { get; set; }

        [Column(Name = "CreateAt")]
        public DateTime CreateAt { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "FileSize")]
        public int FileSize { get; set; }

        [Column(Name = "UpdateAt")]
        public DateTime UpdateAt { get; set; }
    }
}
