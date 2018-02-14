using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.Models.ObjectBiz
{
    public class Menus
    {
        public Menus()
        {
            MenuList = new List<Menus>();
        }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public decimal Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单跳转的URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 菜单图片
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 当前菜单子菜单集合
        /// </summary>
        public List<Menus> MenuList { get; set; }
    }
}
