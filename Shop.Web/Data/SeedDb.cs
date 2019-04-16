
namespace Shop.Web.Data
{    
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public class SeedDb
    {
        private readonly DataContext context;
        private Random random;

        public SeedDb(DataContext context)
        {
            this.context = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Products.Any())
            {
                this.AddProduct("Iphone x");
                this.AddProduct("magic mouse");
                this.AddProduct("iwatch series 4");
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            this.context.Products.Add(new Product
            {
                name = name,
                Price = this.random.Next(1000),
                Isavailabe = true,
                Stock = this.random.Next(100)
            });
        }

    }
}
