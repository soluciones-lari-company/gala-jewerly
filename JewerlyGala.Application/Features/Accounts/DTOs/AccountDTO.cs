using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.Accounts.DTOs
{
    public class AccountDTO : IMapFrom<Account>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
