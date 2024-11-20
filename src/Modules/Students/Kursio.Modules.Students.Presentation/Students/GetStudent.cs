using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MediatR;
using Kursio.Modules.Students.Application.Students.GetStudent;
using Kursio.Common.Domain;
using Kursio.Common.Application.Caching;
using Kursio.Modules.Students.Domain.Students;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Common.Presentation.ApiResults;

namespace Kursio.Modules.Students.Presentation.Students;

internal sealed class GetStudent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("students/{id:guid}", async (Guid id, ISender sender, ICacheService cacheService) =>
        {
            StudentResponse studentResponse = await cacheService.GetAsync<StudentResponse>(StudentCacheKeys.Student(id));

            if (studentResponse is not null)
            {
                return Results.Ok(studentResponse);
            }

            var query = new GetStudentQuery(id);

            Result<StudentResponse> result = await sender.Send(query);

            if (result.IsSuccess)
            {
                await cacheService.SetAsync(StudentCacheKeys.Student(id), result.Value);
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Students);
    }
}
