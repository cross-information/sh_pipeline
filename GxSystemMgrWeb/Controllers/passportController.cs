using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Gx.CommonUnits;
using Gx.Business;
using Gx.Models.ObjectBiz;

namespace GxSystemMgrWeb.Controllers
{
    public class passportController : Controller
    {
        //
        // GET: /passport/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form) {
            string loginName = form.Get("LoginName");
            string loginPass = form.Get("loginPass");
            AjaxResult result = null;
            if (string.IsNullOrEmpty(loginName))
            {
                result = AjaxResult.Error("用户名不能为空");
                return Json(result);
            }
            if (string.IsNullOrEmpty(loginPass))
            {
                result = AjaxResult.Error("密码不能为空");
                return Json(result);
            }
            GxSysUserBusiness gxSysUserBusiness = new GxSysUserBusiness();
            var entity = gxSysUserBusiness.FindGxSysUser(loginName, loginPass);
            result = AjaxResult.Error(entity.Message);
            return Json(result);
        }

        public ActionResult LogOut()
        { 
            try
            {
                HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                FormsAuthentication.SignOut();

                cookie = new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty);
                cookie.Expires = DateTime.Now.AddHours(-1);
                cookie.Path = FormsAuthentication.FormsCookiePath;
                if (System.Configuration.ConfigurationManager.AppSettings["HttpCookieDomain"] != null)
                {
                    cookie.Domain = System.Configuration.ConfigurationManager.AppSettings["HttpCookieDomain"].ToString();
                    System.Web.HttpContext.Current.Response.AddHeader("p3p", "CP=\"CAO PSA OUR\"");
                }
                Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                Response.Cookies.Set(cookie);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Login", "Passport");
        }
    }
}
