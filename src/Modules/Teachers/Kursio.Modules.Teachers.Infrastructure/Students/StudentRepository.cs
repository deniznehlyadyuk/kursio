using Kursio.Modules.Teachers.Domain.Courses;
using Kursio.Modules.Teachers.Infrastructure.Database;

namespace Kursio.Modules.Teachers.Infrastructure.Students;
internal sealed class StudentRepository(TeachersDbContext dbContext) : IStudentRepository
{
    public async Task<Student?> FindAsync(Guid id)
    {
        return await dbContext.Students.FindAsync(id);
    }

    public async Task InsertAsync(Student student, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(student, cancellationToken);
    }

    public void Remove(Student student)
    {
        dbContext.Remove(student);
    }
}
