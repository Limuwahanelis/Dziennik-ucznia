using Microsoft.AspNetCore.Identity;

namespace DziennikUcznia.Identity
{
    public class AppUser:IdentityUser
    {
        public AppUser(string name):base(name)
        {
        }
        public AppUser() : base()
        {
        }
    }
}
