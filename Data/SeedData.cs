using Microsoft.AspNetCore.Identity;

namespace AxiomCRM.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider services)
    {
        var roleManager =
            services.GetRequiredService<RoleManager<IdentityRole>>();

        var userManager =
            services.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles =
        {
            "Admin",
            "Manager",
            "SalesExecutive"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        await CreateUser(
            userManager,
            "admin@acxiom.com",
            "Admin123!",
            "Admin");

        await CreateUser(
            userManager,
            "manager@acxiom.com",
            "Manager123!",
            "Manager");

        await CreateUser(
            userManager,
            "sales@acxiom.com",
            "Sales123!",
            "SalesExecutive");
    }

    private static async Task CreateUser(
        UserManager<IdentityUser> manager,
        string email,
        string password,
        string role)
    {
        var user = await manager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result =
                await manager.CreateAsync(user, password);

            if (result.Succeeded)
                await manager.AddToRoleAsync(user, role);
        }
    }
}