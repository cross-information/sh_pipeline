using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.Models.ObjectBiz
{
    public class RoleMenuBo
    {
        /// <summary>
        /// 菜单大类编号
        /// </summary>
        public decimal ModelId { get; set; }

        /// <summary>
        /// 菜单大类名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单上级编号
        /// </summary>
        public decimal? ParaMenuID { get; set; }

        /// <summary>
        /// 菜单链接
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 大类排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public decimal MenuId { get; set; }

        /// <summary>
        /// 菜单的显示图标
        /// </summary>
        public string IconType_Menu { get; set; }

        /// <summary>
        /// 菜单大类的显示图标
        /// </summary>
        public string IconType_Model { get; set; }
    }
}
