using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tg117.Domain.Models
{
    public class OrderDetails
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public required OrderHeader OrderHeader { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public required Course Service { get; set; }

        [Required]
        public string ServiceName { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }
    }
}