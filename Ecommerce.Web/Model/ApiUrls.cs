using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.Model
{

    public class ApiUrls
    {
        public string BaseUrl { get; set; }
        public string GetProducts { get; set; }
        public string GetProductbyId { get; set; }
        public string GetBasketItem { get; set; }
        public string PostBasketItem { get; set; }
        public string PutChangeItemQuantity { get; set; }
        public string DeleteBasketItems { get; set; }
        public string DeleteItemFromBasket { get; set; }
    }

    public class GetApiUrls : IGetApiUrls
    {
        private readonly IOptions<ApiUrls> _apiUrls;
        public GetApiUrls(IOptions<ApiUrls> apiUrls)
        {
            _apiUrls = apiUrls;
        }

        public string ProductsApiUrl => _apiUrls.Value.BaseUrl + _apiUrls.Value.GetProducts;

        public string GetProductByIdApiUrl(int productId) => _apiUrls.Value.BaseUrl + _apiUrls.Value.GetProductbyId + productId;

        public string GetBasketItem(int userId) => _apiUrls.Value.BaseUrl + _apiUrls.Value.GetBasketItem + "/"+userId;

        public string PostBasketItem => _apiUrls.Value.BaseUrl + _apiUrls.Value.PostBasketItem;

        public string PutChangeItemQuantity(int basketItemId, int quantity) => _apiUrls.Value.BaseUrl + _apiUrls.Value.PutChangeItemQuantity + basketItemId + "/" + quantity;

        public string DeleteBasketItems(int userId) => _apiUrls.Value.BaseUrl + _apiUrls.Value.DeleteBasketItems + "/" + userId;

        public string DeleteItemFromBasket(int basketItemId) => _apiUrls.Value.BaseUrl + _apiUrls.Value.DeleteItemFromBasket + basketItemId;

    } 

}
