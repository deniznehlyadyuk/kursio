using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Teachers.DeleteTeachers;
using Kursio.Modules.Teachers.Domain.Teachers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Teachers;
internal sealed class DeleteTeachers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("teachers", async ([FromBody] DeleteTeachersRequest request, ISender sender, ICacheService cacheService) =>
        {
            var command = new DeleteTeachersCommand(request.Ids);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await Parallel.ForEachAsync(request.Ids, async (id, cancellationToken) =>
                {
                    await cacheService.RemoveAsync(TeacherCacheKeys.Teacher(id), cancellationToken);
                });
            }

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Teachers);
    }

    internal sealed class DeleteTeachersRequest
    {
        public IEnumerable<Guid> Ids { get; set; }
    }
}
