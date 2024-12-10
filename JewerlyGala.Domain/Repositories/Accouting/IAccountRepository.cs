using JewerlyGala.Domain.Entities;
namespace JewerlyGala.Domain.Repositories.Accouting
{
    public interface IAccountRepository
    {
        Account Account { get; set; }
        public Task<bool> GetAsync(Guid id);
        Task<Guid> CreateAsync();
        Task<bool> UpdateAsync();
        Task<ICollection<Account>> GetAllAsync();
    }
}
