namespace Kursio.Modules.Students.Domain.Students;

public static class StudentCacheKeys
{
    public static string Student(Guid id) => $"student-{id}";
    public static readonly string Students = "students";
}
