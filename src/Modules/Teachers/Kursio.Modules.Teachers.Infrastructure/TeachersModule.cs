using Kursio.Common.Infrastructure.Outbox;
using Kursio.Common.Presentation.Endpoints;
using Kursio.Modules.Teachers.Application.Abstraction.Data;
using Kursio.Modules.Teachers.Domain.Classrooms;
using Kursio.Modules.Teachers.Domain.Courses;
using Kursio.Modules.Teachers.Domain.Teachers;
using Kursio.Modules.Teachers.Domain.Topics;
using Kursio.Modules.Teachers.Infrastructure.Classrooms;
using Kursio.Modules.Teachers.Infrastructure.Database;
using Kursio.Modules.Teachers.Infrastructure.Students;
using Kursio.Modules.Teachers.Infrastructure.Teachers;
using Kursio.Modules.Teachers.Infrastructure.Topics;
using Kursio.Modules.Teachers.Presentation.Students;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kursio.Modules.Teachers.Infrastructure;

public static class TeachersModule
{
    public static IServiceCollection AddTeachersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        services.AddInfrastructure(configuration);

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator, string instanceId)
    {
        registrationConfigurator.AddConsumer<StudentCreatedIntegrationEventConsumer>()
            .Endpoint(c => c.InstanceId = instanceId);

        registrationConfigurator.AddConsumer<StudentUpdatedIntegrationEventConsumer>()
            .Endpoint(c => c.InstanceId = instanceId);

        registrationConfigurator.AddConsumer<StudentDeletedIntegrationEventConsumer>()
            .Endpoint(c => c.InstanceId = instanceId);
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<TeachersDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Teachers))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptors>()));

        services.AddScoped<ITopicRepository, TopicRepository>();
        services.AddScoped<IClassroomRepository, ClassroomRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TeachersDbContext>());
    }
}
