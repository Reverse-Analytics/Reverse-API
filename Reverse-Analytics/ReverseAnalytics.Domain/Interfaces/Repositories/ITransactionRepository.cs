using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Repositories;

public interface ITransactionRepository : IRepositoryBase<Transaction>
{
    Task<PaginatedList<Transaction>> FindAllAsync(TransactionQueryParameters queryParameters);
}
