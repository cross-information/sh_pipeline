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
    public class CoordinatesController : ApiController
    {
        /// <summary>
        /// 修改人井信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]CoordinatesModel parameter)
        {
            try
            {
                GxRjxxBusiness gxRjxxBusiness = new GxRjxxBusiness();
                var entity = gxRjxxBusiness.Find(parameter.Id);
                if (entity == null)
                {
                    var loginResult = new DataResult<string>();
                    loginResult.ErrorCode = "404";
                    loginResult.Result = false;
                    loginResult.Message = "未找到人井信息";
                    return Request.CreateResponse(HttpStatusCode.OK, loginResult);
                }
                entity.LONGITUDE = parameter.Longitude;
                entity.LATITUDE = parameter.Latitude;
                entity.MODIFYBY = "接口数修改";
                entity.MODIFYTIME = DateTime.Now;
                gxRjxxBusiness.Update(entity);

                var dataResult = new DataResult<string>();
                if (gxRjxxBusiness.SaveChange() > 0)
                {
                    dataResult.ErrorCode = "200";
                    dataResult.Result = true;
                    dataResult.Message = "人井坐标信息修改成功";
                    dataResult.Data = "";
                    return Request.CreateResponse(HttpStatusCode.OK, dataResult);
                }
                else
                {
                    dataResult.ErrorCode = "500";
                    dataResult.Result = false;
                    dataResult.Message = "人井坐标信息修改失败";
                    dataResult.Data = "";
                    return Request.CreateResponse(HttpStatusCode.OK, dataResult);
                }
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
}
