using Ecommerce.Web.Model;
using System.Collections.Generic;

namespace Ecommerce.Web.Repositories
{
    public interface IBasketRepository
    {
        IList<BasketItemViewModel> GetBasketItem();
        BasketItemViewModel AddItemintoBasket(int productId, int quantity);
        IList<BasketItemViewModel> UpdateBasketItemQuantity(int basketItemId, int quantity);
        IList<BasketItemViewModel> DeleteItemFromBasket(int basketItemId);
        IList<BasketItemViewModel> DeleteAllBasketItems(int userId);
    }
}