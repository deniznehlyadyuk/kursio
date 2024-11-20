using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Students.Application.Students.DeleteStudentPayment;
using Kursio.Modules.Students.Domain.Students;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Students.Presentation.Students;
internal sealed class DeleteStudentPayment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("students/payment/{id:guid}", async (
            Guid id,
            ISender sender,
            ICacheService cacheService) =>
        {
            var command = new DeleteStudentPaymentCommand(id);

            Result<Guid> studentIdResult = await sender.Send(command);

            if (studentIdResult.IsSuccess)
            {
                await cacheService.RemoveAsync(StudentCacheKeys.Student(studentIdResult.Value));
            }

            return studentIdResult.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Students);
    }
}
