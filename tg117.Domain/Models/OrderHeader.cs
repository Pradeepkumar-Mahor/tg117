﻿using System.ComponentModel.DataAnnotations;

namespace tg117.Domain.Models
{
    public class OrderHeader
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string ZipCode { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public int ServiceCount { get; set; }
    }
}