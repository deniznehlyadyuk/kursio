using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;

namespace Kursio.Modules.Students.Application.Students.UpdateStudentPayment;

internal sealed class UpdateStudentPaymentCommandHandler(
    IStudentPaymentRepository studentPaymentRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateStudentPaymentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateStudentPaymentCommand request, CancellationToken cancellationToken)
    {
        StudentPayment? studentPayment = await studentPaymentRepository.FindAsync(request.Id);

        if (studentPayment is null)
        {
            return Result.Failure<Guid>(StudentErrors.PaymentNotFound(request.Id));
        }

        Result result = studentPayment.Update(request.PaymentAmount);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        studentPaymentRepository.Update(studentPayment);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return studentPayment.StudentId;
    }
}
