namespace Kursio.Modules.Teachers.Domain.Courses;
public interface IStudentRepository
{
    Task<Student?> FindAsync(Guid id);
    Task InsertAsync(Student student, CancellationToken cancellationToken = default);
    void Remove(Student student);
}
