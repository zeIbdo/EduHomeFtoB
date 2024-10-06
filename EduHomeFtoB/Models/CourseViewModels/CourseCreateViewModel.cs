namespace EduHomeFtoB.Models.CourseViewModels
{
    public class CourseCreateViewModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public decimal Fee { get; set; }
        public string Language { get; set; } = null!;
        public IFormFile CourseImage { get; set; }

    }
}
