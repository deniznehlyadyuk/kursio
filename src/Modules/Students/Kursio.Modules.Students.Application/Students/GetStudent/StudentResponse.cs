namespace Kursio.Modules.Students.Application.Students.GetStudent;

public sealed record StudentResponse(
    Guid Id,
    string FullName,
    string? PhoneNumber,
    string? ParentFullName,
    string? ParentPhoneNumber,
    int Debt);
