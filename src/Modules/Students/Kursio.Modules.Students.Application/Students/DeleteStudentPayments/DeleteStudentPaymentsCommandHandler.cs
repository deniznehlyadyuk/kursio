using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;

namespace Kursio.Modules.Students.Application.Students.DeleteStudentPayments;

internal sealed class DeleteStudentPaymentsCommandHandler(
    IStudentPaymentRepository studentPaymentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteStudentPaymentsCommand, IEnumerable<Guid>>
{
    public async Task<Result<IEnumerable<Guid>>> Handle(DeleteStudentPaymentsCommand request, CancellationToken cancellationToken)
    {
        IList<StudentPayment> studentPayments = await studentPaymentRepository.GetAllByIds(request.Ids);

        foreach (StudentPayment studentPayment in studentPayments)
        {
            studentPayment.Remove();
        }

        studentPaymentRepository.RemoveAll(studentPayments);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(studentPayments.Select(sp => sp.StudentId));
    }
}
