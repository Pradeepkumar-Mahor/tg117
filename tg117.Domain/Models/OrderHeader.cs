using System.ComponentModel.DataAnnotations;

namespace tg117.Domain.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required string City { get; set; }

        [Required]
        public required string ZipCode { get; set; }

        public DateTime OrderDate { get; set; }
        public required string Status { get; set; }
        public required string Comments { get; set; }
        public int ServiceCount { get; set; }
    }
}