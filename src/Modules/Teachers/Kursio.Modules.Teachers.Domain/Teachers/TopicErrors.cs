using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Teachers;

public static class TeacherErrors
{
    public static Error NotFound(Guid id)
    {
        return Error.NotFound("Teachers.NotFound", $"The teacher with the identifier {id} was not found");
    }

    public static Error CannotDeleteTeacherWithCourses(Guid id)
    {
        return Error.Problem("Teachers.CannotDeleteWithCourses", $"The teacher with the identifier {id} is associated with one or more courses and cannot be deleted.");
    }
}
