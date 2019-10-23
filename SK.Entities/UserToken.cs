using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Entities
{
    [Table(Name = "UserToken")]
    public partial class UserToken
    {
        [Column(Name = "UserID", IsPrimaryKey = true)]
        public string UserID { get; set; }

        [Column(Name = "Token")]
        public string Token { get; set; }
    }
}
