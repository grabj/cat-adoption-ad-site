using Microsoft.AspNetCore.Identity;

namespace OnlyCatsWebapp.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
