using OnlyCatsWebapp.Areas.Identity.Data;
using OnlyCatsWebapp.Core.Repositories;
using System.Reflection;

namespace OnlyCatsWebapp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u=> u.Id == id);
        }

        //repsitory to get users.
        public ICollection<ApplicationUser> GetUsers()
        {
                return _context.Users.ToList();
            
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        { 
            //add entity
            _context.Update(user);
            //call save changes
            _context.SaveChanges();
            return user;
        }

        public ApplicationUser DeleteUser(ApplicationUser user)
        {
            _context.Remove(user);
            _context.SaveChanges();
            return user;
        }
    }
}
