using Kursio.Common.Application.Messaging;
using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;

namespace Kursio.Modules.Students.Application.Students.UpdateStudentPayment;

internal sealed class StudentPaymentUpdatedDomainEventHandler(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork)
    : IDomainEventHandler<StudentPaymentUpdatedDomainEvent>
{
    public async Task Handle(
        StudentPaymentUpdatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Student? student = await studentRepository.FindAsync(notification.StudentId);

        if (student is null)
        {
            return;
        }

        student.UpdateDebt(notification.PaymentAmountDiff);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
