using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Topics;

namespace Kursio.Modules.Teachers.Application.Topics.DeleteTopic;

internal sealed class DeleteTopicCommandHandler(
    ITopicRepository topicRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteTopicCommand>
{
    public async Task<Result> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
    {
        Topic? topic = await topicRepository.FindAsync(request.Id);

        if (topic is null)
        {
            return Result.Failure(TopicErrors.NotFound(request.Id));
        }

        if (topic.AssociatedCourseCount > 0)
        {
            return Result.Failure(TopicErrors.CannotDeleteTopicWithCourses(request.Id));
        }

        topicRepository.Remove(topic);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
