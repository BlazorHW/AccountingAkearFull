

using Accouting.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Accouting.Moudels
{
    public class Accounts
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100,ErrorMessage ="يجب ادخال اسم حساب")]
        public string NameAccount { get; set; }

        [Required( ErrorMessage ="يجب ادخال رقم حساب")]
        public int NumberAccount { get; set; }

        /// <summary>
        /// حساب فرعي -حساب رئيسي
        /// </summary>
        public bool SubAccounts { get; set; }

        [StringLength(300)]
        public string? Description { get; set; } 
        public decimal? OpeningBlance { get; set; }

        /// <summary>
        /// الميزانية - قائمة الدخل
        /// </summary>
        public bool IsBudgetProfit { get; set; }
    
        public int? AccountTypeId { get; set; }
        [ForeignKey("AccountTypeId")]
        public AccountType? AccountTypes { get; set; }
        

    }
}
