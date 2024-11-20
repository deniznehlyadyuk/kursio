using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Kursio.Modules.Teachers.Application.Teachers.GetTeachers;
using Kursio.Modules.Teachers.Application.Teachers.GetTeacher;

namespace Kursio.Modules.Teachers.Presentation.Teachers;
internal sealed class GetTeachers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("teachers/list", async (GetTeachersRequest request, ISender sender) =>
        {
            var query = new GetTeachersQuery(request.Filter, request.Sorting, request.Pagination);

            Result<IReadOnlyCollection<TeacherResponse>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Teachers);
    }

    internal sealed class GetTeachersRequest
    {
        public QueryBuilderFilterModel Filter { get; set; } = new();
        public QueryBuilderSortingModel Sorting { get; set; } = new();
        public QueryBuilderPaginationModel Pagination { get; set; } = new();
    }
}
