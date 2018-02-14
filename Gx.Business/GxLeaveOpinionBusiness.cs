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
    public class GxLeaveOpinionBusiness
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public void Add(GX_LEAVEOPINION models)
        {
            GxLeaveOpinionRepository opinionRepository = new GxLeaveOpinionRepository(unitOfWork);
            opinionRepository.AddEntity(models);
        }

        public void Update(GX_LEAVEOPINION models)
        {
            GxLeaveOpinionRepository opinionRepository = new GxLeaveOpinionRepository(unitOfWork);
            opinionRepository.UpdateEntity(models);
        }

        public void Delete(GX_LEAVEOPINION models)
        {
            GxLeaveOpinionRepository opinionRepository = new GxLeaveOpinionRepository(unitOfWork);
            opinionRepository.DeleteEntity(models);
        }

        public int SaveChange()
        {
            return unitOfWork.Save();
        }
    }
}
