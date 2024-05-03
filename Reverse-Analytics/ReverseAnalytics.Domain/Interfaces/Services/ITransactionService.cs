using ReverseAnalytics.Domain.DTOs.Transaction;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Services;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllAsync();
    Task<IEnumerable<TransactionDto>> GetAllAsync(TransactionQueryParameters queryParameters);
    Task<TransactionDto> GetByIdAsync(int id);
    Task<TransactionDto> CreateAsync(TransactionForCreateDto transactionToCreate);
    Task<TransactionDto> UpdateAsync(TransactionForUpdateDto transactionToUpdate);
    Task<TransactionDto> DeleteAsync(int id);
}
