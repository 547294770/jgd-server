using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SK.Entities;

namespace SK.Handler
{
    public class BaseHandler : IHttpHandler
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        public virtual bool IsReusable
        {
            get { return true; }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            string[] paths = context.Request.Url.AbsolutePath.Substring(1).Split('/');
            if (paths.Length < 3)
            {
                throw new Exception("请求路径有误");
            }

            string area = paths[0];
            string controller = paths[paths.Length - 2];
            string action = paths[paths.Length - 1];

            Controller = controller;
            Action = action;

            this.OnInit(context);
        }

        protected virtual void OnInit(HttpContext context)
        {
            string[] hosts = new string[] { "localhost", "127.0.0.1" };
            if (hosts.Contains(context.Request.Url.Host)) return;
            CreateAccessToken();
        }

        private static void CreateAccessToken()
        {
            AccessTokenDataContext token = new AccessTokenDataContext();
            var entity = token.AccessToken.FirstOrDefault();


            if (entity != null)
            {
                //微信的AccessToken是2小时过期的
                var ts = entity.Expired - DateTime.Now;
                if (ts <= TimeSpan.Zero)
                {
                    string access_token;
                    int expires_in;
                    GetAccessToken(out access_token, out expires_in);

                    if (!string.IsNullOrEmpty(access_token) && expires_in > 0)
                    {
                        token.AccessToken.DeleteOnSubmit(entity);
                        token.SubmitChanges();

                        entity = new AccessToken();
                        entity.Token = access_token;
                        entity.Expired = DateTime.Now.AddSeconds(expires_in);
                        token.AccessToken.InsertOnSubmit(entity);
                        token.SubmitChanges();
                    }
                }
            }
            else
            {
                string access_token;
                int expires_in;
                GetAccessToken(out access_token, out expires_in);

                if (!string.IsNullOrEmpty(access_token) && expires_in > 0)
                {
                    entity = new AccessToken();
                    entity.Token = access_token;
                    entity.Expired = DateTime.Now.AddSeconds(expires_in);
                    token.AccessToken.InsertOnSubmit(entity);
                    token.SubmitChanges();
                }
            }
        }

        private static void GetAccessToken(out string access_token, out int expires_in)
        {
            try
            {
                WebClient client = new WebClient();

                var appId = System.Configuration.ConfigurationManager.AppSettings["appid"];
                var secret = System.Configuration.ConfigurationManager.AppSettings["secret"];
                var bytes = client.DownloadData(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, secret));
                var data = Encoding.UTF8.GetString(bytes);
                //{"access_token":"ACCESS_TOKEN","expires_in":7200}
                var json = (JObject)JsonConvert.DeserializeObject(data);
                access_token = json["access_token"].Value<string>();
                expires_in = json["expires_in"].Value<int>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                access_token = string.Empty;
                expires_in = 0;
            }
        }
    }
}
