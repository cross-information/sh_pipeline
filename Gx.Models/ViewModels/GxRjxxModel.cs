using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.Models.ViewModels
{
    public class GxRjxxModel
    {
        public decimal ID { get; set; }
        public string GCBH { get; set; }
        public string GXWZ { get; set; }
        public Nullable<decimal> GXCD { get; set; }
        public string DXLX { get; set; }
        public string GXLX { get; set; }
        public string JSZT { get; set; }
        public string YWLX { get; set; }
        public string GJZ { get; set; }
        public string XNZC { get; set; }
        public string GKCL { get; set; }
        public Nullable<decimal> SYZT { get; set; }
        public Nullable<decimal> GKHS { get; set; }
        public Nullable<decimal> GKLS { get; set; }
        public Nullable<decimal> PLSX { get; set; }
        public string GXZT { get; set; }
        public string GH { get; set; }
        public Nullable<decimal> JJ { get; set; }
        public string STQK { get; set; }
        public string QJ { get; set; }
        public string ZJ { get; set; }

        public string GKBH { get; set; }
        public Nullable<decimal> ZKHS { get; set; }
        public Nullable<decimal> ZKLS { get; set; }
        public Nullable<decimal> ZKSL { get; set; }
        public Nullable<decimal> GKSZH { get; set; }
        public Nullable<decimal> GKSZL { get; set; }

        //起止井所在的位置（第二个tab页面选择起止井坐标）
        public string Qjwz { get; set; }
        public string Zjwz { get; set; }

        /// <summary>
        /// 工程项目编号
        /// </summary>
        public decimal? gcxmid { get; set; }
        /// <summary>
        /// 管孔信息编号
        /// </summary>
        public decimal GkxxId { get; set; }
    }
}
