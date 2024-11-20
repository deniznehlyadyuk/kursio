namespace Kursio.Modules.Teachers.Domain.Classrooms;
public static class ClassroomCacheKeys
{
    public static string Classroom(Guid id)
    {
        return $"classroom-{id}";
    }
}
