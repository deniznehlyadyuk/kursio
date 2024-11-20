using Kursio.Modules.Teachers.Domain.Teachers;
using Kursio.Modules.Teachers.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Kursio.Modules.Teachers.Infrastructure.Teachers;
internal sealed class TeacherRepository(TeachersDbContext dbContext) : ITeacherRepository
{
    public async Task InsertAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(teacher, cancellationToken); 
    }

    public async Task<Teacher?> FindAsync(Guid id)
    {
        return await dbContext.Teachers.FindAsync(id);
    }

    public void Remove(Teacher teacher)
    {
        dbContext.Remove(teacher);
    }

    public async Task RemoveAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        await dbContext.Teachers
            .Where(teacher => ids.Contains(teacher.Id) && teacher.AssociatedCourseCount == 0)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
