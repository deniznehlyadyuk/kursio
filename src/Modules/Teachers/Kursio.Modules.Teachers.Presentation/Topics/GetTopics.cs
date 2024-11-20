using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Topics.GetTopics;
using Kursio.Modules.Teachers.Application.Topics.GetTopic;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Topics;
internal sealed class GetTopics : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("topics/list", async (GetTopicsRequest request, ISender sender) =>
        {
            var query = new GetTopicsQuery(request.Filter, request.Sorting, request.Pagination);

            Result<IReadOnlyCollection<TopicResponse>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Topics);
    }

    internal sealed class GetTopicsRequest
    {
        public QueryBuilderFilterModel Filter { get; set; } = new();
        public QueryBuilderSortingModel Sorting { get; set; } = new();
        public QueryBuilderPaginationModel Pagination { get; set; } = new();
    }
}
