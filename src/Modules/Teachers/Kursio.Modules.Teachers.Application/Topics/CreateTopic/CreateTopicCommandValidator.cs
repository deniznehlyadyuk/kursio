using FluentValidation;

namespace Kursio.Modules.Teachers.Application.Topics.CreateTopic;

internal sealed class CreateTopicCommandValidator : AbstractValidator<CreateTopicCommand>
{
    public CreateTopicCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}
