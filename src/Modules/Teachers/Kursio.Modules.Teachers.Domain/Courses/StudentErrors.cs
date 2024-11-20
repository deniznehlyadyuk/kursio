using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Courses;

public static class StudentErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Students.NotFound", $"The student with the identifier {id} was not found");
}
