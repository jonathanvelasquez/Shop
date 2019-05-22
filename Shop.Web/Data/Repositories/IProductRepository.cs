namespace Shop.Web.Data
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Shop.Web.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboProducts();

    }

}


