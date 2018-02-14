using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gx.Business;
using Gx.Models;
using Gx.Models.ObjectBiz;
using GxSystemApi.Models;
using GxSystemApi.Models.ViewModels;

namespace GxSystemApi.Controllers
{
    public class GetRjxxPicController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]RjPhoto parameter)
        {
            try
            {
                GxRjxxBusiness rjxxBusiness = new GxRjxxBusiness();
                var lsit = rjxxBusiness.FindAllRjxxPhoto().Where(t => t.PARENTID == parameter.rjxxid).ToList();

                var dataResult = new DataResult<List<GX_PHOTOGALLERY>>();

                dataResult.ErrorCode = "200";
                dataResult.Result = true;
                dataResult.Message = "数据返回正常";
                dataResult.Data = lsit;

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

    public class RjPhoto
    {
        public decimal rjxxid { get; set; }
    }
}
