﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tg117.Domain.Models
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public string Categories { get; set; } = string.Empty;

        public string CourseBy { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }

        public int Duration { get; set; }

        [Display(Name = "Description")]
        public string LongDesc { get; set; } = string.Empty;

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public required Category Category { get; set; }

        [Required]
        public Guid FrequencyId { get; set; }

        [ForeignKey("FrequencyId")]
        public required Frequency Frequency { get; set; }
    }
}