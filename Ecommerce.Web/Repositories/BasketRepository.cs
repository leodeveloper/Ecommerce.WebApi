using AutoMapper;
using Newtonsoft.Json;
using Ecommerce.Model.Dto;
using Ecommerce.Web.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.WebApi.Client.Interface;
using Ecommerce.WebApi.Client.Repository;
using static Ecommerce.WebApi.Client.Helper.HelperEnum;
using Ecommerce.WebApi.Client.ExtentionMethod;

namespace Ecommerce.Web.Repositories
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IGetRequestManager _iGetRequestManager;
        private readonly IPostRequestManager _iPostRequestManager;
        private readonly IPutRequestManager _iPutRequestManager;
        private readonly IDeleteRequestManager _iIDeleteRequestManager;
        private readonly IGetApiUrls _iGetApiUrls;
        private readonly IMapper _mapper;

        public BasketRepository(IGetRequestManager iGetRequestManager, IPostRequestManager iPostRequestManager, IPutRequestManager iPutRequestManager, IDeleteRequestManager iIDeleteRequestManager, IGetApiUrls iGetApiUrls, IMapper mapper)
        {
            _iGetRequestManager = iGetRequestManager;
            _iPostRequestManager = iPostRequestManager;
            _iPutRequestManager = iPutRequestManager;
            _iIDeleteRequestManager = iIDeleteRequestManager;
            _iGetApiUrls = iGetApiUrls;
            _mapper = mapper;
            
        }

        /// <summary>
        /// Get All basket item
        /// </summary>
        /// <returns></returns>
        public IList<BasketItemViewModel> GetBasketItem()
        {
            string apiResponse = _iGetRequestManager.SendRequest(_iGetApiUrls.GetBasketItem, "", "", false, HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            return Convert_ApiResponse_ToListBaskitemViewModel(apiResponse);
        }
       
        /// <summary>
        /// Add Item into Basket
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public BasketItemViewModel AddItemintoBasket(int productId, int quantity, int userId)
        {
            BasketItem basketItem = new BasketItem();
            basketItem.ProductId = productId;
            basketItem.Quantity = quantity;
            basketItem.UserId = userId;
            string jsonsContent = JsonConvert.SerializeObject(basketItem);
            string apiResponse = _iPostRequestManager.SendRequest(_iGetApiUrls.PostBasketItem, jsonsContent, "", "", false, HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            GenericResponseRepository<BasketItem> response = new GenericResponseRepository<BasketItem>();
            basketItem = response.Convert(apiResponse);
            return _mapper.Map<BasketItemViewModel>(basketItem);
        }

        /// <summary>
        /// Update Quantity of basket item
        /// </summary>
        /// <param name="basketItemId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public IList<BasketItemViewModel> UpdateBasketItemQuantity(int basketItemId, int quantity)
        {    
            string apiResponse = _iPutRequestManager.SendRequest(_iGetApiUrls.PutChangeItemQuantity(basketItemId, quantity), null, "", "", false, HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            return Convert_ApiResponse_ToListBaskitemViewModel(apiResponse);
        }

        /// <summary>
        /// Delete item from basket
        /// </summary>
        /// <param name="basketItemId"></param>
        /// <returns></returns>
        public IList<BasketItemViewModel> DeleteItemFromBasket(int basketItemId)
        {
            string apiResponse = _iIDeleteRequestManager.SendRequest(_iGetApiUrls.DeleteItemFromBasket(basketItemId), null, "", "", false, HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            return Convert_ApiResponse_ToListBaskitemViewModel(apiResponse);
        }

        /// <summary>
        /// Clear all item from basket
        /// </summary>
        /// <returns></returns>
        public IList<BasketItemViewModel> DeleteAllBasketItems(int userId)
        {
            string apiResponse = _iIDeleteRequestManager.SendRequest(_iGetApiUrls.DeleteBasketItems(userId), null, "", "", false, HttpRequestContentType.ApplicationJson.GetDescription(), null);
            if (apiResponse == null)
                return null;

            return Convert_ApiResponse_ToListBaskitemViewModel(apiResponse);
        }


        #region Private Method
        /// <summary>
        /// convert Api response into list basket view model
        /// </summary>
        /// <param name="apiResponse"></param>
        /// <returns></returns>
        private IList<BasketItemViewModel> Convert_ApiResponse_ToListBaskitemViewModel(string apiResponse)
        {
            GenericResponseRepository<BasketItem> response = new GenericResponseRepository<BasketItem>();
            IList<BasketItem> basketItem = response.Convert_to_List(apiResponse);
            return _mapper.Map<IList<BasketItemViewModel>>(basketItem);
        }
        #endregion
    }
}
