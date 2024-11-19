using Microsoft.AspNetCore.Identity;

namespace JewerlyGala.Domain.Identity
{
    public class User: IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; } = default!;
    }
}
