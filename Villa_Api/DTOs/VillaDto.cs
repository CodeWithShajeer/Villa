using System.ComponentModel.DataAnnotations;

namespace Villa_Api.DTOs
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public int Occupency { get; set; }
        public int Sqft { get; set; }
    }
}
