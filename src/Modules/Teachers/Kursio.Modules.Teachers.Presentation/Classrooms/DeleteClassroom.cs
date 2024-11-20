using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Classrooms.DeleteClassroom;
using Kursio.Modules.Teachers.Domain.Classrooms;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Classrooms;
internal sealed class DeleteClassroom : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("classrooms/{id:guid}", async (Guid id, ISender sender, ICacheService cacheService) =>
        {
            var command = new DeleteClassroomCommand(id);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await cacheService.RemoveAsync(ClassroomCacheKeys.Classroom(id));
            }

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Classrooms);
    }
}
