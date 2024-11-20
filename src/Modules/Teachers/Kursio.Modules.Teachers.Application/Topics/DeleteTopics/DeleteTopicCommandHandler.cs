using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Topics;

namespace Kursio.Modules.Teachers.Application.Topics.DeleteTopics;

internal sealed class DeleteTopicsCommandHandler(
    ITopicRepository topicRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteTopicsCommand>
{
    public async Task<Result> Handle(DeleteTopicsCommand request, CancellationToken cancellationToken)
    {
        await topicRepository.RemoveAllAsync(request.Ids, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
