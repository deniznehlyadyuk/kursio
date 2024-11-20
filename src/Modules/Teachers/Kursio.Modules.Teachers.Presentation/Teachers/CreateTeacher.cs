using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Teachers.CreateTeacher;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Teachers;
internal sealed class CreateTeacher : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("teachers", async (CreateTeacherRequest request, ISender sender) =>
        {
            var command = new CreateTeacherCommand(request.FullName, request.PhoneNumber);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Teachers);
    }

    internal sealed class CreateTeacherRequest
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
