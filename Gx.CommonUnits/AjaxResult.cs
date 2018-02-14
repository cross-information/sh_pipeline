
namespace Gx.CommonUnits
{
    /// <summary>
    /// Ajax返回结果
    /// </summary>
    public class AjaxResult
    {
        private bool _iserror = false;

        /// <summary>
        /// 是否产生错误
        /// </summary>
        public bool IsError { get { return _iserror; } }

        /// <summary>
        /// 错误信息，或者成功信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 成功可能时返回的数据
        /// </summary>
        public object Data { get; set; }

        #region Error
        public static AjaxResult Error()
        {
            return new AjaxResult
            {
                _iserror = true
            };
        }
        public static AjaxResult Error(string message)
        {
            return new AjaxResult
            {
                _iserror = true,
                Message = message
            };
        }
        public static AjaxResult Error(object data,string message)
        {
            return new AjaxResult
            {
                _iserror = true,
                Message = message,
                Data = data
            };
        }
        #endregion

        #region Success
        public static AjaxResult Success()
        {
            return new AjaxResult
            {
                _iserror = false
            };
        }
        public static AjaxResult Success(string message)
        {
            return new AjaxResult
            {
                _iserror = false,
                Message = message
            };
        }
        public static AjaxResult Success(object data)
        {
            return new AjaxResult
            {
                _iserror = false,
                Data = data
            };
        }
        public static AjaxResult Success(object data, string message)
        {
            return new AjaxResult
            {
                _iserror = false,
                Data = data,
                Message = message
            };
        }
        #endregion
    }
}
