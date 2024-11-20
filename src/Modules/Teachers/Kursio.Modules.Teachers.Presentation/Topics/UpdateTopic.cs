using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Topics.UpdateTopic;
using Kursio.Modules.Teachers.Domain.Topics;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Topics;
internal sealed class UpdateTopic : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("topics/{id:guid}", async (Guid id, UpdateTopicRequest request, ISender sender, ICacheService cacheService) =>
        {
            var command = new UpdateTopicCommand(id, request.Name);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await cacheService.RemoveAsync(TopicCacheKeys.Topic(id));
            }

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Topics);
    }

    internal sealed class UpdateTopicRequest
    {
        public string Name { get; set; }
    }
}
