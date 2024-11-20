using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Topics.DeleteTopics;
using Kursio.Modules.Teachers.Domain.Topics;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Topics;
internal sealed class DeleteTopics : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("topics", async ([FromBody] DeleteTopicsRequest request, ISender sender, ICacheService cacheService) =>
        {
            var command = new DeleteTopicsCommand(request.Ids);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await Parallel.ForEachAsync(request.Ids, async (id, cancellationToken) =>
                {
                    await cacheService.RemoveAsync(TopicCacheKeys.Topic(id), cancellationToken);
                });
            }

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Topics);
    }

    internal sealed class DeleteTopicsRequest
    {
        public IEnumerable<Guid> Ids { get; set; }
    }
}
