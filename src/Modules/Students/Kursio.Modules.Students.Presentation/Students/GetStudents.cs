using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Students.Application.Students.GetStudent;
using Kursio.Modules.Students.Application.Students.GetStudents;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Students.Presentation.Students;
internal sealed class GetStudents : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("students/list", async (GetStudentsRequest request, ISender sender) =>
        {
            var query = new GetStudentsQuery(request.Filter, request.Sorting, request.Pagination);

            Result<IReadOnlyCollection<StudentResponse>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Students);
    }

    internal sealed class GetStudentsRequest
    {
        public QueryBuilderFilterModel Filter { get; set; } = new();
        public QueryBuilderSortingModel Sorting { get; set; } = new();
        public QueryBuilderPaginationModel Pagination { get; set; } = new();
    }
}
