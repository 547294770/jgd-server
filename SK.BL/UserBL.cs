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
            UserDataContext dc = new UserDataContext();
            var obj = dc.User.FirstOrDefault(p => p.UserName == userName && p.PassWord == passWord);
            return obj;
        }

        /// <summary>
        /// 根据UserId获取Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserToken(string userId)
        {
            UserDataContext dc = new UserDataContext();
            UserTokenDataContext UserTokenDc = new UserTokenDataContext();

            lock (lockObj)
            {
                var obj = UserTokenDc.UserToken.FirstOrDefault(p => p.UserID == userId);
                var userInfo = dc.User.FirstOrDefault(p => p.ID == userId);
                var tokenString = Guid.NewGuid().ToString("N");
                if (userInfo == null) return string.Empty;

                if (obj == null)
                {
                    var token = new UserToken
                    {
                        UserID = userInfo.ID,
                        Token = tokenString
                    };

                    UserTokenDc.UserToken.InsertOnSubmit(token);
                    UserTokenDc.SubmitChanges();
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
            UserTokenDataContext UserTokenDc = new UserTokenDataContext();
            UserDataContext dc = new UserDataContext();

            var obj = UserTokenDc.UserToken.FirstOrDefault(p => p.Token == token);
            if (obj == null) return null;

            return dc.User.FirstOrDefault(p => p.ID == obj.UserID);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public User Login(string userName, string passWord)
        {
            UserTokenDataContext UserTokenDc = new UserTokenDataContext();

            var user = GetUserInfo(userName, passWord);
            if (user != null) {

                var tokenE = UserTokenDc.UserToken.FirstOrDefault(p => p.UserID == user.ID);
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

                    UserTokenDc.UserToken.InsertOnSubmit(token);
                }

                UserTokenDc.SubmitChanges();

                return user;
            }

            return null;
        }

        /// <summary>
        /// 根据用户ID获取公司信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Company GetCompany(string userId)
        {
            CompanyDataContext cxt = new CompanyDataContext();
            var entity =  cxt.Company.FirstOrDefault(p => p.UserID == userId);
            return entity;
        }
    }
}
