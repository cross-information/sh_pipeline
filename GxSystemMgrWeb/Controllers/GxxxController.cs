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
using Gx.Models.ViewModels;
using JGB.Common.Units;

namespace GxSystemMgrWeb.Controllers
{
    public class GxxxController : BaseController
    {
        private Common common = new Common();
        private GxGxxxBusiness gxxxBusiness = new GxGxxxBusiness();
        private GxSysUserRoleBusiness gxSysUserRoleBusiness = new GxSysUserRoleBusiness();
        private GxRjxxBusiness rjxxBusiness = new GxRjxxBusiness();
        private GxYsxmxxBusiness gxYsxmxxBusiness = new GxYsxmxxBusiness();
        private GxSysUserBusiness gxSysUserBusiness = new GxSysUserBusiness();

        /// <summary>
        /// 管线信息管理
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Gxxx()
        {
            ViewData["xmgcid"] = ConvertUtility.ToString(Request["gcbdid"]);
            var model = gxYsxmxxBusiness.FindEntity(ConvertUtility.ToDecimal(Request["gcbdid"]));
            ViewData["gcbh"] = model == null ? "" : model.GCBH;
            ViewData["gcmc"] = model == null ? "" : model.GCBH;
            var userModel = gxSysUserBusiness.FindGxSysUser(ConvertUtility.ToDecimal(model == null ? "0" : model.SGDWMC));
            ViewData["sgdwmc"] = userModel == null ? "" : userModel.USERNAME;
            return PartialView();
        }

        /// <summary>
        /// 获取页面显示的数据列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult List(int page, int rows)
        {
            var xmgcid = ConvertUtility.ToDecimal(Request["gcbdid"]);
            var gxbh = ConvertUtility.ToString(Request["gxbh"]);
            var stqk = ConvertUtility.ToString(Request["stqk"]);
            int totalRecord = 0;
            var list = gxxxBusiness.FindAllGxxxList(xmgcid, gxbh, stqk, page, rows, out totalRecord);
            List<GX_GXXX> gxxxList = new List<GX_GXXX>();
            foreach (var item in list)
            {
                GX_GXXX gxxx = new GX_GXXX();
                gxxx = item;
                gxxx.QJ = GetRjmcById(item.QJ);
                gxxx.ZJ = GetRjmcById(item.ZJ);
                gxxxList.Add(gxxx);
            }
            return Json(new { total = totalRecord, rows = gxxxList });
        }

        /// <summary>
        /// 获取人井名称
        /// </summary>
        /// <param name="rjid"></param>
        /// <returns></returns>
        public string GetRjmcById(string rjid)
        {
            var model = rjxxBusiness.Find(ConvertUtility.ToDecimal(rjid));
            return model == null ? "" : model.RJMC;
        }
        
        #region 管孔信息

        public PartialViewResult Gkxx()
        {
            ViewData["GXBH"] = "GX" + common.GetRandom();
            ViewData["GKBH"] = "GK" + common.GetRandom();
            ViewData["ysid"] = ConvertUtility.ToString(Request["ysid"]);
            ViewData["id"] = ConvertUtility.ToString(common.GetRandom());

            var ysryRole = System.Configuration.ConfigurationManager.AppSettings["YsryRole"];
            var sgryRole = System.Configuration.ConfigurationManager.AppSettings["SgryRole"];
            ViewData["YsryUserRole"] = gxSysUserRoleBusiness.FindRoleByUserIdAndRleName(ysryRole, CurrentUser.Id) == null ? "" : "Ysry";
            ViewData["SgryUserRole"] = gxSysUserRoleBusiness.FindRoleByUserIdAndRleName(sgryRole, CurrentUser.Id) == null ? "" : "Sgry";
            return PartialView();
        }

