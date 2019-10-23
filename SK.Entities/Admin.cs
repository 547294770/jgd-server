using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities.Enums;

namespace SK.Entities
{
    [Table(Name = "Admin")]
    public partial class Admin
    {
        [Column(Name = "ID")]
        public string ID { get; set; }

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
