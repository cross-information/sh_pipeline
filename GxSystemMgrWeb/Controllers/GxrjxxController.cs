using Gx.Business;
using Gx.CommonUnits;
using Gx.CommonUnits.Tools;
using Gx.Models;
using JGB.Common.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GxSystemMgrWeb.Controllers
{
    public class GxrjxxController : BaseController
    {
        private GxRjxxBusiness gxXlzxBusiness = new GxRjxxBusiness();
        private GxPhotogalleryBusiness gxPhotogalleryBusiness = new GxPhotogalleryBusiness();
        private GxYsxmxxBusiness gxYsxmxxBusiness = new GxYsxmxxBusiness();
        private GxSysUserBusiness gxSysUserBusiness = new GxSysUserBusiness();

        public PartialViewResult Index()
        {
            ViewData["gcxmid"] = ConvertUtility.ToDecimal(Request["gcxmid"]);
            var model = gxYsxmxxBusiness.FindEntity(ConvertUtility.ToDecimal(Request["gcxmid"]));
            ViewData["gcbh"] = model == null ? "" : model.GCBH;
            ViewData["gcmc"] = model == null ? "" : model.GCBH;
            var userModel = gxSysUserBusiness.FindGxSysUser(ConvertUtility.ToDecimal(model == null ? "0" : model.SGDWMC));
            ViewData["sgdwmc"] = userModel == null ? "" : userModel.USERNAME;
            return PartialView();
        }

        public ActionResult List(int page = 1, int rows = 15)
        {
            int reacordCount = 0;
            var gcxmid = ConvertUtility.ToDecimal(Request["gcxmid"]);
            var rxbh = ConvertUtility.ToString(Request["rxbh"]);
            var list = gxXlzxBusiness.FindAllGXRJXX(gcxmid, rxbh, page, rows, out reacordCount);
            return Json(new { total = reacordCount, rows = list });
        }

        public PartialViewResult Add()
        {
            ViewData["gcxmid"] = ConvertUtility.ToDecimal(Request["gcxmid"]);
            var model = gxYsxmxxBusiness.FindEntity(ConvertUtility.ToDecimal(Request["gcxmid"]));
            ViewData["gcbh"] = model == null ? "" : model.GCBH;
            ViewData["gcmc"] = model == null ? "" : model.GCBH;
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(FormCollection form, GX_RJXX gxrjxx)
        {
            try
            {
                gxrjxx.ID = new Common().GetRandom();
                gxrjxx.RJBH = new Common().GetRandom().ToString();
                gxrjxx.RJZT = "1";
                gxrjxx.XMYSXXID = ConvertUtility.ToDecimal(form["Id"]);
                gxrjxx.GCBH = form["gcbh"];
                gxrjxx.RJMC = form["RJMC"];
                gxrjxx.GJZ = gxrjxx.GCBH + "_" + form["GCRJBH"];
                gxrjxx.GCRJBH = form["GCRJBH"];
                gxrjxx.JGSL = ConvertUtility.ToInt(form["JGSL"]);
                gxrjxx.DXLX = form["DXLX"];
                gxrjxx.RSJCC = form["RSJCC"];
                gxrjxx.DXLX = form["DXLX"];
                gxrjxx.LONGITUDE = form["LONGITUDE"];
                gxrjxx.LATITUDE = form["LATITUDE"];
                gxrjxx.CREATEBY = CurrentUser.UserName;
                gxrjxx.CREATETIME = DateTime.Now;
                gxXlzxBusiness.Add(gxrjxx);
                return Json(gxXlzxBusiness.SaveChange() > 0 ? AjaxResult.Success("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
            }
            catch (Exception ex) {
                return Json(AjaxResult.Error("数据操作失败！"));
            }
        }

        public PartialViewResult Edit()
        {
            var model = gxXlzxBusiness.Find(ConvertUtility.ToDecimal(Request["id"]));
            var ysxmxxModel = gxYsxmxxBusiness.FindEntity(ConvertUtility.ToDecimal(model.XMYSXXID));
            ViewData["gcbh"] = ysxmxxModel == null ? "" : ysxmxxModel.GCBH;
            ViewData["gcmc"] = ysxmxxModel == null ? "" : ysxmxxModel.GCBH;
            ViewData["photoGrally"] = gxPhotogalleryBusiness.Find(ConvertUtility.ToDecimal(Request["id"]));
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, GX_RJXX gxrjxx)
        {
            decimal id = ConvertUtility.ToDecimal(form["Id"]);
            var model = gxXlzxBusiness.Find(id);
            if (model == null)
                return Json(AjaxResult.Error("未找到需要更新的数据！"));
            gxrjxx = model;
            gxrjxx.RJMC = form["RJMC"];
            gxrjxx.GCRJBH = form["GCRJBH"];
            gxrjxx.JGSL = ConvertUtility.ToInt(form["JGSL"]);
            gxrjxx.DXLX = form["DXLX"];
            gxrjxx.RSJCC = form["RSJCC"];
            gxrjxx.DXLX = form["DXLX"];
            gxrjxx.LONGITUDE = form["LONGITUDE"];
            gxrjxx.LATITUDE = form["LATITUDE"];
            gxrjxx.MODIFYBY = CurrentUser.UserName;
            gxrjxx.MODIFYTIME = DateTime.Now;
            gxXlzxBusiness.Update(gxrjxx);
            return Json(gxXlzxBusiness.SaveChange() > 0 ? AjaxResult.Success("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
        }

        [HttpPost]
        public ActionResult Delete(List<string> list)
        {
            foreach (var item in list)
            {
                var model = gxXlzxBusiness.Find(Convert.ToDecimal(item));
                gxXlzxBusiness.Delete(model);
            }
            var ret = gxXlzxBusiness.SaveChange();
            return Json(ret > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        }

        /// <summary>
        /// 管线信息页面选择人井信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult RjxxList(int page, int rows)
        {
            //项目工程编号
            var ysxmid = ConvertUtility.ToDecimal(Request["ysxmid"]);
            int reacordCount = 0;
            string gxbh = ConvertUtility.ToString(Request["gxbh"]);
            var list = gxXlzxBusiness.FindAllGxrjxxByYsXmIdRjmx(ysxmid, gxbh, page, rows, out reacordCount);
            return Json(new { total = reacordCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
    }
}
