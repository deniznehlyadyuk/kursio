namespace Kursio.Modules.Students.Domain.Students;

public interface IStudentRepository
{
    Task InsertAsync(Student student, CancellationToken cancellationToken = default);
    Task<Student?> FindAsync(Guid id);
    void Update(Student student);
    void Remove(Student student);
    Task RemoveAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
