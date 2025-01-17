using System.ComponentModel.DataAnnotations;

namespace tg117.Domain
{
    public class Frequency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FrequencyCount { get; set; }

        [Required]
        [Display(Name = "Frequency Name")]
        public required string Name { get; set; }
    }
}