using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Entities
{
    partial class User
    {
        public enum UserType : byte
        {
            /// <summary>
            /// 会员
            /// </summary>
            User,

            /// <summary>
            /// 商家
            /// </summary>
            Merchant
        }
    }
}
