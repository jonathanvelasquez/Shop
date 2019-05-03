namespace Shop.Web.Data
{    
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Helpers;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }


        public SeedDb(DataContext context)
        {
            this.context = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            var user = await this.userHelper.GetUserByEmailAsync("jonathanalberto123@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Jonathan",
                    LastName = "Velasquez",
                    Email = "jonathanalberto123@hotmail.com",
                    UserName = "jonathanalberto123@hotmail.com",
                    PhoneNumber = "3212395136"
                };

                var result = await this.userHelper.AddUserAsync(user, "balin2335394");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("Iphone x",user);
                this.AddProduct("magic mouse", user);
                this.AddProduct("iwatch series 4", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                name = name,
                Price = this.random.Next(1000),
                Isavailabe = true,
                Stock = this.random.Next(100),
                User = user
            });
        }

    }
}
