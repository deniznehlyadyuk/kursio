
namespace Kursio.Modules.Teachers.Domain.Topics;

public interface ITopicRepository
{
    Task InsertAsync(Topic topic, CancellationToken cancellationToken = default);
    Task<Topic?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Topic?> FindAsync(Guid id);
    void Remove(Topic topic);
    Task RemoveAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
