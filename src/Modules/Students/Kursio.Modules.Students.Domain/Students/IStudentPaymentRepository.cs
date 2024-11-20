
namespace Kursio.Modules.Students.Domain.Students;
public interface IStudentPaymentRepository
{
    Task<StudentPayment?> FindAsync(Guid id);
    Task<IList<StudentPayment>> GetAllByIds(IEnumerable<Guid> ids);
    Task InsertAsync(StudentPayment studentPayment, CancellationToken cancellationToken = default);
    void Remove(StudentPayment studentPayment);
    void RemoveAll(IEnumerable<StudentPayment> studentPayments);
    void Update(StudentPayment studentPayment);
}
