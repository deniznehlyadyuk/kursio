using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;

namespace Kursio.Modules.Students.Application.Students.DeleteStudentPayment;

internal sealed class DeleteStudentPaymentCommandHandler(
    IStudentPaymentRepository studentPaymentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteStudentPaymentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeleteStudentPaymentCommand request, CancellationToken cancellationToken)
    {
        StudentPayment? studentPayment = await studentPaymentRepository.FindAsync(request.Id);

        if (studentPayment is null)
        {
            return Result.Failure<Guid>(StudentErrors.PaymentNotFound(request.Id));
        }

        studentPayment.Remove();

        studentPaymentRepository.Remove(studentPayment);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return studentPayment.StudentId;
    }
}
