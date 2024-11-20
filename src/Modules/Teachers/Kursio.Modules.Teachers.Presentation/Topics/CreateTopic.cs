using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Topics.CreateTopic;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Topics;
internal sealed class CreateTopic : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("topics", async (CreateTopicRequest request, ISender sender) =>
        {
            var command = new CreateTopicCommand(request.Name);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Topics);
    }

    internal sealed class CreateTopicRequest
    {
        public string Name { get; set; }
    }
}
