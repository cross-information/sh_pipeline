//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gx.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class gx_sys_role
    {
        public int Id { get; set; }
        public string roleName { get; set; }
        public string roleDesc { get; set; }
        public Nullable<int> roleStatus { get; set; }
        public string createby { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
        public string modify { get; set; }
        public Nullable<System.DateTime> modifytime { get; set; }
    }
}