using Accouting.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accouting.Moudels
{
    public class MakeJournalBody
    {
        public int Id { get; set; }
        public string? Details { get; set; }
        public decimal Debit { get; set; } 
        public decimal Credit { get; set; } 
        public decimal Blance { get; set; } 
        /// <summary>
        /// الميزانية - قائمة الدخل
        /// </summary>
        public bool IsBudgetProfit { get; set; }

        public int MakeJournalHeadId { get; set; }
        public MakeJournalHead? makejournalHeads { get; set; }

        public int AccountID { get; set; }
        public Accounts? Accounts { get; set; }

        public int CostCenterID { get; set; }
        public CostCenter? costCenters { get; set; }

    }
}
