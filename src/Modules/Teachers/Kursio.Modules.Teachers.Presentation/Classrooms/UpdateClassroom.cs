using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Classrooms.UpdateClassroom;
using Kursio.Modules.Teachers.Domain.Classrooms;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Classrooms;
internal sealed class UpdateClassroom : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("classrooms/{id:guid}", async (Guid id, UpdateClassroomRequest request, ISender sender, ICacheService cacheService) =>
        {
            var command = new UpdateClassroomCommand(id, request.Name);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await cacheService.RemoveAsync(ClassroomCacheKeys.Classroom(id));
            }

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Classrooms);
    }

    internal sealed class UpdateClassroomRequest
    {
        public string Name { get; set; }
    }
}
