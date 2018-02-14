using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http; 
using System.Web.Mvc;
using Gx.Business;
using Gx.CommonUnits;
using Gx.CommonUnits.Tools;
using Gx.Models;
using JGB.Common.Units;

namespace GxSystemMgrWeb.Controllers
{
    public class GxXlzxController : BaseController
    {
        private GxXlzxBusiness gxXlzxBusiness = new GxXlzxBusiness();

        public PartialViewResult Index()
        {
            ViewData["roleid"] = ConvertUtility.ToString(Request["roleid"]);
            return PartialView();
        }

        [HttpPost]
        public ActionResult List(int page, int rows)
        {
            int reacordCount = 0;
            var roleid = ConvertUtility.ToDecimal(Request["roleid"]);
            var list = gxXlzxBusiness.FindAlXlzxList(roleid, page, rows, out reacordCount);
            return Json(new { total = reacordCount, rows = list });
        }

        public PartialViewResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(FormCollection form, GX_XLZX gxXlzx)
        {
            gxXlzx.ID = new Common().GetRandom();
            gxXlzx.NAME = form["name"];
            gxXlzx.ROLEID = ConvertUtility.ToDecimal(form["roleId"]);
            gxXlzx.CREATEBY = CurrentUser.UserName;
            gxXlzx.CREATETIME = DateTime.Now;
            gxXlzxBusiness.AddEntity(gxXlzx);

            return Json(gxXlzxBusiness.SaveChange() > 0 ? AjaxResult.Error("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
        }

        public PartialViewResult Edit(int id)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, GX_XLZX gxXlzx)
        {
            decimal id = ConvertUtility.ToDecimal(form["Id"]);
            var model = gxXlzxBusiness.Find(id);
            if (model == null)
                return Json(AjaxResult.Error("未找到需要更新的数据！"));
            gxXlzx.NAME = form["name"];
            gxXlzx.ROLEID = ConvertUtility.ToDecimal(form["roleId"]);
            gxXlzx.CREATEBY = model.CREATEBY;
            gxXlzx.CREATETIME = model.CREATETIME;
            gxXlzx.MODIFYBY = CurrentUser.UserName;
            gxXlzx.MODIFYTIME = DateTime.Now;
            gxXlzxBusiness.UpdateEntity(gxXlzx);
            return Json(gxXlzxBusiness.SaveChange() > 0 ? AjaxResult.Error("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
        }

        [HttpPost]
        public ActionResult Delet(List<string> list)
        {
            foreach (var item in list)
            {
                var model = gxXlzxBusiness.Find(Convert.ToDecimal(item));
                gxXlzxBusiness.DeleteEntity(model);
            }
            var ret = gxXlzxBusiness.SaveChange();
            return Json(ret > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        }
    }
}
