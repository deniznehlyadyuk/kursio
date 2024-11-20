using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MediatR;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Application.Caching;
using Kursio.Modules.Students.Domain.Students;
using Kursio.Modules.Students.Application.Students.DeleteStudent;

namespace Kursio.Modules.Students.Presentation.Students;

internal sealed class DeleteStudent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("students/{id:guid}", async (Guid id, ISender sender, ICacheService cacheService) =>
        {
            var command = new DeleteStudentCommand(id);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await cacheService.RemoveAsync(StudentCacheKeys.Student(id));
            }

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Students);
    }
}
