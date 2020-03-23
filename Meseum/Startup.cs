using Meseum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Meseum.Startup))]
namespace Meseum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
            createRolesandUsers();
        }
        // In this method we will create default User roles and Admin user for login
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("SPAdmin"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SPAdmin";
                roleManager.Create(role);


                //Here we create a Admin super user who will maintain the website                   


            }
            if (UserManager.FindByEmail("dbugtest2016@gmail.com") == null)
            {
                var user = new ApplicationUser();

                user.UserName = "dibug";
                user.Email = "dbugtest2016@gmail.com";
                string userPWD = "Password1@";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "SPAdmin");

                }
            }
            else
            {
                ApplicationUser user = UserManager.FindByEmail("dbugtest2016@gmail.com");
                var result1 = UserManager.AddToRole(user.Id, "SPAdmin");

            }

            //  creating Creating Manager role
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

            }

            // creating Creating Employee role
            if (!roleManager.RoleExists("CMSAdmin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "CMSAdmin";
                roleManager.Create(role);

            }
        }
    }
}
