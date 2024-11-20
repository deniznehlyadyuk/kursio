using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MediatR;
using Kursio.Modules.Students.Application.Students.CreateStudent;
using Kursio.Common.Domain;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Common.Presentation.ApiResults;

namespace Kursio.Modules.Students.Presentation.Students;

internal sealed class CreateStudent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("students", async (CreateStudentRequest request, ISender sender) =>
        {
            var command = new CreateStudentCommand(
                request.FullName,
                request.PhoneNumber,
                request.ParentFullName,
                request.ParentPhoneNumber);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Students);
    }

    internal sealed class CreateStudentRequest
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string ParentFullName { get; set; }
        public string ParentPhoneNumber { get; set; }
    }
}
