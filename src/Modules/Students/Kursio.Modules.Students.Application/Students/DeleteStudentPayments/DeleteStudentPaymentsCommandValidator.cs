using FluentValidation;

namespace Kursio.Modules.Students.Application.Students.DeleteStudentPayments;

internal sealed class DeleteStudentPaymentsCommandValidator : AbstractValidator<DeleteStudentPaymentsCommand>
{
    public DeleteStudentPaymentsCommandValidator()
    {
        RuleFor(r => r.Ids).NotEmpty();
    }
}
