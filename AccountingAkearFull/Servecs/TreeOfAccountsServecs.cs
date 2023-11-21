using Accouting.Data.Interfaces;
using Accouting.Models;
using Accouting.Models.ViewMessage;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Accouting.Servecs
{
    public class TreeOfAccountsServecs
    {
        private IUnitOfWork<TreeOfAccounts> _unitOfwork;
        public TreeOfAccountsServecs(IUnitOfWork<TreeOfAccounts> unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }
        public async Task<IEnumerable<TreeOfAccounts>> GetAllTreeOfAccounts()
        {
            return await _unitOfwork.Entity.Find(x => x.IsDeleted == false).ToListAsync();
        }
        public async Task<ResponseViewModel> AddTreeOfAccounts(TreeOfAccounts treeOfAccounts)
        {
            if (_unitOfwork.Entity.Find(x => x.NumberAcc == treeOfAccounts.NumberAcc).Count() > 0)
            {
                return new ResponseViewModel { State = false, Message = "الرقم الحساب موجود سابقا" };
            }
            _unitOfwork.Entity.Insert(new TreeOfAccounts()
            {
                NumberAcc = treeOfAccounts.NumberAcc,
              
            });
            await _unitOfwork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحفظ بنجاح" };
        }
        public async Task<ResponseViewModel> DeleteTreeOfAccounts(int Id)
        {
            var oldTreeOfAccounts = await _unitOfwork.Entity.GetByIdAsync(Id);
            if (oldTreeOfAccounts == null)
            {
                return new ResponseViewModel { State = false, Message = "الرقم الحساب غير موجود" };
            }
            oldTreeOfAccounts.IsDeleted = true;
            _unitOfwork.Entity.Update(oldTreeOfAccounts);
            await _unitOfwork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحذف بنجاح" };
        }
        public async Task<TreeOfAccounts> GetTreeOfAccountsByIdAsync(int Id)
        {
            return await _unitOfwork.Entity.GetByIdAsync(Id);

        }
        public async Task<ResponseViewModel> EditTreeOfAccounts(int id, TreeOfAccounts treeOfAccounts)
        {
            var oldtreeOfAccounts = await _unitOfwork.Entity.GetByIdAsync(id);
            if (oldtreeOfAccounts == null)
            {
                return new ResponseViewModel { State = false, Message = "الرقم الحساب غير موجود" };
            }
            if (_unitOfwork.Entity.Find(x => x.NumberAcc == treeOfAccounts.NumberAcc  && x.Id != id).Count() > 0)
            {
                return new ResponseViewModel { State = false, Message = "الرقم الإشاري موجود سابقا" };
            }
            oldtreeOfAccounts.NumberAcc = treeOfAccounts.NumberAcc;
            _unitOfwork.Entity.Update(oldtreeOfAccounts);
            await _unitOfwork.SaveAsync();
            return new ResponseViewModel { State = true, Message = "تم الحفظ بنجاح" };
        }
        public async Task<TreeOfAccounts?> GetLastTreeOfAccounts()
        {
            return await _unitOfwork.Entity.GetAll().OrderBy(x => x.Id).LastOrDefaultAsync();
        }
    }
}

