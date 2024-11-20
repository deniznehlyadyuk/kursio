using Kursio.Common.Application.Messaging;
using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;

namespace Kursio.Modules.Students.Application.Students.CreateStudentPayment;

internal sealed class StudentPaymentCreatedDomainEventHandler(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork)
    : IDomainEventHandler<StudentPaymentCreatedDomainEvent>
{
    public async Task Handle(
        StudentPaymentCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Student? student = await studentRepository.FindAsync(notification.StudentId);

        if (student is null)
        {
            return;
        }

        student.DecreaseDebt(notification.PaymentAmount);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
