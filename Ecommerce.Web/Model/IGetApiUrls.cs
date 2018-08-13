namespace Ecommerce.Web.Model
{
    public interface IGetApiUrls
    {
       
        string GetBasketItem { get; }
        string PostBasketItem { get; }
        string ProductsApiUrl { get; }

        string DeleteItemFromBasket(int basketItemId);
        string GetProductByIdApiUrl(int productId);
        string PutChangeItemQuantity(int basketItemId, int quantity);
        string DeleteBasketItems(int userId);
    }
}