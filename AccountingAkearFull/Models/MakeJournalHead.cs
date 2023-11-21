using Accouting.Models;
using System.ComponentModel.DataAnnotations;

namespace Accouting.Moudels
{
    public class MakeJournalHead
    {
        [Key]
        public int Id { get; set; } 
        public int EntryJournalId { get; set; } 
        public DateTime date { get; set; } = DateTime.Now;
        [StringLength(250)]
        public string? Description { get; set; }
      
        public ICollection<MakeJournalBody>? makeJournalBodys { get; set; } = new HashSet<MakeJournalBody>();
        
    }
}
