using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SK.Entities;

namespace SK.BL
{
    public class WXTemplateBL : BaseBL
    {
        private const string TempIDNewOrder = "YJbgp87C2kWDnARKQ6dsNa6ApkGHlNaEJ6skhJIKhf4";
        private const string TempIDPickUp = "p1EoBjVmiOqLAuNS-Lq1yya5BqiwRVgpYm1WOTo2Xkc";
        private const string TempIDDelivery = "YSqd7L_p60EI8TOEJ6cSXFnMF-Pdz144mWlElee2cQ0";
        private const string TempIDInLib = "wNdKApbYP0HksQVFwo9UxSw2bedCC9WQjDVzG2Rmxmk";
        private const string TempIDOutLib = "5ylDWkRVhP37rIW0zjxDBb_Msh2SPEj9US_TbsNtV60";
        

        public static bool SendMessage(string tplPath, Dictionary<string, string> dict)
        {
            WebClient client = new WebClient();

            AccessTokenDataContext cxt = new AccessTokenDataContext();
            var token = cxt.AccessToken.FirstOrDefault();
            if (token != null)
            {
                try
                {
                    var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", token.Token);
                    var dataStr = File.ReadAllText(tplPath);

                    foreach (var key in dict.Keys)
                    {
                        dataStr = dataStr.Replace("{" + key + "}", dict[key]);
                    }

                    var bytes = Encoding.UTF8.GetBytes(dataStr);
                    var byteResult = client.UploadData(url, bytes);
                    var data = Encoding.UTF8.GetString(byteResult);

                    //{
                    //  "errcode":0,
                    //   "errmsg":"ok",
                    //   "msgid":200228332
                    //}

                    var json = (JObject)JsonConvert.DeserializeObject(data);
                    var errcode = json["errcode"].Value<int>();
                    var errmsg = json["errmsg"].Value<string>();

                    if (errcode == 0)
                    {
                        return true;
                    }
                }
                catch
                {
                    
                }
            }

            return false;
        }

