using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Topics.GetTopic;
using Kursio.Modules.Teachers.Domain.Topics;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Topics;
internal sealed class GetTopic : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("topic/{id:guid}", async (Guid id, ISender sender, ICacheService cacheService) =>
        {
            TopicResponse topicResponse = await cacheService.GetAsync<TopicResponse>(TopicCacheKeys.Topic(id));

            if (topicResponse is not null)
            {
                return Results.Ok(topicResponse);
            }

            var query = new GetTopicQuery(id);

            Result<TopicResponse> result = await sender.Send(query);

            if (result.IsSuccess)
            {
                await cacheService.SetAsync(TopicCacheKeys.Topic(id), result);
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Topics);
    }
}
