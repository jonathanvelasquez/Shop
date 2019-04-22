namespace Shop.Web.Data
{
    using Shop.Web.Entities;
    using System.Linq;

    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable GetAllWithUsers();
    }

}


