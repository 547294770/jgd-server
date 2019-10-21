using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities;

namespace SK.Common
{
    public class DBContext : DataContext
    {
        public DBContext(IDbConnection connection) : base(connection) {
            var dd = "";
        }
        public DBContext(string connection) : base(connection) { }

        public Table<Admin> Admin;
        public Table<User> User;
        public Table<UserToken> UserToken;
        public Table<UserShop> UserShop;
        public Table<UserProduct> UserProduct;
    }
}
