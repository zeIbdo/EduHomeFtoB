﻿using System.ComponentModel.DataAnnotations;

namespace EduHomeFtoB.Models;

public class CourseCreateViewModel
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
    public string Duration { get; set; } = null!;
    [Range(0, double.MaxValue)]
    public decimal Fee { get; set; }
    public DateTime StartDate { get; set; }

    public IFormFile CourseImage { get; set; }
    public int CategoryId { get; set; }
}
