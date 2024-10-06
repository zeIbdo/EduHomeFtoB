using EduHomeFtoB.Models.Entities;

namespace EduHomeFtoB.Models
{
    public class Course:BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public decimal Fee { get; set; }
        public string Language { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
