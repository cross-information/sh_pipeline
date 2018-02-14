using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Gx.Business;
using Gx.Models;
using Gx.Models.ObjectBiz;

namespace GxSystemApi.Controllers
{
    public class UploadFileController : ApiController
    {
        private readonly string _uploadUrl = System.Configuration.ConfigurationManager.AppSettings["WsUpload"];

        [HttpPost]
        public HttpResponseMessage Post([FromBody]UploadFile parameter)
        {
            try
            {
                var dataResult = new DataResult<List<GX_PHOTOGALLERY>>();
                GxRjxxBusiness rjxxBusiness = new GxRjxxBusiness();
                var list = rjxxBusiness.FindAllRjxxPhoto().Where(t => t.PARENTID == parameter.rjxxid).ToList();
                if (list.Count <= 0)
                {
                    dataResult.ErrorCode = "404";
                    dataResult.Result = false;
                    dataResult.Message = "未找到人井信息";
                }
                else
                {
                    try
                    {
                        byte[] imageBytes = Convert.FromBase64String(parameter.fileContent);
                        //定义并实例化一个内存流，以存放提交上来的字节数组。  
                        //读入MemoryStream对象  
                        var memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
                        //memoryStream.Write(imageBytes, 0, imageBytes.Length);
                        //二进制转成图片保存  
                        System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream);
                        var uploadUrl = HttpContext.Current.Server.MapPath(_uploadUrl);

                        if (!Directory.Exists(uploadUrl))
                            Directory.CreateDirectory(uploadUrl);

                        var randNum = new Random().Next(10000, 99999);
                        var fileName = "/" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + randNum + "." + parameter.suffix;
                        image.Save(uploadUrl + fileName);

                        dataResult.ErrorCode = "200";
                        dataResult.Result = true;
                        dataResult.Message = "图片上传成功";
                        dataResult.Data = list;
                    }
                    catch (Exception ex)
                    {
                        var msg = "";
                        if (ex.InnerException != null)
                            msg = "图片上传失败：" + ex.InnerException.Message;
                        else
                            msg = "图片上传失败：" + ex.Message;
                        dataResult.ErrorCode = "500";
                        dataResult.Result = true;
                        dataResult.Message = msg;
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, dataResult);
            }
            catch (Exception ex)
            {
                DataResult<string> dataResult = new DataResult<string>();
                dataResult.ErrorCode = "100101";
                dataResult.Result = false;
                dataResult.Message = "获取数据异常：" + (ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                return Request.CreateResponse(HttpStatusCode.OK, dataResult);
            }
        }
    }

    public class UploadFile
    {
        public decimal rjxxid { get; set; }

        public string suffix { get; set; }

        public string fileContent { get; set; }
    }
}
