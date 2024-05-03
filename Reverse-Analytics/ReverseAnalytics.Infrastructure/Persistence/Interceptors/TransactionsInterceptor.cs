using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Interceptors;

public class TransactionsInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        // UpdateTransactions(eventData.Context as ApplicationDbContext);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        // UpdateTransactions(eventData.Context as ApplicationDbContext);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateTransactions(ApplicationDbContext? context)
    {
        if (context is null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>().ToList())
        {
            if (entry.Entity is not ITransaction entity)
            {
                continue;
            }

            if (entry.State == EntityState.Added)
            {
                var transaction = CreateTransaction(entity);
                context.Transactions.Add(transaction);
            }

            if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                var transaction = context.Transactions
                    .FirstOrDefault(x => x.SourceId == entity.GetTransactionSourceId() && x.Source == entity.TransactionSource) ?? throw new Exception();

                transaction.Amount = entity.GetTransactionAmount();
            }
        }
    }

    private static Transaction CreateTransaction(ITransaction transaction)
    {
        return new Transaction
        {
            Amount = transaction.GetTransactionAmount(),
            Date = DateTime.Now,
            Source = transaction.TransactionSource,
            SourceId = transaction.GetTransactionSourceId(),
            Type = transaction.TransactionType,
        };
    }
}
