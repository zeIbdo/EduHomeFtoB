using EduHomeFtoB.Models.Entities;

namespace EduHomeFtoB.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Course> Courses { get; set; }= new List<Course>();
    }
}
