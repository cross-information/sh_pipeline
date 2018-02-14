using System;
using System.Collections.Generic;
using System.Linq;
using System.Net; 
using System.Web.Mvc;
using Gx.Business;
using Gx.CommonUnits;
using Gx.CommonUnits.Tools;
using Gx.Models;
using JGB.Common.Units;

namespace GxSystemMgrWeb.Controllers
{
    public class GxSysModelsController : BaseController
    {
        private GxSysModelsBusniess gxSysModelsBusniess = new GxSysModelsBusniess();
        private Common common = new Common();

        public PartialViewResult Index()
        {
            return PartialView();
        }

        public ActionResult List(int page, int rows)
        {
            int totalRecord;
            var list = gxSysModelsBusniess.FindAlMenuList(page, rows, out totalRecord);
            return Json(new { total = totalRecord, rows = list });
        }

        public PartialViewResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(FormCollection form ,GX_SYS_MODELS gxSysModels)
        {
            try
            {
                gxSysModels.ID = common.GetRandom();
                gxSysModels.NAME = form["NAME"];
                gxSysModels.REMARK = form["REMARK"];
                gxSysModels.FLAG = ConvertUtility.ToDecimal(form["FLAG"]);
                gxSysModels.CREATEBY = CurrentUser.UserId;
                gxSysModels.CREATETIME = DateTime.Now;
                gxSysModelsBusniess.Add(gxSysModels);
            }
            catch (Exception ex)
            {
                return Json(new OperationResult(OperationResultType.Error, "数据操作失败：" + (ex.InnerException == null ? ex.Message : ex.InnerException.Message)));
            }
            return Json(gxSysModelsBusniess.SaveChange() > 0 ? new OperationResult(OperationResultType.Success, "数据操作成功！") : new OperationResult(OperationResultType.Error, "数据操作失败！")); 
        }

        public PartialViewResult Edit(int id)
        {
            var model = gxSysModelsBusniess.Find(id);
            return PartialView(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(FormCollection form, GX_SYS_MODELS gxSysModels)
        {
            decimal id = ConvertUtility.ToDecimal(form["Id"]);
            try
            {
                var model = gxSysModelsBusniess.Find(id);
                if (model == null)
                    return Json(new OperationResult(OperationResultType.Error, "未找到你要修改的数据"));
                model.NAME = form["NAME"];
                model.REMARK = form["REMARK"];
                model.FLAG = ConvertUtility.ToDecimal(form["FLAG"]);
                model.MODIFYBY = CurrentUser.UserId;
                model.MODIFYTIME = DateTime.Now;
                gxSysModelsBusniess.Update(model);
            }
            catch (Exception ex)
            {
                return Json(new OperationResult(OperationResultType.Error, "数据操作失败：" + (ex.InnerException == null ? ex.Message : ex.InnerException.Message)));
            }
            return Json(gxSysModelsBusniess.SaveChange() > 0 ? new OperationResult(OperationResultType.Success, "数据操作成功！") : new OperationResult(OperationResultType.Error, "数据操作失败！"));
        }

        /// <summary>
        /// 删除大类信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(List<string> list)
        {
            foreach (var item in list)
            {
                var id = Convert.ToDecimal(item);
                var model = gxSysModelsBusniess.Find(id);
                gxSysModelsBusniess.Delete(model);
            }
            var ret = gxSysModelsBusniess.SaveChange();
            return Json(ret > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        }

        /// <summary>
        /// 返回菜单类型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllModels()
        {
            var list = gxSysModelsBusniess.FindAllModels().ToList();
            var listData = list.Select(c => new EasyuiDropDown { Value = c.ID.ToString(), Text = c.NAME }).ToList();
            listData.Insert(0, new EasyuiDropDown { Value = "", Text = "请选择" });
            return Json(listData);
        } 
    }
}
