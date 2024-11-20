using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Classrooms.DeleteClassrooms;
using Kursio.Modules.Teachers.Domain.Classrooms;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Classrooms;
internal sealed class DeleteClassrooms : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("classrooms", async ([FromBody] DeleteClassroomsRequest request, ISender sender, ICacheService cacheService) =>
        {
            var command = new DeleteClassroomsCommand(request.Ids);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await Parallel.ForEachAsync(request.Ids, async (id, cancellationToken) =>
                {
                    await cacheService.RemoveAsync(ClassroomCacheKeys.Classroom(id), cancellationToken);
                });
            }

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Classrooms);
    }

    internal sealed class DeleteClassroomsRequest
    {
        public IEnumerable<Guid> Ids { get; set; }
    }
}
