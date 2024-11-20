using Kursio.Modules.Teachers.Domain.Topics;
using Kursio.Modules.Teachers.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Kursio.Modules.Teachers.Infrastructure.Topics;
internal sealed class TopicRepository(TeachersDbContext dbContext) : ITopicRepository
{
    public async Task InsertAsync(Topic topic, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(topic, cancellationToken);
    }

    public async Task<Topic?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await dbContext.Topics.FirstOrDefaultAsync(t => t.Name == name, cancellationToken);
    }

    public async Task<Topic?> FindAsync(Guid id)
    {
        return await dbContext.Topics.FindAsync(id);
    }

    public void Remove(Topic topic)
    {
        dbContext.Remove(topic);
    }

    public async Task RemoveAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        await dbContext.Topics
            .Where(topic => ids.Contains(topic.Id) && topic.AssociatedCourseCount == 0)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
