using Kursio.Modules.Students.Domain.Students;
using Kursio.Modules.Students.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Kursio.Modules.Students.Infrastructure.Students;

internal sealed class StudentRepository(StudentsDbContext dbContext) : IStudentRepository
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

    public void Update(Student student)
    {
        dbContext.Update(student);
    }

    public async Task RemoveAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        await dbContext.Students
            .Where(student => ids.Contains(student.Id))
            .ExecuteDeleteAsync(cancellationToken);
    }
}
