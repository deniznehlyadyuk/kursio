using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MediatR;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Application.Caching;
using Kursio.Modules.Students.Domain.Students;
using Kursio.Modules.Students.Application.Students.UpdateStudent;

namespace Kursio.Modules.Students.Presentation.Students;

internal sealed class UpdateStudent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("students/{id:guid}", async (Guid id, UpdateStudentRequest request, ISender sender, ICacheService cacheService) =>
        {
            var command = new UpdateStudentCommand(
                id,
                request.FullName,
                request.PhoneNumber,
                request.ParentFullName,
                request.ParentPhoneNumber);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await cacheService.RemoveAsync(StudentCacheKeys.Student(id));
            }

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Students);
    }

    internal sealed class UpdateStudentRequest
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string ParentFullName { get; set; }
        public string ParentPhoneNumber { get; set; }
    }
}
