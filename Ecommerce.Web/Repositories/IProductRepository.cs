using System.Collections.Generic;
using Ecommerce.Web.Model;

namespace Ecommerce.Web.Repositories
{
    public interface IProductRepository
    {
        IList<ProductViewModel> GetProducts();
        ProductViewModel GetProductById(int productId);
    }
}