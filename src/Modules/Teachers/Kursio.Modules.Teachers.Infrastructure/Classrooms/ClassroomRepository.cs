using Kursio.Modules.Teachers.Domain.Classrooms;
using Kursio.Modules.Teachers.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Kursio.Modules.Teachers.Infrastructure.Classrooms;
internal sealed class ClassroomRepository(TeachersDbContext dbContext) : IClassroomRepository
{
    public async Task InsertAsync(Classroom classroom, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(classroom, cancellationToken);
    }

    public async Task<Classroom?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await dbContext.Classrooms.FirstOrDefaultAsync(t => t.Name == name, cancellationToken);
    }

    public async Task<Classroom?> FindAsync(Guid id)
    {
        return await dbContext.Classrooms.FindAsync(id);
    }

    public void Remove(Classroom classroom)
    {
        dbContext.Remove(classroom);
    }

    public async Task RemoveAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        await dbContext.Classrooms
            .Where(classroom => ids.Contains(classroom.Id) && classroom.AssociatedCourseCount == 0)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
