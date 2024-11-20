using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Kursio.Modules.Teachers.Application.Classrooms.GetClassrooms;
using Kursio.Modules.Teachers.Application.Classrooms.GetClassroom;

namespace Kursio.Modules.Teachers.Presentation.Classrooms;
internal sealed class GetClassrooms : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("classrooms/list", async (GetClassroomsRequest request, ISender sender) =>
        {
            var query = new GetClassroomsQuery(request.Filter, request.Sorting, request.Pagination);

            Result<IReadOnlyCollection<ClassroomResponse>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Classrooms);
    }

    internal sealed class GetClassroomsRequest
    {
        public QueryBuilderFilterModel Filter { get; set; } = new();
        public QueryBuilderSortingModel Sorting { get; set; } = new();
        public QueryBuilderPaginationModel Pagination { get; set; } = new();
    }
}
