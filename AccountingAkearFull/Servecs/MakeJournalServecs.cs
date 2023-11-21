using Accouting.Data.Interfaces;
using Accouting.Models;
using Accouting.Models.ViewMessage;
using Accouting.Moudels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace Accouting.Servecs
{
    public class MakeJournalServecs
    {
        private IUnitOfWork<MakeJournalBody> _unitOfWork;
     
        private IUnitOfWork<Accounts> _accounts;
        private AccountSereves _accountSereves;
        public MakeJournalServecs(IUnitOfWork<MakeJournalBody> unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<IEnumerable<MakeJournalBody>> GetAllMakeJournalsAsync()
        {
            return await _unitOfWork.Entity.GetAll().Include(x => x.costCenters).Include(x=>x.Accounts).ToListAsync();
        }

        public async Task<ResponseViewModel> AddMakeJournalBody(MakeJournalBody makeJourna)
        {
            //if (_unitOfWork.Entity.Find(x => x.Details == makeJourna.Details ).Count() > 0)
            //{
            //    return new ResponseViewModel { State = false, Message = "الاسم او الرقم الحساب موجود سابقا" };
            //}

            _unitOfWork.Entity.Insert(new MakeJournalBody()
            {
                Details = makeJourna.Details,
                Debit = makeJourna.Debit,
                Credit = makeJourna.Credit,
                AccountID = makeJourna.AccountID,
                CostCenterID = makeJourna.CostCenterID,
               
            });
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحفظ بنجاح" };
        }
        public async Task<ResponseViewModel> DeleteMakeJournalBody(int id)
        {
            _unitOfWork.Entity.Delete(id);
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحدف بنجاح" };
        }
        public async Task<MakeJournalBody> GetMakeJournalBodyByIdAsync(int Id)
        {
            return await _unitOfWork.Entity.GetByIdAsync(Id);
        }
        public async Task<ResponseViewModel> EditMakeJournalBody(int id, MakeJournalBody makeJourna)
        {
            var OldmakeJourna = await _unitOfWork.Entity.GetByIdAsync(id);
           
          
            OldmakeJourna.Details = makeJourna.Details;
            OldmakeJourna.Debit = makeJourna.Debit;
            OldmakeJourna.Credit = makeJourna.Credit;

            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم التعديل بنجاح" };
        }
    }
}

