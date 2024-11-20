using Kursio.Modules.Students.Domain.Students;
using Kursio.Modules.Students.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Kursio.Modules.Students.Infrastructure.Students;

internal sealed class StudentPaymentRepository(StudentsDbContext dbContext) : IStudentPaymentRepository
{
    public async Task<StudentPayment?> FindAsync(Guid id)
    {
        return await dbContext.StudentPayments.FindAsync(id);
    }

    public async Task<IList<StudentPayment>> GetAllByIds(IEnumerable<Guid> ids)
    {
        return await dbContext.StudentPayments.Where(studentPayment => ids.Contains(studentPayment.Id)).ToListAsync();
    }

    public async Task InsertAsync(StudentPayment studentPayment, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(studentPayment, cancellationToken);
    }

    public void Remove(StudentPayment studentPayment)
    {
        dbContext.StudentPayments.Remove(studentPayment);
    }

    public void RemoveAll(IEnumerable<StudentPayment> studentPayments)
    {
        dbContext.RemoveRange(studentPayments);
    }

    public void Update(StudentPayment studentPayment)
    {
        dbContext.Update(studentPayment);
    }
}
