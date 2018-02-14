namespace Gx.Models.ObjectBiz
{
    //WebAPI 数据结果类
    public class DataResult<T>
    {
        public bool Result { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// 查询数据列表总条数
        /// </summary>
        public int TotalPage { get; set; }

        public T Data { get; set; }

        public DataResult()
        {

        }

        public DataResult(bool r, string e, string m)
        {
            Result = r;
            ErrorCode = e;
            Message = m;
        }

        public DataResult(bool r, string e, string m, T data, int tp)
        {
            Result = r;
            ErrorCode = e;
            Message = m;
            Data = data;
            TotalPage = tp;
        }
    }
}
