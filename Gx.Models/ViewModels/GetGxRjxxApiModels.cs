using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.Models.ViewModels
{
    public class GetGxRjxxApiModels
    {
        public string Id { get; set; }
        public string Htbh { get; set; }
        public string Gcmc { get; set; }
        public string Gcbh { get; set; }
        public string Sgdwmc { get; set; }
        public string Jldwdm { get; set; }
        public decimal? Yszt { get; set; }
        public DateTime? Htsj { get; set; }
        public List<Gxxx> GxxxList { get; set; }
        public List<Rjxx> RjxxList { get; set; }
    }

    public class Gxxx
    {
        public string Gcbh { get; set; }
        public string Gxwz { get; set; }
        public decimal? Gxcd { get; set; }
        public string Dxlx { get; set; }
        public string Gxlx { get; set; }
        public string Jszt { get; set; }
        public string Ywlx { get; set; }
        public string Gjz { get; set; }
        public string Xnzc { get; set; }
        public string Gkcl { get; set; }
        public decimal? Syzt { get; set; }
        public decimal? Gkhs { get; set; }
        public decimal? Gkls { get; set; }
        public decimal? Plsx { get; set; }
        public string Gxzt { get; set; }
        public List<Gkxx> GkxxList { get; set; }
    }

    public class Rjxx
    {
        public string Gcbh { get; set; }
        public string Rjbh { get; set; }
        public string Rjmc { get; set; }
        public string Gcrjbh { get; set; }
        public decimal? Jgsl { get; set; }
        public string Dxlx { get; set; }
        public string Rsjcc { get; set; }
        public string Jglx { get; set; }
        public string Gjz { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Rjzt { get; set; }
    }

    public class Gkxx
    {
        public string Gkbh { get; set; }
        public string Gkcl { get; set; }
        public decimal? Syzt { get; set; }
        public decimal? Zkhs { get; set; }
        public decimal? Zkls { get; set; }
        public decimal? Zksl { get; set; }
        public decimal? Gkszh { get; set; }
        public decimal? Gkszl { get; set; }
    }
}
