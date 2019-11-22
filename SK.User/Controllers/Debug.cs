using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SK.Handler;
using SK.Common.Extentions;
using Newtonsoft.Json;
using SK.Entities;
using System.Net;
using SK.BL;
using SK.Common;

namespace SK.User.Controllers
{
    public class Debug : BasePage
    {
        public void token()
        {
            WebClient client = new WebClient();
            var bytes = client.DownloadData("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxcee2bf962b1ef8f3&secret=0e9f64b8ca87d8671f0d8b3ea311e2ab");

            var data = Encoding.UTF8.GetString(bytes);
            this.ShowResult(true, "成功", data);
        }

        public void message1()
        {
            string title = string.Format("{0}，您有一个提货信息", "小秋");
            string tplPath = this.Context.Server.MapPath("/content/templates/提货通知.json");
            WXTemplateBL.SendMessageForPickUp(
                "oNJEyuF1_rkeK9RWOpOu8pmIxRPw",
                tplPath,
                Config.Setting.WXWebHost + "/dist/#/Pages/JgdDetail?ID=eeceeb19-b76f-4731-a339-00c8d10cd0dc",
                title,
                "",
                "",
               "",
               "",
               "",
               string.Format("加工单：{0}已加工完毕，请贵司安排提货。", "20191123014311"));
        }

        public void message()
        {
            //string path = this.Context.Server.MapPath("/content/templates/新订单通知.json");
            //Dictionary<string,string> dict= new Dictionary<string,string>();

            //dict.Add("touser", "oNJEyuF1_rkeK9RWOpOu8pmIxRPw");
            //dict.Add("url", "http://weixin.qq.com/download");
            //dict.Add("first", "恭喜你购买成功！");
            //dict.Add("tradeDateTime", "2019-10-23");
            //dict.Add("orderType", "加工单");
            //dict.Add("customerInfo", "小秋测试111");
            //dict.Add("orderItemName", "20191027");
            //dict.Add("orderItemData", "不锈钢管材");
            //dict.Add("remark", "模板消息测试备注内容");

            //var result =   WXTemplateBL.SendMessage(path, dict);

            //string tplPath = this.Context.Server.MapPath("/content/templates/送货通知.json");
            ////WXTemplateBL.SendMessageForNewOrder(tplPath,
            ////    "",
            ////    "新订单通知",
            ////    "test1",
            ////    "加工单",
            ////   "20191028111",
            ////   "模板消息测试备注内容");

            ////{{first.DATA}}
            ////送货单号：{{keyword1.DATA}}
            ////送货人：{{keyword2.DATA}}
            ////送货人电话：{{keyword3.DATA}}
            ////送货车牌：{{keyword4.DATA}}
            ////预计到达时间：{{keyword5.DATA}}
            ////{{remark.DATA}}


            //WXTemplateBL.SendMessageForDelivery(tplPath,
            //    "",
            //    "送货通知",
            //    "20191028001",
            //    "张先生",
            //   "13987654321",
            //   "粤B.394900",
            //   "20191029",
            //   "555555555555");

            //string tplPath = this.Context.Server.MapPath("/content/templates/提货通知.json");

            //WXTemplateBL.SendMessageForDelivery(tplPath,
            //    "",
            //    "提货通知",
            //    "20191028001",
            //    "邱先生",
            //   "13987654321",
            //   "粤B.394900",
            //   "20191029",
            //   "提货测试");

            //string tplPath = this.Context.Server.MapPath("/content/templates/商品入库通知.json");

            //WXTemplateBL.SendMessageForInLib(tplPath,
            //    "",
            //    "入库通知",
            //    "20191028001",
            //    "邱先生",
            //   "13987654321",
            //   "入库通知测试");

            string tplPath = this.Context.Server.MapPath("/content/templates/出库提醒.json");

            //WXTemplateBL.SendMessageForOutLib(tplPath,
            //    "",
            //    "出库提醒",
            //    "20191028001",
            //    "邱先生",
            //   "13987654321",
            //   "13987654321",
            //   "出库提醒测试");


            this.ShowResult(true, "成功", "ok");

//            WebClient client = new WebClient();
//            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", "26_L3xwFVomm1rbt4R8IhyYL_yyEaA83zgmXSc9WErV2Yfw7aJoroingP9A_TO_H3QkOrfMAUccjQnGjsfqLkWrxRZvMGEHCl5NqcE8Pcw77oUkfXdYSfuEX-3j68NM3jH7r0WmiDRIvPQDkqCZKGSgABAVRD");
//            var dataStr = @"{
//           ""touser"":""oNJEyuF1_rkeK9RWOpOu8pmIxRPw"",
//           ""template_id"":""YJbgp87C2kWDnARKQ6dsNa6ApkGHlNaEJ6skhJIKhf4"",
//           ""url"":""http://weixin.qq.com/download"",         
//           ""data"":{
//                   ""first"": {
//                       ""value"":""恭喜你购买成功！"",
//                       ""color"":""#173177""
//                   },
//                   ""tradeDateTime"":{
//                       ""value"":""2019-10-23"",
//                       ""color"":""#173177""
//                   },
//                   ""orderType"": {
//                       ""value"":""加工单"",
//                       ""color"":""#173177""
//                   },
//                   ""customerInfo"": {
//                       ""value"":""小秋测试"",
//                       ""color"":""#173177""
//                   },
//                   ""orderItemName"":{
//                       ""value"":""2000001"",
//                       ""color"":""#173177""
//                   },
//                   ""orderItemData"":{
//                       ""value"":""不锈钢管材"",
//                       ""color"":""#173177""
//                   },
//                   ""remark"":{
//                       ""value"":""模板消息测试备注内容"",
//                       ""color"":""#173177""
//                   }
//           }
//       }";
//            var bytes = Encoding.UTF8.GetBytes(dataStr);
//            var byteResult = client.UploadData(url, bytes);
//            var data = Encoding.UTF8.GetString(byteResult);
            //this.ShowResult(true, "成功", data);
        }

        public void cookie1()
        {
            var cookie = new System.Web.HttpCookie(Consts.USER_INFO);
            cookie.Expires = DateTime.Now.AddHours(-1);
            Context.Response.Cookies.Add(cookie);
            Context.Response.Write("1111");
        }

        public void cookie2()
        {
            var cookie = Context.Request.Cookies[Consts.USER_INFO];

            Context.Response.Write("cookie" + cookie);
            Context.Response.End();
        }

        public void host()
        {
            Context.Response.Write("hostname:" + Request.Url.Host);
            Context.Response.End();
        }

        public void neworder()
        {
            ProcessingOrderDataContext cxt = new ProcessingOrderDataContext();

            var order =  cxt.ProcessingOrder.FirstOrDefault(p => p.ID == "c2e7d720-9775-4b88-9dc2-c00cb52dd639");
            if (order == null) {
                this.FailMessage("订单为空");
                return;
            }
            

            string tplPath = this.Context.Server.MapPath("/content/templates/新订单通知.json");
            WXTemplateBL.SendMessageForNewOrder(tplPath,
                Config.Setting.WXWebHost + "/dist/#/Pages/JgdInfo?ID=" + order.ID,
                "新订单通知",
                order.UserName,
                "加工单",
               order.OrderNo,
               order.Content);

        }

    }
}
