using Accouting.Data.Interfaces;
using Accouting.Data.UnitOfWork;
using Accouting.Models.ViewMessage;
using Accouting.Moudels;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Accouting.Servecs
{
    public class AccountSereves
    {
        private IUnitOfWork<Accounts> _unitOfWork;


        public AccountSereves(IUnitOfWork<Accounts> unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Accounts>> GetAllAccounts()
        {
            return await _unitOfWork.Entity.GetAll().ToListAsync();
        }

        public async Task<ResponseViewModel> AddAccount(Accounts accounts)
        {

            if (_unitOfWork.Entity.Find(x => x.NameAccount == accounts.NameAccount || x.NumberAccount == accounts.NumberAccount).Count() > 0)
            {
                return new ResponseViewModel { State = false, Message = "الاسم او الرقم الحساب موجود سابقا" };
            }

            _unitOfWork.Entity.Insert(new Accounts()
            {
                NumberAccount = accounts.NumberAccount,
               NameAccount = accounts.NameAccount,
               OpeningBlance = accounts.OpeningBlance,
               Description = accounts.Description,
               IsBudgetProfit = accounts.IsBudgetProfit,
               //IsProfit = accounts.IsProfit,
             
            });
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحفظ بنجاح" };
        }
        public async Task<ResponseViewModel> DeleteAccount(int id)
        {
            _unitOfWork.Entity.Delete(id);
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحدف بنجاح" };
        }
        public async Task<Accounts> GetAccountByIdAsync(int Id)
        {
            return await _unitOfWork.Entity.GetByIdAsync(Id);
        }

        public async Task<ResponseViewModel> EditAccount(int id, Accounts accounts)
        {
            var OldAccount = await _unitOfWork.Entity.GetByIdAsync(id);
            if (OldAccount == null)
            {
                return new ResponseViewModel { State = true, Message = "الحساب غير موجود" };
            }
            if (_unitOfWork.Entity.Find(x => x.NumberAccount == accounts.NumberAccount && x.Id != id ).Count()>0 )
            {
                return new ResponseViewModel { State = false, Message = "الحساب موجود مسبقا" };
            }
            OldAccount.NameAccount = accounts.NameAccount;
            OldAccount.NumberAccount = accounts.NumberAccount;
            OldAccount.SubAccounts = accounts.SubAccounts;
            OldAccount.AccountTypes = accounts.AccountTypes;
            OldAccount.Description = accounts.Description;
            OldAccount.OpeningBlance = accounts.OpeningBlance;
            OldAccount.IsBudgetProfit = accounts.IsBudgetProfit;
            //OldAccount.IsProfit = accounts.IsProfit; 
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم التعديل بنجاح" };
        }
        public async Task<Accounts?> GetLastIncoming()
        {

            return await _unitOfWork.Entity.GetAll().OrderBy(x => x.Id).LastOrDefaultAsync();
        }
    }
}
