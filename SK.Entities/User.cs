using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities.Enums;

namespace SK.Entities
{
    [Table(Name = "User")]
    public partial class User
    {
        [Column(Name = "ID")]
        public string ID { get; set; }

        [Column(Name = "UserName")]
        public string UserName { get; set; }

        [Column(Name = "PassWord")]
        public string PassWord { get; set; }

        [Column(Name = "CreateAt")]
        public DateTime CreateAt { get; set; }

        [Column(Name = "CompanyName")]
        public string CompanyName { get; set; }

        [Column(Name = "CompanyAddress")]
        public string CompanyAddress { get; set; }

        [Column(Name = "Tel")]
        public string Tel { get; set; }

        [Column(Name = "Contact")]
        public string Contact { get; set; }
    }
}
