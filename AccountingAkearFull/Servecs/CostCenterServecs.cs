using Accouting.Data.Interfaces;
using Accouting.Models;
using Accouting.Models.ViewMessage;
using Accouting.Moudels;
using Microsoft.EntityFrameworkCore;

namespace Accouting.Servecs
{
    public class CostCenterServecs
    {
        private IUnitOfWork<CostCenter> _unitOfWork;
        public CostCenterServecs(IUnitOfWork<CostCenter> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CostCenter>> GetAllCostCenter()
        {
            return await _unitOfWork.Entity.GetAll().ToListAsync();
        }

        public async Task<ResponseViewModel> AddCostCenter(CostCenter costCenters)
        {
            if (_unitOfWork.Entity.Find(x => x.NameCostCenter == costCenters.NameCostCenter).Count() > 0)
            {
                return new ResponseViewModel { State = false, Message = "الاسم  تكلفة موجود سابقا" };
            }
            _unitOfWork.Entity.Insert(new CostCenter()
            {
                NameCostCenter = costCenters.NameCostCenter,
                NumberCostCenter = costCenters.NumberCostCenter,
               
            });
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحفظ بنجاح" };
        }
        public async Task<ResponseViewModel> DeleteCostCenter(int id)
        {
            _unitOfWork.Entity.Delete(id);
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحدف بنجاح" };
        }
        public async Task<CostCenter> GetCostCenterByIdAsync(int Id)
        {
            return await _unitOfWork.Entity.GetByIdAsync(Id);
        }
        public async Task<ResponseViewModel> EditAccount(int id, CostCenter costCenters)
        {
            var OldCostCenter = await _unitOfWork.Entity.GetByIdAsync(id);
            if (OldCostCenter == null)
            {
                return new ResponseViewModel { State = true, Message = "الحساب غير موجود" };
            }
            if (_unitOfWork.Entity.Find(x => x.NameCostCenter == costCenters.NameCostCenter  && x.Id != id).Count() > 0)
            {
                return new ResponseViewModel { State = false, Message = "الحساب موجود مسبقا" };
            }
            OldCostCenter.NameCostCenter = costCenters.NameCostCenter;
            OldCostCenter.NumberCostCenter = costCenters.NumberCostCenter;
         
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم التعديل بنجاح" };
        }
    }
}

