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
using Encrypt = Gx.CommonUnits.Encrypt;

namespace GxSystemMgrWeb.Controllers
{
    public class GxSysUserController : BaseController
    {
        private GxSysUserBusiness gxSysUserBusiness = new GxSysUserBusiness(); 
        //
        // GET: /GxSysUser/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1, int rows = 15)
        {
            int totalRecord;
            var list = gxSysUserBusiness.FindAllGxSysUser(page, rows, out totalRecord);
            return Json(new { total = totalRecord, rows = list });
        }

        public PartialViewResult Add()
        {
            return PartialView();
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(FormCollection form, GX_SYS_USER gxSysUser)
        {
            gxSysUser.ID = new Common().GetRandom();
            gxSysUser.USERNAME = form["USERNAME"];
            gxSysUser.USERID = form["USERID"];

            gxSysUser.USERMOBILE = form["USERMOBILE"];
            gxSysUser.USEREMAIL = form["USEREMAIL"];

            gxSysUser.USERPWD = form["USERPWD"];// Encrypt.MD5(Encrypt.MD5(form["USERPWD"] + Encrypt.DesKey));
            //gxSysUser.USERSTATUS = ConvertUtility.ToDecimal(form["USERSTATUS"]);
            
            //gxSysUser.LoginRoleId = ConvertUtility.ConvertToInt(form["LoginRole"], -1);
            //if (gxSysUser.LoginRoleId == -1)
            //    return Json(AjaxResult.Error("用户登录的角色为必选项！"));

            //gxSysUser.CREATEBY = CurrentUser == null ? "" : CurrentUser.UserName;
            //gxSysUser.CREATETIME = DateTime.Now;

            gxSysUserBusiness.Add(gxSysUser);
            return Json(gxSysUserBusiness.SaveChage() > 0 ? new OperationResult(OperationResultType.Success, "数据操作成功！") : new OperationResult(OperationResultType.Error, "数据操作失败！")); 
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public PartialViewResult Edit(int id)
        {
            var model = gxSysUserBusiness.FindGxSysUser(id);
            return PartialView(model);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(FormCollection form, GX_SYS_USER gxSysUser)
        {
            string id = form["Id"] == null ? (CurrentUser == null ? "" : CurrentUser.UserId.ToString()) : form["Id"];
            var model = gxSysUserBusiness.FindGxSysUser(ConvertUtility.ToDecimal(id, 0));
            if (model == null)
                return Json("数据存在错误无法更新！");
            model.USERID = ConvertUtility.ToString(form["USERID"]);

            model.USERNAME = form["USERNAME"];

            if (form["USERPWD"] != "")
                model.USERPWD = Encrypt.MD5(Encrypt.MD5(form["Password"] + Encrypt.DesKey));
            else
                model.USERPWD = form["pwd"];
            if (model.USERPWD == "")
                return Json(AjaxResult.Error("用户密码不能为空！"));
            model.USEREMAIL = form["USEREMAIL"];
            model.USERMOBILE = form["USERMOBILE"];
            model.USERSTATUS = ConvertUtility.ToInt(form["USERSTATUS"]) == 0 ? model.USERSTATUS : ConvertUtility.ToInt(form["Status"]);
            if (model.USERSTATUS == 0)
                return Json(AjaxResult.Error("用户的登录状态为必选项！"));
            //admin.LoginRoleId = ConvertUtility.ConvertToInt(form["LoginRole"], -1) == -1 ? model.LoginRoleId : ConvertUtility.ConvertToInt(form["LoginRole"], -1);
            //if (admin.LoginRoleId == -1)
            //    return Json(AjaxResult.Error("用户登录的角色为必选项！"));
            model.MODIFYBY = CurrentUser.UserName;
            model.MODIFYTIME = DateTime.Now;
            gxSysUserBusiness.Update(model);
            return Json(gxSysUserBusiness.SaveChage() > 0 ? new OperationResult(OperationResultType.Success, "数据操作成功！") : new OperationResult(OperationResultType.Error, "数据操作失败！"));
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(List<string> list)
        {
            foreach (var item in list)
            {
                var gxSysUser = new GX_SYS_USER();
                gxSysUser.ID = Convert.ToDecimal(item);
                gxSysUser = gxSysUserBusiness.FindGxSysUser(gxSysUser);
                gxSysUserBusiness.Delete(gxSysUser);
            }
            var ret = gxSysUserBusiness.SaveChage();
            return Json(ret > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        } 
    }
}