        public static void SendMessageForNewOrder(string tplPath, 
            string url,
            string first, 
            string customerInfo, 
            string orderItemName, 
            string orderItemData, 
            string remark)
        {
            ToUsersDataContext cxt = new ToUsersDataContext();

            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("touser", "");
            dict.Add("template_id", TempIDNewOrder);
            dict.Add("url", url);
            dict.Add("first", first);
            dict.Add("tradeDateTime", DateTime.Now.ToString());
            dict.Add("orderType", "加工单");
            dict.Add("customerInfo", customerInfo);
            dict.Add("orderItemName", orderItemName);
            dict.Add("orderItemData", orderItemData);
            dict.Add("remark", remark);

            foreach (var item in cxt.ToUsers.Where(p => p.TemplateID == TempIDNewOrder))
            {
                dict["touser"] = item.OpenID;
                WXTemplateBL.SendMessage(tplPath, dict);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 提货通知
        /// </summary>
        /// <param name="touser"></param>
        /// <param name="tplPath"></param>
        /// <param name="url"></param>
        /// <param name="first"></param>
        /// <param name="keyword1"></param>
        /// <param name="keyword2"></param>
        /// <param name="keyword3"></param>
        /// <param name="keyword4"></param>
        /// <param name="keyword5"></param>
        /// <param name="remark"></param>
        public static void SendMessageForPickUp(string touser,string tplPath,
            string url,
            string first,
            string keyword1,
            string keyword2,
            string keyword3,
            string keyword4,
            string keyword5,
            string remark)
        {

            /*
             * 
             * {{first.DATA}}
提货单号：{{keyword1.DATA}}
提货人：{{keyword2.DATA}}
提货人电话：{{keyword3.DATA}}
提货车牌：{{keyword4.DATA}}
预计提货时间：{{keyword5.DATA}}
{{remark.DATA}}
             * */


            ToUsersDataContext cxt = new ToUsersDataContext();

            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("touser", touser);
            dict.Add("template_id", TempIDPickUp);
            dict.Add("url", url);
            dict.Add("first", first);
            dict.Add("keyword1", keyword1);
            dict.Add("keyword2", keyword2);
            dict.Add("keyword3", keyword3);
            dict.Add("keyword4", keyword4);
            dict.Add("keyword5", keyword5);
            dict.Add("remark", remark);

            WXTemplateBL.SendMessage(tplPath, dict);

            //foreach (var item in cxt.ToUsers.Where(p => p.TemplateID == TempIDPickUp))
            //{
            //    dict["touser"] = item.OpenID;
            //    WXTemplateBL.SendMessage(tplPath, dict);
            //    Thread.Sleep(100);
            //}
        }

        /// <summary>
        /// 送货通知
        /// </summary>
        /// <param name="tplPath"></param>
        /// <param name="url"></param>
        /// <param name="first"></param>
        /// <param name="keyword1"></param>
        /// <param name="keyword2"></param>
        /// <param name="keyword3"></param>
        /// <param name="keyword4"></param>
        /// <param name="keyword5"></param>
        /// <param name="remark"></param>
        public static void SendMessageForDelivery(string tplPath,
            string url,
            string first,
            string keyword1,
            string keyword2,
            string keyword3,
            string keyword4,
            string keyword5,
            string remark)
        {

            //{{first.DATA}}
            //送货单号：{{keyword1.DATA}}
            //送货人：{{keyword2.DATA}}
            //送货人电话：{{keyword3.DATA}}
            //送货车牌：{{keyword4.DATA}}
            //预计到达时间：{{keyword5.DATA}}
            //{{remark.DATA}}

            ToUsersDataContext cxt = new ToUsersDataContext();

            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("touser", "");
            dict.Add("template_id", TempIDDelivery);
            dict.Add("url", url);
            dict.Add("first", first);
            dict.Add("keyword1", keyword1);
            dict.Add("keyword2", keyword2);
            dict.Add("keyword3", keyword3);
            dict.Add("keyword4", keyword4);
            dict.Add("keyword5", keyword5);
            dict.Add("remark", remark);

            foreach (var item in cxt.ToUsers.Where(p => p.TemplateID == TempIDDelivery))
            {
                dict["touser"] = item.OpenID;
                WXTemplateBL.SendMessage(tplPath, dict);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 入库通知
        /// </summary>
        /// <param name="touser"></param>
        /// <param name="tplPath"></param>
        /// <param name="url"></param>
        /// <param name="first"></param>
        /// <param name="keyword1"></param>
        /// <param name="keyword2"></param>
        /// <param name="keyword3"></param>
        /// <param name="remark"></param>
        public static void SendMessageForInLib(string touser,string tplPath,
            string url,
            string first,
            string keyword1,
            string keyword2,
            string keyword3,
            string remark)
        {
            /*
             * 
             * 尊敬的用户，您有新的产品入库
商品名称：电饭锅
入库数量：10个
入库时间：2016-7-28
请注意查看
             * */

            ToUsersDataContext cxt = new ToUsersDataContext();

            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("touser", touser);
            dict.Add("template_id", TempIDInLib);
            dict.Add("url", url);
            dict.Add("first", first);
            dict.Add("keyword1", keyword1);
            dict.Add("keyword2", keyword2);
            dict.Add("keyword3", keyword3);
            dict.Add("remark", remark);

            WXTemplateBL.SendMessage(tplPath, dict);

            //foreach (var item in cxt.ToUsers.Where(p => p.TemplateID == TempIDInLib))
            //{
            //    dict["touser"] = item.OpenID;
            //    WXTemplateBL.SendMessage(tplPath, dict);
            //    Thread.Sleep(100);
            //}
        }

        /// <summary>
        /// 出库通知
        /// </summary>
        /// <param name="touser"></param>
        /// <param name="tplPath"></param>
        /// <param name="url"></param>
        /// <param name="first"></param>
        /// <param name="keyword1">出库单号</param>
        /// <param name="keyword2">出库日期</param>
        /// <param name="keyword3">客户名称</param>
        /// <param name="keyword4">发货仓库</param>
        /// <param name="remark">备注信息</param>
        public static void SendMessageForOutLib(string touser,string tplPath,
            string url,
            string first,
            string keyword1,
            string keyword2,
            string keyword3,
            string keyword4,
            string remark)
        {
            /*
             * 
             * 
             * {{first.DATA}}
出库单号：{{keyword1.DATA}}
出库日期：{{keyword2.DATA}}
客户名称：{{keyword3.DATA}}
发货仓库：{{keyword4.DATA}}
{{remark.DATA}} */


            ToUsersDataContext cxt = new ToUsersDataContext();

            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("touser", touser);
            dict.Add("template_id", TempIDOutLib);
            dict.Add("url", url);
            dict.Add("first", first);
            dict.Add("keyword1", keyword1);
            dict.Add("keyword2", keyword2);
            dict.Add("keyword3", keyword3);
            dict.Add("keyword4", keyword4);
            dict.Add("remark", remark);

            WXTemplateBL.SendMessage(tplPath, dict);

            //foreach (var item in cxt.ToUsers.Where(p => p.TemplateID == TempIDOutLib))
            //{
            //    dict["touser"] = item.OpenID;
            //    WXTemplateBL.SendMessage(tplPath, dict);
            //    Thread.Sleep(100);
            //}
        }
    }
}
