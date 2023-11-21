using System.ComponentModel.DataAnnotations;

namespace Accouting.Models
{
    public class TreeOfAccounts
    {
        public int Id { get; set; }
        [StringLength(5, ErrorMessage = "هذا الحقل يحمل فقط 15 حرف")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NumberAcc { get; set; }
      
        public bool IsDeleted { get; set; } = false;
    }
}
