using OnlyCatsWebapp.Areas.Identity.Data;

namespace OnlyCatsWebapp.Core.Repositories
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetUsers();

        ApplicationUser GetUser(string id);

        ApplicationUser UpdateUser(ApplicationUser user);

        ApplicationUser DeleteUser(ApplicationUser user);
    }
    
}
