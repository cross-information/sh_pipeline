using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gx.DataAccess;
using Gx.DataAccess.Repository;
using Gx.Models;

namespace Gx.Business
{
    public class GxDictConfigBusiness
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public void AddEntity(GX_DICT_CONFIG dictConfig)
        {
            GxDictConfigRepository dictConfigRepository = new GxDictConfigRepository(unitOfWork);
            dictConfigRepository.AddEntity(dictConfig);
        }

        public void UpdateEntity(GX_DICT_CONFIG dictConfig)
        {
            GxDictConfigRepository dictConfigRepository = new GxDictConfigRepository(unitOfWork);
            dictConfigRepository.UpdateEntity(dictConfig);
        }

        public void DeleteEntity(GX_DICT_CONFIG dictConfig)
        {
            GxDictConfigRepository dictConfigRepository = new GxDictConfigRepository(unitOfWork);
            dictConfigRepository.DeleteEntity(dictConfig);
        }

        /// <summary>
        /// 根据ID获取人井信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GX_DICT_CONFIG FindDictConfig(decimal id)
        {
            GxDictConfigRepository dictConfigRepository = new GxDictConfigRepository(unitOfWork);
            return dictConfigRepository.FindRjxx(id);
        }

        /// <summary>
        /// 返回所有数据列表的最后一条数据
        /// </summary>
        /// <returns></returns>
        public GX_DICT_CONFIG FindDictConfig()
        {
            GxDictConfigRepository dictConfigRepository = new GxDictConfigRepository(unitOfWork);
            return dictConfigRepository.FindRjxx();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_DICT_CONFIG> FindAllDictConfig()
        {
            GxDictConfigRepository dictConfigRepository = new GxDictConfigRepository(unitOfWork);
            return dictConfigRepository.FindAllRjxx();
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_DICT_CONFIG> FindAllDictConfig(string name)
        {
            GxDictConfigRepository dictConfigRepository = new GxDictConfigRepository(unitOfWork);
            return dictConfigRepository.FindAllRjxx(name);
        }

        /// <summary>
        /// 返回所有管线信息数据集合
        /// </summary>
        /// <returns></returns>
        public List<GX_DICT_CONFIG> FindAllDictConfig(decimal id)
        {
            GxDictConfigRepository dictConfigRepository = new GxDictConfigRepository(unitOfWork);
            return dictConfigRepository.FindAllRjxx(id);
        }

        /// <summary>
        /// 返回分页数据列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<GX_DICT_CONFIG> FindAllGxDictConfig(string name, decimal parentId, int pageIndex, int pageSize, out int totalRecord)
        {
            GxDictConfigRepository dictConfigRepository = new GxDictConfigRepository(unitOfWork);
            return dictConfigRepository.FindAllGxDictConfig(name, parentId, pageIndex, pageSize, out totalRecord);
        }

        /// <summary>
        /// 执行数据保存操作
        /// </summary>
        /// <returns></returns>
        public int SaveChange()
        {
            return unitOfWork.Save();
        }
    }
}
