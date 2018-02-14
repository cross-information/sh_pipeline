using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.CommonUnits.Tools
{
    public class Common
    {
        /// <summary>
        /// 获取随机产生的随机数
        /// </summary>
        /// <returns></returns>
        public decimal GetRandom()
        {
            var random = new Random();
            var retRandom = Convert.ToString(random.Next(100, 1000));
            var timeRand = DateTime.Now.ToString("HHmmss");
            var returnRandom = Convert.ToString(random.Next(10, 100)) + timeRand + "" + retRandom;
            return Convert.ToDecimal(returnRandom);
        }
    }

    public class EasyuiDropDown
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
