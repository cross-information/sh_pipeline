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
    
    public partial class gx_wfprocessinstance
    {
        public int Id { get; set; }
        public string ProcessGUID { get; set; }
        public string ProcessName { get; set; }
        public string Version { get; set; }
        public Nullable<int> AppinstanceId { get; set; }
        public string AppName { get; set; }
        public string AppInstanceCode { get; set; }
        public Nullable<short> ProcessState { get; set; }
        public Nullable<int> ParentProcessInstanceID { get; set; }
        public string ParentProcessGUID { get; set; }
        public Nullable<int> InvokedActivityInstanceID { get; set; }
        public string InvokedActivityGUID { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string CreatedByUserID { get; set; }
        public string CreatedByUserName { get; set; }
        public Nullable<System.DateTime> LastUpdatedDateTime { get; set; }
        public string LastUpdatedByUserID { get; set; }
        public string LastUpdatedByUserName { get; set; }
        public Nullable<System.DateTime> EndedDateTime { get; set; }
        public string EndedByUserID { get; set; }
        public string EndedByUserName { get; set; }
        public Nullable<byte> RecordStatusInvalid { get; set; }
        public byte[] RowVersionID { get; set; }
    }
}
