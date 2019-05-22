namespace Shop.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Entities;
    using Microsoft.AspNetCore.Http;

    public class ProductViewModel : Product
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
