using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gx.Models.ViewModels
{
    public class ViewDictConfigModel
    {
        public decimal? Id { get; set; }
        public string Name { get; set; }
        public decimal? ParentId { get; set; }
        public string ParentName { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Status { get; set; }

        public string DictDesc { get; set; }
    }
}
