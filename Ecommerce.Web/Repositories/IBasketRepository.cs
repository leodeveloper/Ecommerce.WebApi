using Ecommerce.Web.Model;
using System.Collections.Generic;

namespace Ecommerce.Web.Repositories
{
    public interface IBasketRepository
    {
        IList<BasketItemViewModel> GetBasketItem(int userId);
        BasketItemViewModel AddItemintoBasket(int productId, int quantity, int userId);
        IList<BasketItemViewModel> UpdateBasketItemQuantity(int basketItemId, int quantity);
        IList<BasketItemViewModel> DeleteItemFromBasket(int basketItemId);
        IList<BasketItemViewModel> DeleteAllBasketItems(int userId);
    }
}