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
        [Column(Name = "UserID",IsPrimaryKey = true ,IsDbGenerated = true,IsVersion = true, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        [Column(Name = "UserName")]
        public string UserName { get; set; }

        [Column(Name = "PassWord")]
        public string PassWord { get; set; }

        [Column(Name = "CreateAt")]
        public DateTime CreateAt { get; set; }

        [Column(Name = "UserType")]
        public UserType Type { get; set; }
    }
}
