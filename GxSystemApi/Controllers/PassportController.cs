using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gx.Business;
using Gx.Models.ObjectBiz;
using Gx.Models.ViewModels;
using GxSystemApi.Models;
using GxSystemApi.Models.ViewModels;

namespace GxSystemApi.Controllers
{
    /// <summary>
    /// 用户登录接口
    /// </summary>
    public class PassportController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]PassPortParamter parameter)
        {
            try
            {
                GxSysUserBusiness gxSysUser = new GxSysUserBusiness();
                var entity = gxSysUser.FindGxSysUser().FirstOrDefault(t => t.USERID == parameter.UserId && t.USERPWD == parameter.Pwd);
                if (entity == null)
                {
                    var loginResult = new DataResult<List<ViewUserModel>>();
                    loginResult.ErrorCode = "404";
                    loginResult.Result = false;
                    loginResult.Message = "未找到当前登录用户信息";
                    return Request.CreateResponse(HttpStatusCode.OK, loginResult);
                }
                var dataResult = new DataResult<ViewUserModel>();
                if (entity.USERSTATUS != 10)
                {
                    dataResult.ErrorCode = "500";
                    dataResult.Result = false;
                    dataResult.Message = "当前登录用户已禁止登陆";
                }
                else
                {
                    dataResult.ErrorCode = "200";
                    dataResult.Result = true;
                    dataResult.Message = "当前登录用户登陆成功";
                    ViewUserModel model=new ViewUserModel()
                        {
                            UserId = entity.USERID,
                            UserName = entity.USERNAME,
                            id = entity.ID
                        };
                    dataResult.Data = model;
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
}
