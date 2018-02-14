using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gx.Business;
using Gx.CommonUnits;
using Gx.CommonUnits.Tools;
using Gx.Models;
using Gx.Models.ViewModels;
using JGB.Common.Units;

namespace GxSystemMgrWeb.Controllers
{
    public class DictConfigController : BaseController
    {
        private GxDictConfigBusiness dictConfigBusiness = new GxDictConfigBusiness();
        //
        // GET: /DictConfig/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取字典配置数据集
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult List(int page, int rows)
        {
            var name = ConvertUtility.ToString(Request["name"]);
            var parentId = ConvertUtility.ToDecimal(Request["id"]);
            int totalRecord = 0;
            var list = dictConfigBusiness.FindAllGxDictConfig(name, parentId, page, rows, out totalRecord);
            List<ViewDictConfigModel> returnList = new List<ViewDictConfigModel>();
            foreach (var item in list)
            {
                ViewDictConfigModel model = new ViewDictConfigModel();
                model.Id = item.ID;
                model.Name = item.NAME;
                model.ParentId = item.PARENTID;
                var dictModel = dictConfigBusiness.FindDictConfig(ConvertUtility.ToDecimal(item.PARENTID));
                model.ParentName = dictModel == null ? "" : dictModel.NAME;
                model.CreateTime = item.CREATETIME;
                model.Status = item.STATUS;
                model.DictDesc = item.DICTDESC;
                returnList.Add(model);
            }
            return Json(new { total = totalRecord, rows = returnList });
        }

        public PartialViewResult Add()
        {
            ViewData["type"] = ConvertUtility.ToString(Request["type"]);
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(FormCollection form, GX_DICT_CONFIG dictConfig)
        {
            dictConfig.ID = new Common().GetRandom();
            dictConfig.NAME = form["Name"];
            if (string.IsNullOrEmpty(dictConfig.NAME))
                return Json(AjaxResult.Error("字典名称不能为空！"));
            dictConfig.PARENTID = ConvertUtility.ToDecimal(form["PreId"]);
            dictConfig.STATUS = ConvertUtility.ToString(form["STATUS"]);
            dictConfig.DICTDESC = form["Bz"];
            dictConfig.CREATEBY = CurrentUser.UserName;
            dictConfig.CREATETIME = DateTime.Now;
            dictConfigBusiness.AddEntity(dictConfig);
            return Json(dictConfigBusiness.SaveChange() > 0 ? AjaxResult.Success("数据修改成功") : AjaxResult.Error("数据修改失败"));
        }

        public PartialViewResult Edit()
        {
            var id = ConvertUtility.ToDecimal(Request["key"]);
            var model = dictConfigBusiness.FindDictConfig(id);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, GX_DICT_CONFIG dictConfig)
        {
            var id = ConvertUtility.ToDecimal(form["Id"]);
            var model = dictConfigBusiness.FindDictConfig(id);
            if (model == null)
                return Json(AjaxResult.Error("未找到要修改的数据！"));
            model.NAME = form["Name"];
            model.PARENTID = ConvertUtility.ToDecimal(form["PreId"]);
            model.STATUS = ConvertUtility.ToString(form["STATUS"]);
            model.DICTDESC = form["Bz"];
            model.MODIFYBY = CurrentUser.UserName;
            model.MODIFYTIME = DateTime.Now;
            dictConfigBusiness.UpdateEntity(model);
            return Json(dictConfigBusiness.SaveChange() > 0 ? AjaxResult.Success("数据修改成功") : AjaxResult.Error("数据修改失败"));
        }

        public ActionResult Delete(List<string> list)
        {
            foreach (var item in list)
            {
                var model = dictConfigBusiness.FindDictConfig(Convert.ToDecimal(item));
                dictConfigBusiness.DeleteEntity(model);
            }
            var ret = dictConfigBusiness.SaveChange();
            return Json(ret > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        }

        /// <summary>
        /// 获取所以字典表父节点数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllDictConfig()
        {
            var list = dictConfigBusiness.FindAllDictConfig().Where(t => t.PARENTID == 0).ToList();
            var listData = list.Select(c => new EasyuiDropDown { Value = c.ID.ToString(), Text = c.NAME }).ToList();
            listData.Insert(0, new EasyuiDropDown { Value = "0", Text = "请选择" });
            return Json(listData);
        }

        /// <summary>
        /// 根据字典类型获取字典配置信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDictConfigByParentId()
        {
            var parentId = ConvertUtility.ToDecimal(Request["id"]);
            var list = dictConfigBusiness.FindAllDictConfig().Where(t => t.PARENTID == parentId).ToList();
            var listData = list.Select(c => new EasyuiDropDown { Value = c.ID.ToString(), Text = c.NAME }).ToList();
            listData.Insert(0, new EasyuiDropDown { Value = "0", Text = "请选择" });
            return Json(listData);
        }

        /// <summary>
        /// 根据字典类型获取字典配置信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDictConfigByName()
        {
            var dictId = ConvertUtility.ToDecimal(Request["name"]);
            var list = dictConfigBusiness.FindAllDictConfig(dictId).ToList();
            var listData = list.Select(c => new EasyuiDropDown { Value = c.ID.ToString(), Text = c.NAME }).ToList();
            listData.Insert(0, new EasyuiDropDown { Value = "0", Text = "请选择" });
            return Json(listData);
        }
    }
}
