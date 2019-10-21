using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Entities
{
    [Table(Name = "SysProductType")]
    public partial class ProductType
    {
        [Column(Name = "ID", IsPrimaryKey = true, IsVersion=true, AutoSync = AutoSync.OnInsert, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "Enabled")]
        public bool Enabled { get; set; }
    }
}
