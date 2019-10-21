using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK.Entities;

namespace SK.BL
{
    public class UserBL : BaseBL
    {
        public static UserBL Instance = new UserBL();
        static object lockObj = new object();

        /// <summary>
        /// 根据用户名和密码获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public User GetUserInfo(string userName, string passWord)
        {
            var obj = DBC.User.FirstOrDefault(p => p.UserName == userName && p.PassWord == passWord);
            return obj;
        }

        /// <summary>
        /// 根据UserId获取Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserToken(int userId)
        {
            lock (lockObj)
            {
                var obj = DBC.UserToken.FirstOrDefault(p => p.UserID == userId);
                var userInfo = DBC.User.FirstOrDefault(p => p.ID == userId);
                var tokenString = Guid.NewGuid().ToString("N");
                if (userInfo == null) return string.Empty;

                if (obj == null)
                {
                    var token = new UserToken
                    {
                        UserID = userInfo.ID,
                        Token = tokenString
                    };

                    DBC.UserToken.InsertOnSubmit(token);
                    DBC.SubmitChanges();
                }

                return obj.Token;
            }
        }

        /// <summary>
        /// 根据token获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public User GetUserInfo(string token)
        {
            var obj = DBC.UserToken.FirstOrDefault(p => p.Token == token);
            if (obj == null) return null;

            return DBC.User.FirstOrDefault(p => p.ID == obj.UserID);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public User Login(string userName, string passWord)
        {
            var user = GetUserInfo(userName, passWord);
            if (user != null) {

                var tokenE = DBC.UserToken.FirstOrDefault(p => p.UserID == user.ID);
                if (tokenE != null)
                {
                    tokenE.Token = Guid.NewGuid().ToString("N");
                }
                else
                {
                    UserToken token = new UserToken
                    {
                        UserID = user.ID,
                        Token = Guid.NewGuid().ToString("N")
                    };

                    DBC.UserToken.InsertOnSubmit(token);
                }

                DBC.SubmitChanges();

                return user;
            }

            return null;
        }
    }
}
