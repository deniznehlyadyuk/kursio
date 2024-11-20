using Kursio.Common.Application.Caching;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Students.Application.Students.CreateStudentPayment;
using Kursio.Modules.Students.Domain.Students;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Students.Presentation.Students;
internal sealed class CreateStudentPayment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("students/{id:guid}/payment", async (
            Guid id,
            CreateStudentPaymentRequest request,
            ISender sender,
            ICacheService cacheService) =>
        {
            var command = new CreateStudentPaymentCommand(
                id,
                request.PaymentAmount);

            Result<Guid> result = await sender.Send(command);

            if (result.IsSuccess)
            {
                await cacheService.RemoveAsync(StudentCacheKeys.Student(id));
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Students);
    }

    internal sealed class CreateStudentPaymentRequest
    {
        public int PaymentAmount { get; set; }
    }
}
