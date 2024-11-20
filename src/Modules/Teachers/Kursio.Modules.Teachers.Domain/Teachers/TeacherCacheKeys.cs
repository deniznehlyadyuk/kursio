namespace Kursio.Modules.Teachers.Domain.Teachers;
public static class TeacherCacheKeys
{
    public static string Teacher(Guid id) => $"teacher-{id}";
}
