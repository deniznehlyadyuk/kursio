using FluentValidation;

namespace Kursio.Modules.Teachers.Application.Topics.UpdateTopic;
internal sealed class UpdateTopicCommandValidator : AbstractValidator<UpdateTopicCommand>
{
    public UpdateTopicCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}
