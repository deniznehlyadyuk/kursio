using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Teachers.DeleteTeacher;
using Kursio.Modules.Teachers.Domain.Teachers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Teachers;
internal sealed class DeleteTeacher : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("teachers/{id:guid}", async (Guid id, ISender sender, ICacheService cacheService) =>
        {
            var command = new DeleteTeacherCommand(id);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await cacheService.RemoveAsync(TeacherCacheKeys.Teacher(id));
            }

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Teachers);
    }
}
