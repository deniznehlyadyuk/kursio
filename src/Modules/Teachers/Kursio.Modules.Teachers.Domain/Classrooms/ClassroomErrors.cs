using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Classrooms;

public static class ClassroomErrors
{
    public static Error DuplicateName(string name)
    {
        return Error.Problem("Classrooms.DuplicateName", $"A classroom with the name '{name}' already exists. Please choose a different name.");
    }

    public static Error NotFound(Guid id)
    {
        return Error.NotFound("Classrooms.NotFound", $"The classroom with the identifier {id} was not found");
    }

    public static Error CannotDeleteClassroomWithCourses(Guid id)
    {
        return Error.Problem("Classrooms.CannotDeleteWithCourses", $"The classroom with the identifier {id} is associated with one or more courses and cannot be deleted.");
    }
}
