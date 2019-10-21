using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Entities
{
    [Table(Name = "Admin")]
    public class Admin
    {
        [Column(Name = "AdminID", IsPrimaryKey = true, AutoSync = AutoSync.OnInsert, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "PassWord")]
        public string PassWord { get; set; }

        [Column(Name = "NickName")]
        public string NickName { get; set; }

        [Column(Name = "CreateAt")]
        public DateTime CreateAt { get; set; }
    }
}
