using System.ComponentModel.DataAnnotations;

namespace Accouting.Moudels
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [StringLength(300)]
        public string NameCompany { get; set; }
        public int Phone { get; set; }
        [StringLength(200)]
        public string Addres { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
    }
}
