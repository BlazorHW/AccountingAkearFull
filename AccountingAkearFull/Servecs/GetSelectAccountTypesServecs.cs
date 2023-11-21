using Accouting.Data.Interfaces;
using Accouting.Models;
using Accouting.Models.ViewMessage;
using Accouting.Moudels;
using Microsoft.EntityFrameworkCore;


namespace Accouting.Servecs
{
    public class GetSelectAccountTypesServecs
    {
        private IUnitOfWork<AccountType> _unitOfWork;
        public GetSelectAccountTypesServecs(IUnitOfWork<AccountType> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AccountType>> GetAllAccountType()
        {
            return await _unitOfWork.Entity.GetAll().ToListAsync();
        }

        public async Task<ResponseViewModel> AddAccount(AccountType accounts)
        {
            if (_unitOfWork.Entity.Find(x => x.Accounttypes == accounts.Accounttypes).Count() > 0)
            {
                return new ResponseViewModel { State = false, Message = "الاسم او الرقم الحساب موجود سابقا" };
            }
            _unitOfWork.Entity.Insert(new AccountType()
            {
                Accounttypes = accounts.Accounttypes,
          
            });
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحفظ بنجاح" };
        }
        //public async Task<ViewMessages> AddAccountType(AccountType accountType)
        //{
        //    if (_unitOfWork.Entity.Find(x => x.Accounttypes == accountType.Accounttypes).Count() > 0)
        //    {
        //        return new ViewMessages { State = false, Message = "الاسم  تكلفة موجود سابقا" };
        //    }
        //    _unitOfWork.Entity.Insert(new AccountType()
        //    {
        //        Accounttypes = accountType.Accounttypes,
        //    });
        //    await _unitOfWork.SaveAsync();
        //    return new ViewMessages { State = true, Message = "تم الحفظ بنجاح" };
        //}


    }
}

