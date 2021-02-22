using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Contracts.Client
{
    public interface IControllerAccess<T> : IDisposable
        where T : IIdentifiable
    {
        int MaxPageSize { get; }
        Task<int> CountAsync();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetPageListAsync(int pageIndex, int pageSize);
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync();
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
