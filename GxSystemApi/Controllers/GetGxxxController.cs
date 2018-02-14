using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gx.Models.ObjectBiz;
using Gx.Models.ViewModels;

namespace GxSystemApi.Controllers
{
    public class GetGxxxController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] GxxxParameter parameter)
        {
            try
            {
                List<Gxxx> listGxxx = new List<Gxxx>();
                Gxxx gxxx = new Gxxx();
                gxxx.Gcbh = "1111";

                List<Gkxx> gkxxList = new List<Gkxx>();
                Gkxx gkxx = new Gkxx();
                gkxx.Gkbh = "111";
                gkxxList.Add(gkxx);
                 
                gxxx.GkxxList = gkxxList;
                listGxxx.Add(gxxx);

                var dataResult = new DataResult<List<Gxxx>>();
                dataResult.ErrorCode = "200";
                dataResult.Result = true;
                dataResult.Message = "数据返回正常";
                dataResult.Data = listGxxx;
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

    public class GxxxParameter
    {
        public decimal Gxid { get; set; }
    }
}
