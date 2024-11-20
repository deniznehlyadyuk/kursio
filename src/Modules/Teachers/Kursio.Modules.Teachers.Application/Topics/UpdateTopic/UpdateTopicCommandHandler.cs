using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Topics;

namespace Kursio.Modules.Teachers.Application.Topics.UpdateTopic;
internal sealed class UpdateTopicCommandHandler(
    ITopicRepository topicRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateTopicCommand>
{
    public async Task<Result> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
    {
        Topic? duplicateTopic = await topicRepository.FindByNameAsync(request.Name, cancellationToken);

        if (duplicateTopic is not null && duplicateTopic.Id != request.Id)
        {
            return Result.Failure<Guid>(TopicErrors.DuplicateName(request.Name));
        }

        Topic? topic = await topicRepository.FindAsync(request.Id);

        if (topic is null)
        {
            return Result.Failure(TopicErrors.NotFound(request.Id));
        }

        topic.Update(request.Name);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
