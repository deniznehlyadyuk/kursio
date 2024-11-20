using Kursio.Common.Application.Messaging;
using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;

namespace Kursio.Modules.Students.Application.Students.DeleteStudentPayment;

internal sealed class StudentPaymentDeletedDomainEventHandler(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork)
    : IDomainEventHandler<StudentPaymentDeletedDomainEvent>
{
    public async Task Handle(StudentPaymentDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        Student? student = await studentRepository.FindAsync(notification.StudentId);

        if (student is null)
        {
            return;
        }

        student.UpdateDebt(-1 * notification.PaymentAmount);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
