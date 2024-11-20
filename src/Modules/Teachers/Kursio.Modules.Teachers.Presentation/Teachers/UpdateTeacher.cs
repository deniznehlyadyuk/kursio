using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Teachers.UpdateTeacher;
using Kursio.Modules.Teachers.Domain.Teachers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Teachers;
internal sealed class UpdateTeacher: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("teachers/{id:guid}", async (Guid id, UpdateTeacherRequest request, ISender sender, ICacheService cacheService) =>
        {
            var command = new UpdateTeacherCommand(id, request.FullName, request.PhoneNumber);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await cacheService.RemoveAsync(TeacherCacheKeys.Teacher(id));
            }

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Teachers);
    }

    internal sealed class UpdateTeacherRequest
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
