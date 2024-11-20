namespace Kursio.Modules.Teachers.Domain.Teachers;
public interface ITeacherRepository
{
    Task InsertAsync(Teacher teacher, CancellationToken cancellationToken = default);
    Task<Teacher?> FindAsync(Guid id);
    void Remove(Teacher teacher);
    Task RemoveAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
