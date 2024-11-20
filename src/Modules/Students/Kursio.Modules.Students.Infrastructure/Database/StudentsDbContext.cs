using System.Data.Common;
using Kursio.Common.Infrastructure.Outbox;
using Kursio.Modules.Students.Application.Abstraction.Data;
using Kursio.Modules.Students.Domain.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Kursio.Modules.Students.Infrastructure.Database;

public sealed class StudentsDbContext(DbContextOptions<StudentsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Student> Students { get; set; }
    internal DbSet<StudentPayment> StudentPayments { get; set; }

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }

        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Students);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
    }
}