        /// <summary>
        /// 保存管线信息(管线信息页面起止人井信息保存)
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateGxxx(FormCollection form)
        {
            try
            {
                var id = ConvertUtility.ToDecimal(form["Gxxxid"]);
                var model = gxxxBusiness.Find(id);
                if (model == null)
                    return Json(AjaxResult.Error("请保存管线基本信息后再保存管线起止井信息"));
                GX_GXXX gxxx = new GX_GXXX();
                gxxx = model;
                //起止井位置信息
                gxxx.QJ = form["Qjszwz"];
                gxxx.ZJ = form["Zjszwz"];
                //起止井所在管孔位置
                gxxx.QJGKWZ = form["gxszgkqjwz"];
                gxxx.ZJGKWZ = form["gxszgkwz"];
                gxxxBusiness.UpdateEntity(model);
                var ret = gxxxBusiness.SaveChange();
                return Json(ret > 0 ? AjaxResult.Success("数据操作成功") : AjaxResult.Error("数据操作失败"));
            }
            catch (Exception ex)
            {
                return Json(AjaxResult.Error("删除失败:" + (ex.InnerException != null ? ex.InnerException.Message : ex.Message)));
            }
        }

        /// <summary>
        /// 删除管线信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeletGxxx()
        {
            //TODO 需添加操作人权限验证
            var model = gxxxBusiness.Find(Convert.ToDecimal(Request["id"]));
            gxxxBusiness.DeleteEntity(model);
            var ret = gxxxBusiness.SaveChange();
            return Json(ret > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        }

        /// <summary>
        /// 保存管线基本信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddJbxx(FormCollection form, GX_GXXX gxxx)
        {
            gxxx.ID = ConvertUtility.ToDecimal(form["Gxxxid"]);
            var model = gxxxBusiness.Find(ConvertUtility.ToDecimal(form["Gxxxid"]));
            if (model == null)
            {
                //如果页面未关闭 多次点击保存按钮 验证是否存在 不存在生成数据 
                gxxxBusiness.AddEntity(gxxx);
                gxxxBusiness.SaveChange();
            }
            //如果页面数据存在 则编辑数据
            gxxx.XMYSXXID = ConvertUtility.ToDecimal(form["ysid"]);
            gxxx.GCBH = form["GXBH"];
            gxxx.GXWZ = form["GXWZ"];
            gxxx.GXCD = ConvertUtility.ToDecimal(form["GXCD"]);
            gxxx.DXLX = form["DXLX"];
            gxxx.GXLX = form["GXLX"];
            gxxx.JSZT = form["JSZT"];
            gxxx.YWLX = form["YWLX"];
            gxxx.GJZ = form["GJZ"];
            gxxx.XNZC = form["XNZC"];
            gxxx.GKCL = form["GKCL"];
            gxxx.SYZT = ConvertUtility.ToDecimal(form["SYZT"]);
            gxxx.GKHS = ConvertUtility.ToDecimal(form["GKHS"]);
            gxxx.GKLS = ConvertUtility.ToDecimal(form["GCBH"]);
            gxxx.PLSX = ConvertUtility.ToDecimal(form["GCBH"]);
            gxxx.GXZT = form["GCBH"];
            gxxx.CREATEBY = CurrentUser.UserName;
            gxxx.CREATETIME = DateTime.Now;
            gxxxBusiness.UpdateEntity(gxxx);
            ViewData["GxxxId"] = gxxx.ID;
            return Json(gxxxBusiness.SaveChange() > 0 ? AjaxResult.Success("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
        }

        /// <summary>
        /// 修改管线信息（管孔页面保存按钮操作）
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateGxxxByGkxxPage(FormCollection form)
        {
            var id = ConvertUtility.ToDecimal(form["Gxxxid"]);
            var model = gxxxBusiness.Find(id);
            if (model == null)
                return Json(AjaxResult.Error("请保存管线基本信息后再生成管孔信息"));
            model.GKCL = form["GXXXGKCL"];
            model.SYZT = ConvertUtility.ToDecimal(form["GXXXSYZT"]);
            model.GKLS = ConvertUtility.ToDecimal(form["GKLS"]);
            model.GKHS = ConvertUtility.ToDecimal(form["GKHS"]);
            model.PLSX = ConvertUtility.ToDecimal(form["PLSX"]);
            model.MODIFYBY = CurrentUser.UserName;
            model.MODIFYTIME = DateTime.Now;
            gxxxBusiness.UpdateEntity(model);
            return Json(gxxxBusiness.SaveChange() > 0 ? AjaxResult.Success("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
        }

        /// <summary>
        /// 保存管孔信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AddGkxx(FormCollection form, GX_GKXX gkxx)
        {
            if (string.IsNullOrEmpty(form["gkhsParent"]) || string.IsNullOrEmpty(form["gklsParent"]))
                AjaxResult.Error("请选择管孔所在位置！");
            gkxx.ID = common.GetRandom();
            //管线信息生成完 产生id并返回到页面 再对应到管孔信息表
            gkxx.GXXXID = ConvertUtility.ToDecimal(form["Gxxxid"]);
            gkxx.GKBH = form["GKBH"];
            gkxx.GKCL = form["GKXXGKCL"];
            gkxx.SYZT = ConvertUtility.ToDecimal(form["GKXXSYZT"]);
            gkxx.ZKHS = ConvertUtility.ToDecimal(form["GKHS1"]);
            gkxx.ZKLS = ConvertUtility.ToDecimal(form["GKLS1"]);
            gkxx.ZKSL = ConvertUtility.ToDecimal(form["GXSL"]);
            gkxx.GKSZH = ConvertUtility.ToDecimal(form["gkhsParent"]);
            gkxx.GKSZL = ConvertUtility.ToDecimal(form["gklsParent"]);
            gkxx.CREATEBY = CurrentUser.UserName;
            gkxx.CREATETIME = DateTime.Now;
            gxxxBusiness.AddGkxx(gkxx);
            return Json(gxxxBusiness.SaveChange() > 0 ? AjaxResult.Success("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
        }

        /// <summary>
        /// 管线信息查看页面
        /// </summary>
        /// <returns></returns>
        public PartialViewResult LookDetail()
        {
            var id = ConvertUtility.ToDecimal(Request["id"]);
            if (id <= 0)
                return PartialView(new GxRjxxModel());
            //获取页面基本信息保存按钮权限
            var ysryRole = System.Configuration.ConfigurationManager.AppSettings["YsryRole"];
            var sgryRole = System.Configuration.ConfigurationManager.AppSettings["SgryRole"];
            ViewData["YsryUserRole"] = gxSysUserRoleBusiness.FindRoleByUserIdAndRleName(ysryRole, CurrentUser.Id) == null ? "" : "Ysry";
            ViewData["SgryUserRole"] = gxSysUserRoleBusiness.FindRoleByUserIdAndRleName(sgryRole, CurrentUser.Id) == null ? "" : "Sgry";

            GxRjxxModel gxRjxxModel = new GxRjxxModel();
            var gxxxModel = gxxxBusiness.Find(id);
            gxRjxxModel.ID = gxxxModel.ID;

            gxRjxxModel.GCBH = gxxxModel.GCBH;
            gxRjxxModel.GXWZ = gxxxModel.GXWZ;
            gxRjxxModel.GXCD = gxxxModel.GXCD;
            gxRjxxModel.DXLX = gxxxModel.DXLX;
            gxRjxxModel.GXLX = gxxxModel.GXLX;
            gxRjxxModel.JSZT = gxxxModel.JSZT;
            gxRjxxModel.YWLX = gxxxModel.YWLX;
            gxRjxxModel.GJZ = gxxxModel.GJZ;
            gxRjxxModel.XNZC = gxxxModel.XNZC;
            gxRjxxModel.GKCL = gxxxModel.GKCL;
            gxRjxxModel.SYZT = gxxxModel.SYZT;
            gxRjxxModel.GKHS = gxxxModel.GKHS;
            gxRjxxModel.GKLS = gxxxModel.GKLS;
            gxRjxxModel.PLSX = gxxxModel.PLSX;
            gxRjxxModel.GXZT = gxxxModel.GXZT;
            gxRjxxModel.GH = gxxxModel.GH;
            gxRjxxModel.JJ = gxxxModel.JJ;
            gxRjxxModel.STQK = gxxxModel.STQK;
            gxRjxxModel.QJ = gxxxModel.QJ;
            gxRjxxModel.ZJ = gxxxModel.ZJ;
            var gxGkxx = gxxxBusiness.FindGkxx(gxxxModel.ID);
            gxRjxxModel.GKBH = gxGkxx == null ? "" : gxGkxx.GKBH;
            gxRjxxModel.ZKHS = gxGkxx == null ? 0 : gxGkxx.ZKHS;
            gxRjxxModel.ZKLS = gxGkxx == null ? 0 : gxGkxx.ZKLS;
            gxRjxxModel.ZKSL = gxGkxx == null ? 0 : gxGkxx.ZKSL;
            gxRjxxModel.GKSZH = gxGkxx == null ? 0 : gxGkxx.GKSZH;
            gxRjxxModel.GKSZL = gxGkxx == null ? 0 : gxGkxx.GKSZL;
            gxRjxxModel.Qjwz = gxxxModel.QJGKWZ;
            gxRjxxModel.Zjwz = gxxxModel.ZJGKWZ;
            gxRjxxModel.gcxmid = gxxxModel.XMYSXXID;
            gxRjxxModel.GkxxId = gxGkxx == null ? 0 : gxGkxx.ID;

            ViewData["QjWz"] = GetRjmcById(ConvertUtility.ToDecimal(gxxxModel.QJ));
            ViewData["ZjWz"] = GetRjmcById(ConvertUtility.ToDecimal(gxxxModel.ZJ));
            return PartialView(gxRjxxModel);
        }
        #endregion

        /// <summary>
        /// 管线信息编辑
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Edit()
        {
            var id = ConvertUtility.ToDecimal(Request["id"]);
            if (id <= 0)
                return PartialView(new GxRjxxModel());
            //获取页面基本信息保存按钮权限
            var ysryRole = System.Configuration.ConfigurationManager.AppSettings["YsryRole"];
            var sgryRole = System.Configuration.ConfigurationManager.AppSettings["SgryRole"];
            ViewData["YsryUserRole"] = gxSysUserRoleBusiness.FindRoleByUserIdAndRleName(ysryRole, CurrentUser.Id) == null ? "" : "Ysry";
            ViewData["SgryUserRole"] = gxSysUserRoleBusiness.FindRoleByUserIdAndRleName(sgryRole, CurrentUser.Id) == null ? "" : "Sgry";

            GxRjxxModel gxRjxxModel = new GxRjxxModel();
            var gxxxModel = gxxxBusiness.Find(id);
            gxRjxxModel.ID = gxxxModel.ID;

            gxRjxxModel.GCBH = gxxxModel.GCBH;
            gxRjxxModel.GXWZ = gxxxModel.GXWZ;
            gxRjxxModel.GXCD = gxxxModel.GXCD;
            gxRjxxModel.DXLX = gxxxModel.DXLX;
            gxRjxxModel.GXLX = gxxxModel.GXLX;
            gxRjxxModel.JSZT = gxxxModel.JSZT;
            gxRjxxModel.YWLX = gxxxModel.YWLX;
            gxRjxxModel.GJZ = gxxxModel.GJZ;
            gxRjxxModel.XNZC = gxxxModel.XNZC;
            gxRjxxModel.GKCL = gxxxModel.GKCL;
            gxRjxxModel.SYZT = gxxxModel.SYZT;
            gxRjxxModel.GKHS = gxxxModel.GKHS;
            gxRjxxModel.GKLS = gxxxModel.GKLS;
            gxRjxxModel.PLSX = gxxxModel.PLSX;
            gxRjxxModel.GXZT = gxxxModel.GXZT;
            gxRjxxModel.GH = gxxxModel.GH;
            gxRjxxModel.JJ = gxxxModel.JJ;
            gxRjxxModel.STQK = gxxxModel.STQK;
            gxRjxxModel.QJ = gxxxModel.QJ;
            gxRjxxModel.ZJ = gxxxModel.ZJ;
            var gxGkxx = gxxxBusiness.FindGkxx(gxxxModel.ID);
            //如果管孔信息不存在 则生成新的管孔编号
            gxRjxxModel.GKBH = gxGkxx == null ? "" : ("GK" + new Common().GetRandom().ToString());
            gxRjxxModel.ZKHS = gxGkxx == null ? 0 : gxGkxx.ZKHS;
            gxRjxxModel.ZKLS = gxGkxx == null ? 0 : gxGkxx.ZKLS;
            gxRjxxModel.ZKSL = gxGkxx == null ? 0 : gxGkxx.ZKSL;
            gxRjxxModel.GKSZH = gxGkxx == null ? 0 : gxGkxx.GKSZH;
            gxRjxxModel.GKSZL = gxGkxx == null ? 0 : gxGkxx.GKSZL;
            gxRjxxModel.Qjwz = gxxxModel.QJGKWZ;
            gxRjxxModel.Zjwz = gxxxModel.ZJGKWZ;
            gxRjxxModel.gcxmid = gxxxModel.XMYSXXID;
            gxRjxxModel.GkxxId = gxGkxx == null ? 0 : gxGkxx.ID;

            ViewData["QjWz"] = GetRjmcById(ConvertUtility.ToDecimal(gxxxModel.QJ));
            ViewData["ZjWz"] = GetRjmcById(ConvertUtility.ToDecimal(gxxxModel.ZJ));
            return PartialView(gxRjxxModel);
        }

        /// <summary>
        /// 获取人井名称
        /// </summary>
        /// <param name="rjid"></param>
        /// <returns></returns>
        public string GetRjmcById(decimal rjid)
        {
            var model = rjxxBusiness.Find(ConvertUtility.ToDecimal(rjid));
            return model == null ? "" : (model.RJBH + model.RJMC);
        } 

        /// <summary>
        /// 保存管线基本信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditJbxx(FormCollection form, GX_GXXX gxxx)
        {
            var id = ConvertUtility.ToDecimal(form["Gxxxid"]);
            var model = gxxxBusiness.Find(id);
            if (model == null)
                return Json(AjaxResult.Error("未找到需要更新的数据"));
            gxxx.XMYSXXID = ConvertUtility.ToDecimal(form["ysid"]);
            gxxx.GCBH = form["GXBH"];
            gxxx.GXWZ = form["GXWZ"];
            gxxx.GXCD = ConvertUtility.ToDecimal(form["GXCD"]);
            gxxx.DXLX = form["DXLX"];
            gxxx.GXLX = form["GXLX"];
            gxxx.JSZT = form["JSZT"];
            gxxx.YWLX = form["YWLX"];
            gxxx.GJZ = form["GJZ"];
            gxxx.XNZC = form["XNZC"];
            gxxx.GKCL = form["GKCL"];
            gxxx.SYZT = ConvertUtility.ToDecimal(form["SYZT"]);
            gxxx.GKHS = ConvertUtility.ToDecimal(form["GKHS"]);
            gxxx.GKLS = ConvertUtility.ToDecimal(form["GCBH"]);
            gxxx.PLSX = ConvertUtility.ToDecimal(form["GCBH"]);
            gxxx.GXZT = form["GCBH"];
            gxxx.MODIFYBY = CurrentUser.UserName;
            gxxx.MODIFYTIME = DateTime.Now;
            gxxxBusiness.UpdateEntity(gxxx);
            ViewData["GxxxId"] = gxxx.ID;
            return Json(gxxxBusiness.SaveChange() > 0 ? AjaxResult.Success("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
        }

        /// <summary>
        /// 修改管线信息（管孔页面保存按钮操作）
        /// </summary>
        /// <returns></returns>
        public ActionResult EditGxxxByGkxxPage(FormCollection form)
        {
            var id = ConvertUtility.ToDecimal(form["Gxxxid"]);
            var model = gxxxBusiness.Find(id);
            if (model == null)
                return Json(AjaxResult.Error("请保存管线基本信息后再生成管孔信息"));
            model.GKCL = form["GXXXGKCL"];
            model.SYZT = ConvertUtility.ToDecimal(form["GXXXSYZT"]);
            model.GKLS = ConvertUtility.ToDecimal(form["GKLS"]);
            model.GKHS = ConvertUtility.ToDecimal(form["GKHS"]);
            model.PLSX = ConvertUtility.ToDecimal(form["PLSX"]);
            model.MODIFYBY = CurrentUser.UserName;
            model.MODIFYTIME = DateTime.Now;
            gxxxBusiness.UpdateEntity(model);
            return Json(gxxxBusiness.SaveChange() > 0 ? AjaxResult.Success("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
        }

        /// <summary>
        /// 保存管线信息(管线信息页面起止人井信息保存)
        /// </summary>
        /// <returns></returns>
        public ActionResult EditGxxx(FormCollection form)
        {
            try
            {
                var id = ConvertUtility.ToDecimal(form["Gxxxid"]);
                var model = gxxxBusiness.Find(id);
                if (model == null)
                    return Json(AjaxResult.Error("请保存管线基本信息后再保存管线起止井信息"));
                GX_GXXX gxxx = new GX_GXXX();
                gxxx = model;
                //起止井位置信息
                gxxx.QJ = form["Qjszwz"];
                gxxx.ZJ = form["Zjszwz"];
                //起止井所在管孔位置
                gxxx.QJGKWZ = form["gxszgkqjwz"];
                gxxx.ZJGKWZ = form["gxszgkwz"];
                gxxx.MODIFYBY = CurrentUser.UserName;
                gxxx.MODIFYTIME = DateTime.Now;
                gxxxBusiness.UpdateEntity(model);
                var ret = gxxxBusiness.SaveChange();
                return Json(ret > 0 ? AjaxResult.Success("数据操作成功") : AjaxResult.Error("数据操作失败"));
            }
            catch (Exception ex)
            {
                return Json(AjaxResult.Error("删除失败:" + (ex.InnerException != null ? ex.InnerException.Message : ex.Message)));
            }
        }

        /// <summary>
        /// 保存管孔信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditGkxx(FormCollection form)
        {
            if (string.IsNullOrEmpty(form["gkhsParent"]) || string.IsNullOrEmpty(form["gklsParent"]))
                AjaxResult.Error("请选择管孔所在位置！");
            var id = ConvertUtility.ToDecimal(form["GkxxId"]);
            var gkxx = gxxxBusiness.FindGkxx(id);
            if (gkxx == null)
                AjaxResult.Error("未找到需要更新的数据！");
            gkxx.GKBH = form["GKXX"];
            gkxx.GKCL = form["GKXXGKCL"];
            gkxx.SYZT = ConvertUtility.ToDecimal(form["GKXXSYZT"]);
            gkxx.ZKHS = ConvertUtility.ToDecimal(form["GKHS1"]);
            gkxx.ZKLS = ConvertUtility.ToDecimal(form["GKLS1"]);
            gkxx.ZKSL = ConvertUtility.ToDecimal(form["GXSL"]);
            gkxx.GKSZH = ConvertUtility.ToDecimal(form["gkhsParent"]);
            gkxx.GKSZL = ConvertUtility.ToDecimal(form["gklsParent"]);
            gkxx.MODIFYBY = CurrentUser.UserName;
            gkxx.MODIFYTIME = DateTime.Now;
            gxxxBusiness.UpdateGkxx(gkxx);
            return Json(gxxxBusiness.SaveChange() > 0 ? AjaxResult.Success("数据操作成功！") : AjaxResult.Error("数据操作失败！"));
        }

        /// <summary>
        /// 获取用户列表根据角色编号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserListByRoleId()
        {
            GxSysUserBusiness sysUserBusiness = new GxSysUserBusiness();
            var roleId = ConvertUtility.ToDecimal(Request["roleid"]);
            var list = sysUserBusiness.FindAllUserByRoleId(roleId).ToList();
            var listData = list.Select(c => new EasyuiDropDown { Value = c.ID.ToString(), Text = c.USERNAME }).ToList();
            listData.Insert(0, new EasyuiDropDown { Value = "0", Text = "请选择" });
            return Json(listData);
        }

        /// <summary>
        /// 获取线路中心列表根据角色编号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetXlzxListByRoleId()
        {
            GxXlzxBusiness sysUserBusiness = new GxXlzxBusiness();
            var roleId = ConvertUtility.ToDecimal(Request["roleid"]);
            var list = sysUserBusiness.FindAllModels().Where(t => t.ROLEID == roleId).ToList();
            var listData = list.Select(c => new EasyuiDropDown { Value = c.ID.ToString(), Text = c.NAME }).ToList();
            listData.Insert(0, new EasyuiDropDown { Value = "0", Text = "请选择" });
            return Json(listData);
        }

        /// <summary>
        /// 任务分配
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AllocatXlzx()
        {
            ViewData["gcxmid"] = ConvertUtility.ToString(Request["gcxmid"]);
            return PartialView();
        }
    }
}
