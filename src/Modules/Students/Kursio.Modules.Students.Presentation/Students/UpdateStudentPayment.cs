using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Students.Application.Students.UpdateStudentPayment;
using Kursio.Modules.Students.Domain.Students;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Students.Presentation.Students;
internal sealed class UpdateStudentPayment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("students/payment/{id:guid}", async (
            Guid id,
            UpdateStudentPaymentRequest request,
            ISender sender,
            ICacheService cacheService) =>
        {
            var command = new UpdateStudentPaymentCommand(id, request.PaymentAmount);

            Result<Guid> studentIdResult = await sender.Send(command);

            if (studentIdResult.IsSuccess)
            {
                await cacheService.RemoveAsync(StudentCacheKeys.Student(studentIdResult.Value));
            }

            return studentIdResult.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Students);
    }

    internal sealed class UpdateStudentPaymentRequest
    {
        public int PaymentAmount { get; set; }
    }
}
