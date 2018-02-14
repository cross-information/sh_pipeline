using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Security;
using Gx.CommonUnits;
using Gx.Models.ObjectBiz;

namespace GxSystemMgrWeb.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 验证用户是否登录
        /// 验证访问的Action是否满足当前执行事件的要求
        /// </summary>
        /// <param name="filterContext">未登陆或登录异常，页面跳转到登录页面</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            var userCachEntity = UserFormsPrincipal<UserEntity<UserInfo>>.TryParsePrincipal(System.Web.HttpContext.Current.Request);
            if (!HttpContext.User.Identity.IsAuthenticated || userCachEntity == null)
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
                System.Web.HttpContext.Current.Response.Redirect("/passport/login");
            } 
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        public UserInfo CurrentUser
        {
            get
            {
                var userCachEntity = UserFormsPrincipal<UserEntity<UserInfo>>.TryParsePrincipal(System.Web.HttpContext.Current.Request);
                return userCachEntity.UserData == null ? null : userCachEntity.UserData.UserInfo;
            }
        }
    }
}
