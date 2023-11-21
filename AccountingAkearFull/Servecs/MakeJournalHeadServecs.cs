using Accouting.Data.Interfaces;
using Accouting.Models.ViewMessage;
using Accouting.Moudels;
using Accouting.Servecs;
using Microsoft.EntityFrameworkCore;

namespace AccountsAkear.Servecs
{
    public class MakeJournalHeadServecs
    {
        private IUnitOfWork<MakeJournalHead> _unitOfWork;

        public MakeJournalHeadServecs(IUnitOfWork<MakeJournalHead> unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<IEnumerable<MakeJournalHead>> GetAllMakeJournalHeadAsync()
        {
            return await _unitOfWork.Entity.GetAll().ToListAsync();
        }

        public async Task<ResponseViewModel> AddMakeJournalHead(MakeJournalHead makeJourna)
        {
            //if (_unitOfWork.Entity.Find(x => x.Details == makeJourna.Details ).Count() > 0)
            //{
            //    return new ResponseViewModel { State = false, Message = "الاسم او الرقم الحساب موجود سابقا" };
            //}

            _unitOfWork.Entity.Insert(new MakeJournalHead()
            {
                date = makeJourna.date,
                Description = makeJourna.Description,
                makeJournalBodys = makeJourna.makeJournalBodys.Select(x => { x.Accounts = null; return x; }).ToList(),
                
            });
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحفظ بنجاح" };
        }
        public async Task<ResponseViewModel> DeleteMakeJournalHead(int id)
        {
            _unitOfWork.Entity.Delete(id);
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحدف بنجاح" };
        }
        public async Task<MakeJournalHead> GetMakeJournalHeadByIdAsync(int Id)
        {
            return await _unitOfWork.Entity.GetByIdAsync(Id);
        }
        public async Task<ResponseViewModel> EditMakeJournalHead(int id, MakeJournalHead makeJourna)
        {
            var OldmakeJourna = await _unitOfWork.Entity.GetByIdAsync(id);
            OldmakeJourna.date = makeJourna.date;
            OldmakeJourna.Description = makeJourna.Description;
            await _unitOfWork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم التعديل بنجاح" };
        }
    }
}

