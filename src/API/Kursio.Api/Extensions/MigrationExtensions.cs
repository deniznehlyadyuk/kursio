using Kursio.Modules.Students.Infrastructure.Database;
using Kursio.Modules.Teachers.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Kursio.Api.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<StudentsDbContext>(scope);
        ApplyMigration<TeachersDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
