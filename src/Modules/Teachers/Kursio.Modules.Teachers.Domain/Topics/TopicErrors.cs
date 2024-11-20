using Kursio.Common.Domain;

namespace Kursio.Modules.Teachers.Domain.Topics;

public static class TopicErrors
{
    public static Error DuplicateName(string name)
    {
        return Error.Problem("Topics.DuplicateName", $"A topic with the name '{name}' already exists. Please choose a different name.");
    }

    public static Error NotFound(Guid id)
    {
        return Error.NotFound("Topics.NotFound", $"The topic with the identifier {id} was not found");
    }

    public static Error CannotDeleteTopicWithCourses(Guid topicId)
    {
        return Error.Problem("Topics.CannotDeleteWithCourses", $"The topic with the identifier {topicId} is associated with one or more courses and cannot be deleted.");
    }
}
