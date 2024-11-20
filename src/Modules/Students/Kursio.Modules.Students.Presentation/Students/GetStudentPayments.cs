using Kursio.Common.Domain;
using Kursio.Common.Domain.QueryBuilder;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Students.Application.Students.GetStudentPayments;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Students.Presentation.Students;

internal sealed class GetStudentPayments : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("students/{studentId:guid}/payments", async (
            Guid studentId,
            GetStudentPaymentsRequest request,
            ISender sender) =>
        {
            var query = new GetStudentPaymentsQuery(studentId, request.Filter, request.Sorting, request.Pagination);

            Result<IReadOnlyCollection<StudentPaymentResponse>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Students);
    }

    internal sealed class GetStudentPaymentsRequest
    {
        public QueryBuilderFilterModel Filter { get; set; } = new();
        public QueryBuilderSortingModel Sorting { get; set; } = new();
        public QueryBuilderPaginationModel Pagination { get; set; } = new();
    }
}
