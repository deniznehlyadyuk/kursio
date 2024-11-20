using Kursio.Common.Infrastructure.Outbox;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;
using Kursio.Modules.Students.Infrastructure.Database;
using Kursio.Modules.Students.Infrastructure.Outbox;
using Kursio.Modules.Students.Infrastructure.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kursio.Modules.Students.Infrastructure;

public static class StudentsModule
{
    public static IServiceCollection AddStudentsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<StudentsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Students))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptors>()));

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentPaymentRepository, StudentPaymentRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<StudentsDbContext>());

        services.Configure<OutboxOptions>(configuration.GetSection("Students:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();
    }
}
