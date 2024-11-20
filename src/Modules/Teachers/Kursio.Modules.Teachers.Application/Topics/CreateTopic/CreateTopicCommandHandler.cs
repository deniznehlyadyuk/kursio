using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Topics;

namespace Kursio.Modules.Teachers.Application.Topics.CreateTopic;

internal sealed class CreateTopicCommandHandler(
    ITopicRepository topicRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateTopicCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        Topic? duplicateTopic = await topicRepository.FindByNameAsync(request.Name, cancellationToken);

        if (duplicateTopic is not null)
        {
            return Result.Failure<Guid>(TopicErrors.DuplicateName(request.Name));
        }

        var topic = Topic.Create(request.Name);

        await topicRepository.InsertAsync(topic, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return topic.Id;
    }
}
