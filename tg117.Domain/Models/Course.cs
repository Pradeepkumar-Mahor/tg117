using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tg117.Domain
{
    public class Course
    {
        [Key]
        public Guid Guid { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public required string CourseName { get; set; }

        [Required]
        public required string Categories { get; set; }

        public required string CourseBy { get; set; }

        [Required]
        public double Price { get; set; }

        public int Duration { get; set; }

        [Display(Name = "Description")]
        public required string LongDesc { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public required string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public required Category Category { get; set; }

        [Required]
        public int FrequencyId { get; set; }

        [ForeignKey("FrequencyId")]
        public required Frequency Frequency { get; set; }
    }
}