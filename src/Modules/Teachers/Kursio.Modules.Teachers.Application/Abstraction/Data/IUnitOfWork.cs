﻿using System.Data.Common;

namespace Kursio.Modules.Teachers.Application.Abstraction.Data;

public interface IUnitOfWork
{
    Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}