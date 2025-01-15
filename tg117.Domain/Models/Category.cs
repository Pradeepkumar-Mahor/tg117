using System.ComponentModel.DataAnnotations;

namespace tg117.Domain
{
    public class Category
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}