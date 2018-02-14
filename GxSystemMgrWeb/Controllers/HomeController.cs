using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gx.Business;
using Gx.Models;
using Gx.Models.ObjectBiz;
using JGB.Common.Units;

namespace GxSystemMgrWeb.Controllers
{
    public class HomeController : BaseController
    {
        private GXSysRoleBusiness gxSysRoleBusiness = new GXSysRoleBusiness();
        private GxSysUserRoleBusiness gxSysUserRoleBusiness = new GxSysUserRoleBusiness();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var id = CurrentUser.Id;
            var models = gxSysUserRoleBusiness.FindRoleEntity(id);
            var roleModels = gxSysRoleBusiness.Find(models.ROLEID);
            ViewBag.UserName = roleModels.ROLENAME + "【" + CurrentUser.UserName + "】";
            return View();
        }

        /// <summary>
        /// 页面显示的菜单数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            var gxSysMenuBusniess = new GxSysMenuBusniess();
            #region 左边的菜单
            //获取左边菜单的数据列
            IList<RoleMenuBo> listBo = new List<RoleMenuBo>();
            listBo = gxSysMenuBusniess.GetMenuRole(CurrentUser.Id);
            var listMenu = new List<Menus>();
            int menuModel = 0;
            if (listBo.Count > 0)
            {
                for (int i = 0; i < listBo.Count; i++)
                {
                    //如果第一次循环完成，判断第二次是不是同一个大类，否则继续下一个循环
                    if (menuModel == ConvertUtility.ToInt(listBo[i].ModelId))
                    {
                        continue;
                    }
                    Menus menu = new Menus();
                    menu.Id = ConvertUtility.ToInt(listBo[i].MenuId);
                    menu.Name = listBo[i].ModelName;
                    menu.Url = "#";
                    menu.Icon = "";
                    menuModel = ConvertUtility.ToInt(listBo[i].ModelId);
                    IList<RoleMenuBo> menubo = new List<RoleMenuBo>();
                    int ModelId = menuModel;

                    menubo = listBo.Where(m => m.ParaMenuID == ModelId).ToList();
                    if (menubo.Count > 0)
                    {
                        for (int j = 0; j < menubo.Count; j++)
                        {
                            Menus menus = new Menus();
                            menus.Id = ConvertUtility.ToInt(menubo[j].MenuId);
                            menus.Name = menubo[j].MenuName;
                            menus.Url = menubo[j].LinkUrl;
                            menus.Icon = menubo[j].IconType_Menu;
                            menu.MenuList.Add(menus);
                        }
                    }
                    listMenu.Add(menu);
                }
            }
            return Json(new { menus = listMenu }, JsonRequestBehavior.AllowGet);
            #endregion
        }
    }
}
