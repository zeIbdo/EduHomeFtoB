using EduHomeFtoB.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace EduHomeFtoB.Models
{
    public class Course:BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; } 
        public string Duration { get; set; } = null!;
        [Range(0, double.MaxValue)]
        public decimal Fee { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
