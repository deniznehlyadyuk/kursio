using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Classrooms.GetClassroom;
using Kursio.Modules.Teachers.Domain.Classrooms;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Classrooms;
internal sealed class GetClassroom : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("classrooms/{id:guid}", async (Guid id, ISender sender, ICacheService cacheService) =>
        {
            ClassroomResponse classroomResponse = await cacheService.GetAsync<ClassroomResponse>(ClassroomCacheKeys.Classroom(id));

            if (classroomResponse is not null)
            {
                return Results.Ok(classroomResponse);
            }

            var query = new GetClassroomQuery(id);

            Result<ClassroomResponse> result = await sender.Send(query);

            if (result.IsSuccess)
            {
                await cacheService.SetAsync(ClassroomCacheKeys.Classroom(id), result);
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Classrooms);
    }
}
