using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Teachers.GetTeacher;
using Kursio.Modules.Teachers.Domain.Teachers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Teachers;
internal sealed class GetTeacher : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("teachers/{id:guid}", async (Guid id, ISender sender, ICacheService cacheService) =>
        {
            TeacherResponse teacherResponse = await cacheService.GetAsync<TeacherResponse>(TeacherCacheKeys.Teacher(id));

            if (teacherResponse is not null)
            {
                return Results.Ok(teacherResponse);
            }

            var query = new GetTeacherQuery(id);

            Result<TeacherResponse> result = await sender.Send(query);

            if (result.IsSuccess)
            {
                await cacheService.SetAsync(TeacherCacheKeys.Teacher(id), result);
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Teachers);
    }
}
