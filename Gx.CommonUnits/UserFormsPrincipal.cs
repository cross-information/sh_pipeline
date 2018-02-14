using System;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Gx.CommonUnits
{
    public class UserFormsPrincipal<TUserData> : IPrincipal where TUserData : class, new()
    {
        private readonly IIdentity _identity;
        private readonly TUserData _userData;

        public UserFormsPrincipal(FormsAuthenticationTicket ticket, TUserData userData)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");
            if (userData == null)
                throw new ArgumentNullException("userData");

            _identity = new FormsIdentity(ticket);
            _userData = userData;
        }

        public TUserData UserData
        {
            get { return _userData; }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            // 把判断用户组的操作留给UserData去实现。

            var principal = _userData as IPrincipal;
            if (principal == null)
                throw new NotImplementedException();
            return principal.IsInRole(role);
        }
        /// <summary>
        /// 执行用户登录操作
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="userData">与登录名相关的用户信息</param>
        /// <param name="timeSpan">登录Cookie的过期时间，单位：分钟。</param>
        public static void SignIn(string loginName, TUserData userData, TimeSpan timeSpan)
        {
            if (string.IsNullOrEmpty(loginName))
                throw new ArgumentNullException("loginName");
            if (userData == null)
                throw new ArgumentNullException("userData");

            // 1. 把需要保存的用户数据转成一个字符串。
            var data = (new JavaScriptSerializer()).Serialize(userData);


            // 2. 创建一个FormsAuthenticationTicket，它包含登录名以及额外的用户数据。
            DateTime expires = DateTime.Now.AddMinutes(timeSpan.Minutes);

            var ticket = new FormsAuthenticationTicket(2, loginName, DateTime.Now, DateTime.Now.AddMinutes(timeSpan.Minutes), false, data);

            // 3. 加密Ticket，变成一个加密的字符串。
            string cookieValue = FormsAuthentication.Encrypt(ticket);

            // 4. 根据加密结果创建登录Cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath
            };
            if (System.Configuration.ConfigurationManager.AppSettings["HttpCookieDomain"] != null)
            {
                cookie.Domain = System.Configuration.ConfigurationManager.AppSettings["HttpCookieDomain"].ToString();
                HttpContext.Current.Response.AddHeader("p3p", "CP=\"CAO PSA OUR\"");

            }
            //cookie.Expires = expires;

            var context = HttpContext.Current;

            // 5. 写登录Cookie
            context.Response.Cookies.Remove(cookie.Name);
            context.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 尝试从HttpRequest.Cookies中构造一个UserFormsPrincipal对象
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UserFormsPrincipal<TUserData> TryParsePrincipal(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // 1. 读登录Cookie
            var cookie = request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return null;

            try
            {
                TUserData userData = null;
                // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                if (ticket != null && string.IsNullOrEmpty(ticket.UserData) == false)
                    // 3. 还原用户数据
                    userData = (new JavaScriptSerializer()).Deserialize<TUserData>(ticket.UserData);

                if (ticket != null && userData != null)
                    // 4. 构造我们的MyFormsPrincipal实例，重新给context.User赋值。
                    return new UserFormsPrincipal<TUserData>(ticket, userData);
            }
            catch { /* 有异常也不要抛出，防止攻击者试探。 */ }
            return null;
        }

        /// <summary>
        /// 根据HttpContext对象设置用户标识对象
        /// </summary>
        /// <param name="context"></param>
        public static void TrySetUserInfo(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            UserFormsPrincipal<TUserData> user = TryParsePrincipal(context.Request);
            if (user != null)
                context.User = user;
        }
    }
}
