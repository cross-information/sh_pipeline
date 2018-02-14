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
    public class GetRjxxController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] RjxxParameter parameter)
        {
            try
            {
                Rjxx rjxx = new Rjxx();
                rjxx.Dxlx = "11";

                List<Rjxx> listRjxx = new List<Rjxx>();
                listRjxx.Add(rjxx);

                var dataResult = new DataResult<List<Rjxx>>();
                dataResult.ErrorCode = "200";
                dataResult.Result = true;
                dataResult.Message = "数据返回正常";
                dataResult.Data = listRjxx;
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

    public class RjxxParameter
    {
        public decimal Rjid { get; set; }
    }
}
