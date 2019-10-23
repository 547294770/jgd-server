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

        public void message()
        {
            WebClient client = new WebClient();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", "26_L3xwFVomm1rbt4R8IhyYL_yyEaA83zgmXSc9WErV2Yfw7aJoroingP9A_TO_H3QkOrfMAUccjQnGjsfqLkWrxRZvMGEHCl5NqcE8Pcw77oUkfXdYSfuEX-3j68NM3jH7r0WmiDRIvPQDkqCZKGSgABAVRD");
            var dataStr = @"{
           ""touser"":""oNJEyuF1_rkeK9RWOpOu8pmIxRPw"",
           ""template_id"":""YJbgp87C2kWDnARKQ6dsNa6ApkGHlNaEJ6skhJIKhf4"",
           ""url"":""http://weixin.qq.com/download"",         
           ""data"":{
                   ""first"": {
                       ""value"":""恭喜你购买成功！"",
                       ""color"":""#173177""
                   },
                   ""tradeDateTime"":{
                       ""value"":""2019-10-23"",
                       ""color"":""#173177""
                   },
                   ""orderType"": {
                       ""value"":""加工单"",
                       ""color"":""#173177""
                   },
                   ""customerInfo"": {
                       ""value"":""小秋测试"",
                       ""color"":""#173177""
                   },
                   ""orderItemName"":{
                       ""value"":""2000001"",
                       ""color"":""#173177""
                   },
                   ""orderItemData"":{
                       ""value"":""不锈钢管材"",
                       ""color"":""#173177""
                   },
                   ""remark"":{
                       ""value"":""模板消息测试备注内容"",
                       ""color"":""#173177""
                   }
           }
       }";
            var bytes = Encoding.UTF8.GetBytes(dataStr);
            var byteResult = client.UploadData(url, bytes);
            var data = Encoding.UTF8.GetString(byteResult);
            this.ShowResult(true, "成功", data);
        }
       
    }
}
