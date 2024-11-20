using Kursio.Common.Domain;
using Kursio.Common.Presentation.ApiResults;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Classrooms.CreateClassroom;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kursio.Modules.Teachers.Presentation.Classrooms;
internal sealed class CreateClassroom : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("classrooms", async (CreateClassroomRequest request, ISender sender) =>
        {
            var command = new CreateClassroomCommand(request.Name);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Classrooms);
    }

    internal sealed class CreateClassroomRequest
    {
        public string Name { get; set; }
    }
}
