using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MediatR;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Application.Caching;
using Kursio.Modules.Students.Domain.Students;
using Kursio.Modules.Students.Application.Students.DeleteStudents;
using Microsoft.AspNetCore.Mvc;

namespace Kursio.Modules.Students.Presentation.Students;

internal sealed class DeleteStudents : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("students", async ([FromBody] DeleteStudentsRequest request, ISender sender, ICacheService cacheService) =>
        {
            var command = new DeleteStudentsCommand(request.Ids);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await Parallel.ForEachAsync(request.Ids, async (id, cancellationToken) =>
                {
                    await cacheService.RemoveAsync(StudentCacheKeys.Student(id), cancellationToken);
                });
            }

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Students);
    }

    internal sealed class DeleteStudentsRequest
    {
        public IEnumerable<Guid> Ids { get; set; }
    }
}
