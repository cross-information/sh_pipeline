using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gx.Business;
using Gx.Models;
using Gx.Models.ObjectBiz;
using Gx.Models.ViewModels;

namespace GxSystemApi.Controllers
{
    public class GetGxRjxxController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Parameter parameter)
        {
            GxYsxmxxBusiness gxYsxmxxBusiness = new GxYsxmxxBusiness();
            GxGxxxBusiness gxGxxxBusiness = new GxGxxxBusiness();
            GxRjxxBusiness gxRjxxBusiness = new GxRjxxBusiness();
            try
            {
                var modelsList = new List<GetGxRjxxApiModels>();
                var ysxmxx = gxYsxmxxBusiness.FindEntity();
                if (!string.IsNullOrEmpty(parameter.UserId))
                {
                    ysxmxx = ysxmxx.Where(t => t.SGDWMC == parameter.UserId).ToList();
                }
                if (parameter.Xmgcid > 0)
                {
                    ysxmxx = ysxmxx.Where(t => t.ID == parameter.Xmgcid).ToList();
                }
                foreach (var item in ysxmxx)
                {
                    var entity = new GetGxRjxxApiModels();
                    entity.Htbh = item.HTBH;
                    entity.Id = item.HTBH;
                    entity.Gcmc = item.HTBH;
                    entity.Gcbh= item.HTBH;
                    entity.Sgdwmc = item.SGDWMC;
                    entity.Jldwdm = item.JLDWDM;
                    entity.Yszt = item.YSZT;
                    entity.Htsj = item.HTSJ;

                    List<Gxxx> gxxxList = new List<Gxxx>();
                    var allGxxx = gxGxxxBusiness.FindAllModels().Where(t => t.XMYSXXID == item.ID);
                    foreach (var gxxx in allGxxx)
                    {
                        Gxxx gxxxEntity = new Gxxx();
                        gxxxEntity.Gcbh = gxxx.GCBH;
                        gxxxEntity.Gxwz = gxxx.GXWZ;
                        gxxxEntity.Gxcd = gxxx.GXCD;
                        gxxxEntity.Dxlx = gxxx.DXLX;
                        gxxxEntity.Gxlx = gxxx.GXLX;
                        gxxxEntity.Jszt = gxxx.JSZT;
                        gxxxEntity.Ywlx= gxxx.YWLX;
                        gxxxEntity.Gjz = gxxx.GJZ;
                        gxxxEntity.Xnzc = gxxx.XNZC;
                        gxxxEntity.Gkcl = gxxx.GKCL;
                        gxxxEntity.Syzt = gxxx.SYZT;
                        gxxxEntity.Gkhs = gxxx.GKHS;
                        gxxxEntity.Gkls = gxxx.GKLS;
                        gxxxEntity.Plsx = gxxx.PLSX;
                        gxxxEntity.Gxzt = gxxx.GXZT; 

                        List<Gkxx> gkxxList = new List<Gkxx>();
                        var allGkxx = gxGxxxBusiness.FindAllGkxxModels().Where(t=>t.GXXXID==gxxx.ID);
                        foreach (var gkxx in allGkxx)
                        {
                            Gkxx gkxxEntity = new Gkxx();
                            gkxxEntity.Gkbh = gkxx.GKBH;
                            gkxxEntity.Gkcl= gkxx.GKCL;
                            gkxxEntity.Syzt= gkxx.SYZT; 
                            gkxxEntity.Zkhs = gkxx.ZKHS;
                            gkxxEntity.Zkls = gkxx.ZKLS;
                            gkxxEntity.Zksl = gkxx.ZKSL;
                            gkxxEntity.Gkszh= gkxx.GKSZH;
                            gkxxEntity.Gkszl = gkxx.GKSZL;
                            gkxxList.Add(gkxxEntity);
                        }
                        gxxxEntity.GkxxList = gkxxList;
                        gxxxList.Add(gxxxEntity);
                    }
                    entity.GxxxList = gxxxList;

                    List<Rjxx> listRjxx = new List<Rjxx>();
                    var rjxxList = gxRjxxBusiness.FindAllModels().Where(t => t.XMYSXXID == item.ID);
                    foreach (var rjxx in rjxxList)
                    {
                        Rjxx gxRjxx = new Rjxx();
                        gxRjxx.Gcbh = rjxx.GCBH;
                        gxRjxx.Rjbh = rjxx.RJBH;
                        gxRjxx.Rjmc = rjxx.RJMC;
                        gxRjxx.Gcrjbh = rjxx.GCRJBH;
                        gxRjxx.Jgsl = rjxx.JGSL;
                        gxRjxx.Dxlx = rjxx.DXLX;
                        gxRjxx.Rsjcc= rjxx.RSJCC;
                        gxRjxx.Jglx = rjxx.JGLX;
                        gxRjxx.Gjz = rjxx.GJZ;
                        gxRjxx.Longitude = rjxx.LONGITUDE;
                        gxRjxx.Latitude = rjxx.LATITUDE;
                        gxRjxx.Rjzt = rjxx.RJZT;
                    }
                    entity.RjxxList = listRjxx;
                    modelsList.Add(entity);
                }

                var dataResult = new DataResult<List<GetGxRjxxApiModels>>();
                dataResult.ErrorCode = "200";
                dataResult.Result = true;
                dataResult.Message = "数据返回正常";
                dataResult.Data = modelsList;
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

    public class Parameter
    {
        public decimal Xmgcid { get; set; }

        public string UserId { get; set; }
    }
}
