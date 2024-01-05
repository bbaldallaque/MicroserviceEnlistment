using MicroserviceEnlistment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceEnlistment
{
    public class UserContext : IdentityDbContext<User>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    }
}
