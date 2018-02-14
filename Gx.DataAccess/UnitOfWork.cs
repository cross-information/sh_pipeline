using System.Web;

namespace Gx.DataAccess
{
    public class UnitOfWork
    {
        private GxSysEntities _context;
        private UnitOfWork _uow;

        public GxSysEntities Context
        {
            get { return _context; }
        }

        public void Dispose()
        {
            HttpContext.Current.Items["GxSysEntities"] = null;
        }

        public static UnitOfWork Instance
        {
            get
            {
                // For unit test
                if (HttpContext.Current == null)
                {
                    return new UnitOfWork();
                }
                var con = HttpContext.Current.Items["GxSysEntities"];
                if (con != null)
                {
                    return con as UnitOfWork;
                }
                else
                {
                    con = new UnitOfWork();
                    HttpContext.Current.Items["GxSysEntities"] = con;
                    return con as UnitOfWork;
                }
            }
        }

        /// <summary>
        /// 只用于异步调用，Web调用请使用Instance属性获取对象
        /// </summary>
        public UnitOfWork()
        {
            _context = new GxSysEntities();
        }

        /// <summary>
        /// 对一个单元内的所有操作一起提交更改
        /// </summary>
        /// <returns>影响行数</returns>
        public int Save()
        {
            return this._context.SaveChanges();
        }
    }
}
