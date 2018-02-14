using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gx.Business;
using Gx.CommonUnits;
using Gx.CommonUnits.Tools;
using Gx.Models;
using JGB.Common.Units;

namespace GxSystemMgrWeb.Controllers
{
    public class GxSysMenuController : BaseController
    {
        private Common common = new Common();
        GxSysMenuBusniess gxSysMenuBusniess = new GxSysMenuBusniess();
        GxSysRoleMenuBusniess gxSysRoleMenuBusniess = new GxSysRoleMenuBusniess();
        private GxSysModelsBusniess gxSysModelsBusniess = new GxSysModelsBusniess();
        //
        // GET: /GxSysMenu/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取大类信息列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult List(int page, int rows)
        {
            int totalRecord;
            var list = gxSysMenuBusniess.FindAlMenuList(page, rows, out totalRecord);
            var data = (
                from h in list
                select new
                {
                    h.MENUNAMC,
                    h.CREATEBY,
                    h.CREATETIME,
                    h.ID,
                    h.MENUURL,
                    h.MENUBZ,
                    h.MENUSTATUS,
                    //h.Sort,
                    h.MENUPARENTID,
                    ModelName = _Valid_lamba(h.MODELID)
                }
                ).ToList();
            return Json(new { total = totalRecord, rows = data });
        }

        /// <summary>
        /// 获取菜单大类信息
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public string _Valid_lamba(decimal? modelId)
        {
            var entity = gxSysModelsBusniess.Find(Convert.ToDecimal(modelId));
            return entity == null ? "" : entity.NAME;
        }

        public PartialViewResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(FormCollection form,GX_SYS_MENU gxSysMenu)
        {
            gxSysMenu.ID = new Common().GetRandom();
            gxSysMenu.MENUNAMC = form["MENUNAMC"];
            gxSysMenu.MODELID = ConvertUtility.ToDecimal(form["MODELID"]);
            gxSysMenu.MENUURL = form["MENUURL"];
            gxSysMenu.MENUBZ = form["MENUBZ"];
            gxSysMenu.MENUSTATUS = ConvertUtility.ToDecimal(form["MENUSTATUS"]);
            if (gxSysMenu.MODELID <= 0)
                return Json(AjaxResult.Error("请选择菜单所属类型！"));
            gxSysMenu.CREATEBY = CurrentUser.UserName;
            gxSysMenu.CREATETIME = DateTime.Now;
            gxSysMenuBusniess.AddEntity(gxSysMenu);
            return Json(gxSysMenuBusniess.SaveChange() > 0 ? AjaxResult.Error("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
        }

        /// <summary>
        /// 菜单修改页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult Edit(int id)
        {
            var model = gxSysMenuBusniess.Find(id);
            return PartialView(model);
        }

        /// <summary>
        /// 菜单修改数据处理
        /// </summary>
        /// <param name="form"></param>
        /// <param name="gxSysMenu"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(FormCollection form, GX_SYS_MENU gxSysMenu)
        {
            decimal id = ConvertUtility.ToDecimal(form["Id"]);
            var model = gxSysMenuBusniess.Find(id);
            if (model == null)
                return Json(AjaxResult.Error("未找到修改的数据！"));
            model.MENUNAMC = form["MENUNAMC"];
            model.MODELID = ConvertUtility.ToDecimal(form["MODELID"]);
            model.MENUURL = form["MENUURL"];
            model.MENUBZ = form["MENUBZ"];
            model.MENUSTATUS = ConvertUtility.ToDecimal(form["MENUSTATUS"]);
            if (model.MODELID <= 0)
                return Json(AjaxResult.Error("请选择菜单所属类型！"));
            model.MODIFYBY = CurrentUser.UserId;
            model.MODIFYTIME = DateTime.Now;
            gxSysMenuBusniess.UpdateEntity(model);
            return Json(gxSysMenuBusniess.SaveChange() > 0 ? AjaxResult.Error("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
        }

        /// <summary>
        /// 数据删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(List<string> list)
        {
            foreach (var item in list)
            {
                var id = Convert.ToDecimal(item);
                var model = gxSysMenuBusniess.Find(id);
                gxSysMenuBusniess.DeleteEntity(model);
            }
            var ret = gxSysMenuBusniess.SaveChange();
            return Json(ret > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        }

        /// <summary>
        /// 设置角色用户菜单
        /// </summary>
        /// <returns></returns>
        public PartialViewResult SetRoleMenu()
        {
            ViewData["roleId"] = ConvertUtility.ToString(Request.QueryString["roleId"]);

            return PartialView();
        }

        /// <summary>
        /// 返回菜单类型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNotSelectMenu()
        {
            int roleId = ConvertUtility.ToInt(Request["roleId"]);
            var list = gxSysMenuBusniess.GetNotSelectMenuRole(roleId).ToList();
            var listData = list.Select(c => new EasyuiDropDown { Value = c.MenuId.ToString(), Text = c.MenuName }).ToList();
            listData.Insert(0, new EasyuiDropDown { Value = "", Text = "请选择" });
            return Json(listData);
        }

        public PartialViewResult AddRoleMenu()
        {
            ViewData["roleId"] = ConvertUtility.ToString(Request.QueryString["roleId"]);
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddRoleMenu(FormCollection form)
        {
            var roleId = ConvertUtility.ToDecimal(Request.Form["RoleId"]);
            GX_SYS_ROLEMENU gxSys = new GX_SYS_ROLEMENU();
            gxSys.ID = common.GetRandom();
            gxSys.ROLEID = roleId;
            gxSys.MENUID = ConvertUtility.ToDecimal(Request.Form["ROLENAME"]);
            gxSysRoleMenuBusniess.Add(gxSys);
            return Json(gxSysRoleMenuBusniess.SaveChange() > 0 ? AjaxResult.Success("添加成功") : AjaxResult.Error("添加失败"));
        }

        public ActionResult RoleMenuList(int page = 1, int rows = 15)
        {
            var roleId = ConvertUtility.ToDecimal(Request["roleId"]);
            int totalRecord;
            var list = gxSysMenuBusniess.FindAllRoleMenuList(roleId, page, rows, out totalRecord);
            return Json(new { total = totalRecord, rows = list });
        }

        public ActionResult DeRoleMenu(List<string> list)
        {
            foreach (var item in list)
            {
                var id = Convert.ToDecimal(item);
                var model = gxSysRoleMenuBusniess.Find(id);
                gxSysRoleMenuBusniess.Delete(model);
            }
            var ret = gxSysRoleMenuBusniess.SaveChange();
            return Json(ret > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        }
    }
}
