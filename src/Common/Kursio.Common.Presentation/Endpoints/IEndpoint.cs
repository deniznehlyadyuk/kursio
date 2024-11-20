using Microsoft.AspNetCore.Routing;

namespace Kursio.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
