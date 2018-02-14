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
    public class GxSysRoleController : BaseController
    {
        private Common common = new Common();
        private GXSysRoleBusiness gxSysRoleBusiness = new GXSysRoleBusiness();
        GxSysUserRoleBusiness business = new GxSysUserRoleBusiness();
        //
        // GET: /GxSysRole/

        public PartialViewResult Index()
        {
            return PartialView();
        }

        public ActionResult List(int page,int rows)
        {
            int totalRecord;
            var list = gxSysRoleBusiness.FindAllGxSysRole(page, rows, out totalRecord);
            return Json(new { total = totalRecord, rows = list });
        }

        public PartialViewResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(FormCollection form, GX_SYS_ROLE gxSysRole)
        {
            try
            {
                gxSysRole.ID = common.GetRandom();
                gxSysRole.ROLENAME = form["ROLENAME"];
                gxSysRole.ROLEDESC = form["ROLEDESC"];
                gxSysRole.ROLESTATUS = form["ROLESTATUS"];
                gxSysRole.CREATEBY = CurrentUser.UserId;
                gxSysRole.CREATETIME = DateTime.Now;
                gxSysRoleBusiness.Add(gxSysRole);
            }
            catch (Exception ex)
            {
                return Json(new OperationResult(OperationResultType.Error, "数据操作失败：" + (ex.InnerException == null ? ex.Message : ex.InnerException.Message)));
            }
            return Json(gxSysRoleBusiness.SaveChange() > 0 ? new OperationResult(OperationResultType.Success, "数据操作成功！") : new OperationResult(OperationResultType.Error, "数据操作失败！")); 
        }

        public PartialViewResult Edit(int id)
        {
            var model = gxSysRoleBusiness.Find(id);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, GX_SYS_ROLE gxSysRole)
        {
            int id = ConvertUtility.ToInt(form["Id"]);
            var model = gxSysRoleBusiness.Find(id);
            if (model == null)
                return Json(AjaxResult.Error("为找到需要更新的数据源！"));
            model.ROLENAME = form["ROLENAME"];
            model.ROLEDESC = form["ROLEDESC"];
            model.ROLESTATUS = form["ROLESTATUS"];
            model.MODIFYBY = CurrentUser.UserId;
            model.MODIFYTIME = DateTime.Now;
            gxSysRoleBusiness.Update(model);
            return Json(gxSysRoleBusiness.SaveChange() > 0 ? new OperationResult(OperationResultType.Success, "数据操作成功！") : new OperationResult(OperationResultType.Error, "数据操作失败！")); 
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(List<string> list)
        {
            foreach (var item in list)
            {
                var id = Convert.ToDecimal(item);
                var model = gxSysRoleBusiness.Find(id);
                gxSysRoleBusiness.Delete(model);
            }
            var ret = gxSysRoleBusiness.SaveChange();
            return Json(ret > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        }

        /// <summary>
        /// 返回菜单类型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllModels()
        {
            var list = gxSysRoleBusiness.FindAll().ToList();
            var listData = list.Select(c => new EasyuiDropDown { Value = c.ID.ToString(), Text = c.ROLENAME }).ToList();
            listData.Insert(0, new EasyuiDropDown { Value = "", Text = "请选择" });
            return Json(listData);
        } 

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <returns></returns>
        public PartialViewResult SetRole()
        {
            ViewData["UserId"] = ConvertUtility.ToString(Request.QueryString["userid"]);
            return PartialView();
        }

        public ActionResult UserRoleList(int page = 1, int rows = 15)
        {
            var userid = ConvertUtility.ToDecimal(Request["userid"]);
            int totalRecord;
            var list = gxSysRoleBusiness.FindAllUserRoleByUserId(userid, page, rows, out totalRecord);
            return Json(new { total = totalRecord, rows = list });
        }

        public PartialViewResult AddRoleUser()
        {
            ViewData["UserId"] = ConvertUtility.ToString(Request.QueryString["userid"]);
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddRoleUser(FormCollection form)
        { 
            var userid = ConvertUtility.ToDecimal(form["userid"]);
            GX_SYS_USERROLERELATE gxSys = new GX_SYS_USERROLERELATE();
            gxSys.ID = common.GetRandom();
            gxSys.ROLEID = ConvertUtility.ToDecimal(form["roleid"]);
            gxSys.USERID = userid;
            business.AddEntity(gxSys);
            return Json(gxSysRoleBusiness.SaveChange() > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        }

        [HttpPost]
        public ActionResult DelUserRole(List<string> list)
        {
            foreach (var item in list)
            {
                var id = Convert.ToDecimal(item);
                var model = business.FindEntity(id);
                business.DeleteEntity(model);
            }
            var ret = gxSysRoleBusiness.SaveChange();
            return Json(ret > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        }
    }
}
