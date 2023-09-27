﻿using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Commands;

public class AddEvidentionRecordCommand
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Class { get; set; }
    public int NumberOfLessons { get; set; }
    public string Issues { get; set; }
    public string LessonsTimes { get; set; }
}