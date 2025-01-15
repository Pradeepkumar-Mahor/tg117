using Microsoft.AspNetCore.Identity;

namespace tg117.Domain
{
    public static class DefaultUsers
    {
        public static async Task SeedBasicUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser defaultUser = new()
            {
                UserName = "basicuser@gmail.com",
                Email = "basicuser@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                AppUser? user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    _ = await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    _ = await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }

        public static async Task SeedSuperAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser defaultUser = new()
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                AppUser? user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    _ = await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    _ = await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    _ = await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    _ = await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
                //  await roleManager.SeedClaimsForSuperAdmin();
            }
        }

        private static async Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            IdentityRole? adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            _ = await roleManager.SetRoleNameAsync(role: adminRole, "Admin");
        }

        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            _ = await roleManager.GetClaimsAsync(role);
            //List<string> allPermissions = Permissions.GeneratePermissionsForModule(module);
            //foreach (string permission in allPermissions)
            //{
            //    if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            //    {
            //        _ = await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            //    }
            //}
        }
    }
}