using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Domain.Entities;

public class EvidentionRecord
{
    [Key]
    public string Ip { get; set; }
    public User Student { get; set; }
    public int NumberOfLessons { get; set; }
    public string LessonsTimes { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string Issues { get; set; }
    public EvidentionRecord(string ip, int numberOfLessons, string lessonsTimes, string issues)
    {
        Ip = ip;
        NumberOfLessons = numberOfLessons;
        LessonsTimes = lessonsTimes;
        Issues = issues;
    }

    public void SetStudent(User student)
    {
        Student = student;
    }
}