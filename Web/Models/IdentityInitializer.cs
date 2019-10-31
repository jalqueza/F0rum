using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Models
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public async static void SeedUsers(UserManager<IdentityUser> userManager)
        {

            // Seeded Admin

            if (userManager.FindByNameAsync("user1").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                   UserName = "user1@localhost",
                   Email = "user1@localhost"
                 };
                var result = await userManager.CreateAsync(user, "pPassword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "User"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Banned").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Banned"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}