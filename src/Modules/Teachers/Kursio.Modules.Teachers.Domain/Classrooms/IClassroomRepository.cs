
namespace Kursio.Modules.Teachers.Domain.Classrooms;

public interface IClassroomRepository
{
    Task InsertAsync(Classroom classroom, CancellationToken cancellationToken = default);
    Task<Classroom?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Classroom?> FindAsync(Guid id);
    void Remove(Classroom classroom);
    Task RemoveAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
