using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SK.Handler;

namespace SK.User.Controllers
{
    public class Upload : BasePage
    {
        public void upload()
        {
            try
            {
                if (Request.Files.Count < 1)
                {
                    ShowResult(false, "请选择上传文件");
                    return;
                }

                var fileUploaded = Request.Files[0];
                string[] allows = new string[] { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
                string path = ConfigurationManager.AppSettings["UploadPath"];
                string extend = System.IO.Path.GetExtension(fileUploaded.FileName);
                string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), extend);
                string file = System.IO.Path.Combine(path, fileName);
                var size = fileUploaded.ContentLength / (1024 * 1024);
                if (!allows.Contains(extend.ToLower()))
                {
                    ShowResult(false, "上传文件格式不正确");
                    return;
                }

                if (size > 2)
                {
                    ShowResult(false, "上传文件不能超过2M");
                    return;
                }

                fileUploaded.SaveAs(file);

                var returnObj = new
                {
                    code = 0,
                    msg = "成功",
                    data = new {
                        src = "/upload/" + fileName,
                        size = fileUploaded.ContentLength,
                        name = fileUploaded.FileName,
                        file = fileName,
                        createat = DateTime.Now,
                        updateat = DateTime.Now,
                    }
                };

                string json = JsonConvert.SerializeObject(returnObj);
                this.Response.Write(json);
            }
            catch (Exception ex)
            {
                ShowResult(false, ex.Message);
            }
        }
    }
}
