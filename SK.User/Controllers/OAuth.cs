using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SK.Handler;
using SK.Common;
using SK.BL;
using System.Web;
using SK.Common.Extentions;
using SK.Entities.Enums;
using System.ComponentModel;
using SK.Entities;
using System.Net;
using Newtonsoft.Json.Linq;
using SK.Common.Caches;

namespace SK.User.Controllers
{
    public class OAuth : BasePage
    {
        private static bool isAccess = false;

        public void userinfo()
        {
            
            string path = this.Context.Server.MapPath("/log.txt");

            string code = this.Context.Request["code"];
            string state = this.Context.Request["state"];

            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
            {
                this.ShowResult(false, "获取code失败");
                return;
            }

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path, true))
            {
                sw.WriteLine(string.Format("date:{0},code : {1}", DateTime.Now, code));
            }

            string appid = System.Configuration.ConfigurationManager.AppSettings["appid"];
            string secret = System.Configuration.ConfigurationManager.AppSettings["secret"];

            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
            WebClient client = new WebClient();
            byte[] dataBytes = client.DownloadData(url);
            string data = Encoding.UTF8.GetString(dataBytes);

            //            {
            //  "access_token":"ACCESS_TOKEN",
            //  "expires_in":7200,
            //  "refresh_token":"REFRESH_TOKEN",
            //  "openid":"OPENID",
            //  "scope":"SCOPE" 
            //}

            if (data.IndexOf("errcode") > -1)
            {
                this.ShowResult(false, "获取ACCESS_TOKEN失败，元数据：" + data);
                return;
            }

            var jObject = (JObject)JsonConvert.DeserializeObject(data);
            var access_token = jObject["access_token"] == null ? "" : jObject["access_token"].Value<string>();
            var refresh_token = jObject["refresh_token"] == null ? "" : jObject["refresh_token"].Value<string>();
            var openid = jObject["openid"] == null ? "" : jObject["openid"].Value<string>();
            var scope = jObject["scope"] == null ? "" : jObject["scope"].Value<string>();
            var expires_in = jObject["expires_in"] == null ? 0 : jObject["expires_in"].Value<int>();

            url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", access_token, openid);
            client = new WebClient();
            dataBytes = client.DownloadData(url);
            data = Encoding.UTF8.GetString(dataBytes);


            //            {   
            //  "openid":" OPENID",
            //  " nickname": NICKNAME,
            //  "sex":"1",
            //  "province":"PROVINCE"
            //  "city":"CITY",
            //  "country":"COUNTRY",
            //  "headimgurl":       "http://thirdwx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46",
            //  "privilege":[ "PRIVILEGE1" "PRIVILEGE2"     ],
            //  "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
            //}
            if (data.IndexOf("errcode") > -1)
            {
                this.ShowResult(false, "获取用户信息失败，元数据：" + data);
                return;
            }

            jObject = (JObject)JsonConvert.DeserializeObject(data);
            openid = jObject["openid"] == null ? "" : jObject["openid"].Value<string>();
            var nickname = jObject["nickname"] == null ? "" : jObject["nickname"].Value<string>();
            var sex = jObject["sex"] == null ? 1 : jObject["sex"].Value<int>();
            var province = jObject["province"] == null ? "" : jObject["province"].Value<string>();
            var city = jObject["city"] == null ? "" : jObject["city"].Value<string>();
            var country = jObject["country"] == null ? "" : jObject["country"].Value<string>();
            var headimgurl = jObject["headimgurl"] == null ? "" : jObject["headimgurl"].Value<string>();
            var privilege = "";// jObject["privilege"].Value<string>();
            var unionid = jObject["unionid"] == null ? "" : jObject["unionid"].Value<string>();

            WXUserDataContext cxt = new WXUserDataContext();

            var usrinfo = cxt.WXUser.FirstOrDefault(p => p.openid == openid);
            if (usrinfo != null) {
                RecordUserInfo(usrinfo);
                return;
            }

            WXUser uEnity = new WXUser
            {
                city = city,
                country = country,
                headimgurl = headimgurl,
                nickname = nickname,
                openid = openid,
                privilege = privilege,
                province = province,
                sex = sex,
                unionid = unionid
            };

            cxt.WXUser.InsertOnSubmit(uEnity);
            cxt.SubmitChanges();

            RecordUserInfo(uEnity);
        }

        private void RecordUserInfo(WXUser uEnity)
        {
            //记录登录信息
            var cookie = new System.Web.HttpCookie(Consts.USER_INFO);
            cookie.Value = uEnity.openid;
            UserCache.AddUser(cookie.Value, uEnity);
            Response.Cookies.Add(cookie);
            this.Context.Items[Consts.USER_INFO] = uEnity;

            this.Context.Response.Redirect("/dist/");
        }
    }
}
