using Kursio.Common.Domain;

namespace Kursio.Modules.Students.Domain.Students;

public static class StudentErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Students.NotFound", $"The student with the identifier {id} was not found");

    public static Error InvalidPaymentAmount =>
        Error.Problem("Students.InvalidPaymentAmount", "The entered amount cannot be less than zero");

    public static Error PaymentNotFound(Guid id) =>
        Error.NotFound("Students.Payment.NotFound", $"The student payment with the identifier {id} was not found");
}
