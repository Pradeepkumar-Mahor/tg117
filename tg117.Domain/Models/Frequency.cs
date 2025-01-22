using System.ComponentModel.DataAnnotations;

namespace tg117.Domain
{
    public class Frequency
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int FrequencyCount { get; set; }

        [Required]
        [Display(Name = "Frequency Name")]
        public string Name { get; set; } = string.Empty;
    }
}