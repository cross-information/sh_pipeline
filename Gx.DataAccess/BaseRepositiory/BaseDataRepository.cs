using System;
using System.Linq;
using Gx.DataAccess.Repository;
using Gx.Models; 

namespace Gx.DataAccess.BaseRepositiory
{
    public class BaseDataRepository : RepositoryBase<gx_dict_config>
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
        public gx_dict_config GetDictConfig(string qxdm)
        {
            gx_dict_config gxDictConfig = null;
            try
            {
                gxDictConfig = context.Set<gx_dict_config>().Select(a => a).FirstOrDefault(a => a.name == qxdm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return gxDictConfig;
        } 
    }
}
