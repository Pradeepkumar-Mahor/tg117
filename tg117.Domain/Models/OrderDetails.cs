using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tg117.Domain.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public required OrderHeader OrderHeader { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public required Course Service { get; set; }

        [Required]
        public required string ServiceName { get; set; }

        [Required]
        public double Price { get; set; }
    }
}