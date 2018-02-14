using System;
using System.Linq;
using Gx.DataAccess.Repository;
using Gx.Models; 

namespace Gx.DataAccess.BaseRepositiory
{
    public class BaseDataRepository : RepositoryBase<GX_DICT_CONFIG>
    {
        #region 构造函数
        public BaseDataRepository()
        {

        }
        public BaseDataRepository(UnitOfWork obj)
        {
            base.context = obj.Context;
        }
        #endregion

        /// <summary>
        /// 查询所在区县
        /// </summary>
        /// <param name="qxdm"></param>
        /// <returns></returns>
        public GX_DICT_CONFIG GetDictConfig(string qxdm)
        {
            GX_DICT_CONFIG gxDictConfig = null;
            try
            {
                gxDictConfig = context.Set<GX_DICT_CONFIG>().Select(a => a).FirstOrDefault(a => a.NAME == qxdm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return gxDictConfig;
        } 
    }
}
