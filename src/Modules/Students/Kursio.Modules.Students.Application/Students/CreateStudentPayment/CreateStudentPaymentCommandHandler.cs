using System.Data.Common;
using Kursio.Common.Application.Messaging;
using Kursio.Common.Domain;
using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;

namespace Kursio.Modules.Students.Application.Students.CreateStudentPayment;

internal sealed class CreateStudentPaymentCommandHandler(
    IStudentRepository studentRepository,
    IStudentPaymentRepository studentPaymentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateStudentPaymentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateStudentPaymentCommand request, CancellationToken cancellationToken)
    {
        await using DbTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        Student? student = await studentRepository.FindAsync(request.StudentId);

        if (student is null)
        {
            return Result.Failure<Guid>(StudentErrors.NotFound(request.StudentId));
        }

        Result<StudentPayment> studentPaymentResult = StudentPayment.Create(
            request.StudentId,
            request.PaymentAmount);

        if (studentPaymentResult.IsFailure)
        {
            return Result.Failure<Guid>(studentPaymentResult.Error);
        }

        await studentPaymentRepository.InsertAsync(studentPaymentResult.Value, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return studentPaymentResult.Value.Id;
    }
}
