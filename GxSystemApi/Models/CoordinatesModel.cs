using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GxSystemApi.Models
{
    public class CoordinatesModel
    {
        public decimal Id { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }
    }
}