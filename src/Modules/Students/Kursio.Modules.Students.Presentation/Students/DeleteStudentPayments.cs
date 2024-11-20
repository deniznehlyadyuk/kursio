using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Students.Application.Students.DeleteStudentPayments;
using Kursio.Modules.Students.Domain.Students;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Students.Presentation.Students;
internal sealed class DeleteStudentPayments : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("students/payment", async (
            [FromBody] DeleteStudentPaymentsRequest request,
            ISender sender,
            ICacheService cacheService) =>
        {
            var command = new DeleteStudentPaymentsCommand(request.Ids);

            Result<IEnumerable<Guid>> studentIdsResult = await sender.Send(command);

            if (studentIdsResult.IsSuccess)
            {
                await Parallel.ForEachAsync(studentIdsResult.Value, async (studentId, cancellationToken) =>
                {
                    await cacheService.RemoveAsync(StudentCacheKeys.Student(studentId), cancellationToken);
                });
            }

            return studentIdsResult.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Students);
    }

    internal sealed class DeleteStudentPaymentsRequest
    {
        public IEnumerable<Guid> Ids { get; set; }
    }
}
