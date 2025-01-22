using System.ComponentModel.DataAnnotations;

namespace tg117.Domain
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}