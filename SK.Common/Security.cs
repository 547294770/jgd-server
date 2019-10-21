using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SK.Common
{
    public class Security
    {
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(strText);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }

            return byte2String;

        }

        /// <summary>
        /// 混淆加密
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="confused">是否混淆</param>
        /// <returns></returns>
        public static string MD5Encrypt(string strText, bool confused)
        {
            string data = MD5Encrypt(strText);

            if (confused)
            {
                StringBuilder sb = new StringBuilder();
                var newString = data.Reverse();
                foreach (var item in newString)
                    sb.Append(item);

                return string.Format("{0}{1}", sb.ToString().Substring(sb.ToString().Length - 8), sb.ToString().Substring(0, sb.ToString().Length - 8));
            }

            return data;
        }

    }
}
