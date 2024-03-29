﻿using System.ComponentModel.DataAnnotations;

namespace ASPNET_WebApp.Infrastructure.Data
{
    public class Anime
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Required]
        [Range(0.0, 10.0)]
        public double Score { get; set; }

        [StringLength(30)]
        public string Aired { get; set; }

        [Range(0,10000)]
        public int Episodes { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Studios { get; set; }

        [Required]
        [StringLength(200)]
        public string Producers { get; set; }

        [Required]
        [StringLength(30)]
        public string Duration { get; set; }

        [Required]
        [StringLength(50)]
        public string Rating { get; set; }

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}