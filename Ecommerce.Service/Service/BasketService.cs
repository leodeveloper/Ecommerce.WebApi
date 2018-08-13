using Ecommerce.Model.Dto;
using Ecommerce.Model.EntityFrameWork;
using Ecommerce.Model.GenericRepository.Repository;
using Ecommerce.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Service
{
    public class BasketService : IBasketService
    {   
        private readonly IRepository _iRepository;
        public BasketService(IRepository iRepository)
        {           
            _iRepository = iRepository;
        }

        public async Task<BasketItem> AddItemintoBasketAsync(BasketItem basketItem)
        {
            IEnumerable<BasketItem> basketItems = await _iRepository.GetAllAsync<BasketItem>();
            //this is for primeray key generate becuase the actuall database is not not connected, once we have actuall db connected the following live of code will be remove
            if (basketItems.Count() > 0)
            {
                basketItem.Id = basketItems.Last().Id + 1;
            }
            else
            {
                basketItem.Id = 1;
            }

            _iRepository.Create<BasketItem>(basketItem);
            await _iRepository.SaveAsync();            
            return basketItem;
        }

        public async Task<IList<BasketItem>> GetBasketItemsAsync()
        {
            var basketItems = await _iRepository.GetAllAsync<BasketItem>();
            basketItems = PopulateProductIntoBasketItem(basketItems.ToList());
            return basketItems.ToList();
        }

        public async Task<IList<BasketItem>> ClearBasketAsync(int userId)
        {
            var basketItems = await _iRepository.GetAsync<BasketItem>(b => b.UserId == userId); 

            foreach(var basketItem in basketItems)
            {
                _iRepository.Delete<BasketItem>(basketItem);
            }            
            await _iRepository.SaveAsync();       

            return await GetBasketItemsAsync();
        }

        public async Task<IList<BasketItem>> DeleteBasketItemByIdAsync(int id)
        {
            var basketItem = await _iRepository.GetByIdAsync<BasketItem>(id);
            if (basketItem != null)
            {
                _iRepository.Delete<BasketItem>(basketItem);
                await _iRepository.SaveAsync();
            }
                    
            return await GetBasketItemsAsync();
        }

        public async Task<IList<BasketItem>> ChangeBasketItemQuantityAsync(int id, int quantity)
        {
            var basketItem = await _iRepository.GetByIdAsync<BasketItem>(id);

            if (basketItem == null)
                return null;

            basketItem.Quantity = quantity;

            _iRepository.Update<BasketItem>(basketItem);
            await _iRepository.SaveAsync();           
            return await GetBasketItemsAsync();
        }

        #region Private Helper
        /// <summary>
        /// this method will be absolute when we have the actuall database, in memory database does not supoort relation database
        /// </summary>
        private List<BasketItem> PopulateProductIntoBasketItem(List<BasketItem> basketItems)
        {
            foreach (var basketItem in basketItems)
            {
                basketItem.Product = _iRepository.GetById<Product>(basketItem.ProductId); 
            }

            return basketItems;
        }

        #endregion
    }
}
