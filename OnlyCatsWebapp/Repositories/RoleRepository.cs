using Microsoft.AspNetCore.Identity;
using OnlyCatsWebapp.Areas.Identity.Data;
using OnlyCatsWebapp.Core.Repositories;

namespace OCApp.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
