using System;
using System.Collections.Generic;
using System.IO;
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
    public class GxProjectController : BaseController
    {
        GxProjectBusiness gxProjectBusniess = new GxProjectBusiness();

        GxYszlfileBusiness gxYszlfileBusiness = new GxYszlfileBusiness();
        //
        // GET: /GxProject/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjectList(int page, int rows)
        {
            var htbj = ConvertUtility.ToString(Request["htbh"]);
            var gcbh = ConvertUtility.ToString(Request["gcbh"]);
            var gcmc = ConvertUtility.ToString(Request["gcmc"]);

            int totalRecord;
            var list = gxProjectBusniess.FindAllGxProject(htbj, gcbh, gcmc, page, rows, out totalRecord);
            var data = (
                from h in list
                select new
                {
                    h.HTBH,
                    h.GCBH,
                    h.GCMC,
                    h.SGDWMC,
                    h.JLDWDM,
                    h.YSZT,
                    h.HTSJ,
                    h.ID
                }
                ).ToList();
            return Json(new { total = totalRecord, rows = list });
        }

        /// <summary>
        /// 获取查看资料
        /// </summary>
        /// <returns></returns>
        public PartialViewResult FileView()
        {
            ViewData["gcxmid"] = ConvertUtility.ToDecimal(Request["gcxmid"]);
            return PartialView();
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetFileList(int page, int rows)
        {
            int totalRecord = 0;
            decimal gcxmid = ConvertUtility.ToDecimal(Request["gcxmid"]);
            var type = ConvertUtility.ToInt(Request["type"]);
            var list = gxYszlfileBusiness.FindAlXlzxList(type, gcxmid, page, rows, out totalRecord);
            return Json(new { total = totalRecord, rows = list });
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public PartialViewResult FileUpload()
        {
            ViewData["type"] = ConvertUtility.ToString(Request["type"]);
            ViewData["gcxmid"] = ConvertUtility.ToString(Request["gcxmid"]);
            return PartialView();
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FileUpload(FormCollection form)
        {
            HttpFileCollectionBase files = Request.Files;
            var file = files[0];
            if (file == null)
                return Json(AjaxResult.Error("未找到需要上传的图片"));
            if (file.ContentLength <= 0)
                return Json(AjaxResult.Error("未找到需要上传的图片"));
            var extension = Path.GetExtension(file.FileName);
            if (extension != null)
            {
                extension = extension.ToLower();
            }
            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".dwg")
            {
                //上传原图
                string folder = Server.MapPath("~/Upload/gx/");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string originalPath = folder + Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(originalPath);
                GX_YSZLFILE yszlfile = new GX_YSZLFILE();
                yszlfile.ID = new Common().GetRandom();
                yszlfile.TYPE = ConvertUtility.ToDecimal(Request["type"]);
                yszlfile.FILEURL = originalPath;
                yszlfile.XMYSXXID = ConvertUtility.ToDecimal(form["gcxmid"]);
                yszlfile.CREATEBY = CurrentUser.UserName;
                yszlfile.UPLOADTIME = DateTime.Now;
                yszlfile.FILEZT = "1";
                yszlfile.FILENAME = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
                yszlfile.FILESIZE = file.ContentLength;
                gxYszlfileBusiness.AddEntity(yszlfile);
                return Json(gxYszlfileBusiness.SaveChange() > 0 ? AjaxResult.Success("图片上传成功") : AjaxResult.Error("图片上传失败"));
            }
            else
            {
                return Json(AjaxResult.Error("上传的图片格式有误"));
            }
        }

        /// <summary>
        /// 图片下载
        /// </summary>
        /// <returns></returns>
        public FileContentResult DownLoadFile()
        {
            var id = ConvertUtility.ToDecimal(Request["id"]);
            var model = gxYszlfileBusiness.Find(id);

            byte[] fileContent = null;
            string mimeType = model.FILEURL.Substring(model.FILEURL.LastIndexOf('.'));
            string fileName = ""; 

            fileContent = SetImgToByte(model.FILEURL);
            fileName = model.FILENAME;

            return File(fileContent, mimeType, fileName + mimeType);
        }

        //UploadHelper.cs
        /// <summary>
        /// 将图片转化为长二进制
        /// </summary>
        /// <param name="imgPath"></param>
        /// <returns></returns>
        public static Byte[] SetImgToByte(string imgPath)
        {
            FileStream file = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            Byte[] byteData = new Byte[file.Length];
            file.Read(byteData, 0, byteData.Length);
            file.Close();
            return byteData;
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeletePic()
        {
            var model = gxYszlfileBusiness.Find(Convert.ToDecimal(Request["id"]));
            gxYszlfileBusiness.DeleteEntity(model);
            var ret = gxYszlfileBusiness.SaveChange();
            return Json(ret > 0 ? AjaxResult.Success("删除成功") : AjaxResult.Error("删除失败"));
        }

        public PartialViewResult TaskResignView()
        {
            ViewData["gcxmid"] = ConvertUtility.ToString(Request["gcxmid"]);
            var id = ConvertUtility.ToDecimal(Request["gcxmid"]);
            var model = gxProjectBusniess.Find(id);
            return PartialView(model);
        }

        /// <summary>
        /// 保存分配的线路中心人员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OprRw(FormCollection form)
        {
            var id = ConvertUtility.ToDecimal(form["gcxmid"]);
            var model = gxProjectBusniess.Find(id);
            if (model == null)
                return Json(AjaxResult.Error("未找到分配的项目"));
            //线路中心
            model.XLZX = form["CheckMan"];
            //分配的人员
            model.YSRY = form["LineServiceCenter"];
            gxProjectBusniess.Update(model);
            return Json(gxProjectBusniess.SaveChange() > 0 ? AjaxResult.Success("任务分配操作成功") : AjaxResult.Error("任务分配操作失败"));
        }

        /// <summary>
        /// 返回验收人员列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCheckManList()
        {
            string roleName = "验收人员";
            var list = gxProjectBusniess.GetUserList(roleName).ToList();
            var listData = list.Select(c => new EasyuiDropDown { Value = c.Id.ToString(), Text = c.UserName }).ToList();
            listData.Insert(0, new EasyuiDropDown { Value = "", Text = "请选择" });
            return Json(listData);
        }

        /// <summary>
        /// 返回管线中心人员列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLineCenterList()
        {
            string roleName = "线路维护中心";
            var list = gxProjectBusniess.GetUserList(roleName).ToList();
            var listData = list.Select(c => new EasyuiDropDown { Value = c.Id.ToString(), Text = c.UserName }).ToList();
            listData.Insert(0, new EasyuiDropDown { Value = "", Text = "请选择" });
            return Json(listData);
        }

        [HttpPost]
        public ActionResult Save()
        {
            try
            {
                var gcxmid = ConvertUtility.ToDecimal(Request["id"]);
                GxLeaveOpinionBusiness opinionBusiness = new GxLeaveOpinionBusiness();
                GX_LEAVEOPINION oLeaveopinion = new GX_LEAVEOPINION();
                oLeaveopinion.APPINSTANCEID = gcxmid;
                opinionBusiness.Add(oLeaveopinion);
                return Json(opinionBusiness.SaveChange() > 0 ? AjaxResult.Success("数据提交成功") : AjaxResult.Error("数据提交失败"));
            }
            catch (Exception ex)
            {
                return Json(AjaxResult.Error("数据提交失败:" + (ex.InnerException == null ? ex.Message : ex.InnerException.Message)));
            }
        }
    }
}
