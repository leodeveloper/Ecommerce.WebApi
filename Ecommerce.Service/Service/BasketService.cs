using Ecommerce.Model.Dto;
using Ecommerce.Model.EntityFrameWork;
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
        private readonly EnityFramWorkDbContext _context;
        public BasketService(EnityFramWorkDbContext context)
        {
            _context = context;
        }

        public async Task<BasketItem> AddItemintoBasketAsync(BasketItem basketItem)
        {
            IList<BasketItem> basketItems = await _context.BasketItems.ToListAsync();
            //this is for primeray key generate becuase the actuall database is not not connected, once we have actuall db connected the following live of code will be remove
            if (basketItems.Count > 0)
            {
                basketItem.Id = basketItems.Last().Id + 1;
            }
            else
            {
                basketItem.Id = 1;
            }

            await _context.BasketItems.AddAsync(basketItem);
            await _context.SaveChangesAsync();
            return basketItem;
        }

        public async Task<IList<BasketItem>> GetBasketItemsAsync()
        {
            var basketItems = await _context.BasketItems.ToListAsync();
            basketItems = PopulateProductIntoBasketItem(basketItems);
            return basketItems;
        }

        public async Task<IList<BasketItem>> ClearBasketAsync(int userId)
        {
            var basketItems = await _context.BasketItems.Where(b => b.UserId == userId).ToListAsync();
            _context.BasketItems.RemoveRange(basketItems);
            await _context.SaveChangesAsync();
            return await GetBasketItemsAsync();
        }

        public async Task<IList<BasketItem>> DeleteBasketItemByIdAsync(int id)
        {
            var basketItem = await _context.BasketItems.FindAsync(id);
            if (basketItem == null)
                return null;

            _context.BasketItems.Remove(basketItem);
            await _context.SaveChangesAsync();
            return await GetBasketItemsAsync();
        }

        public async Task<IList<BasketItem>> ChangeBasketItemQuantityAsync(int id, int quantity)
        {
            var basketItem = await _context.BasketItems.FindAsync(id);

            if (basketItem == null)
                return null;

            basketItem.Quantity = quantity;
           
            _context.Entry(basketItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
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
                basketItem.Product = _context.Products.Find(basketItem.ProductId);
            }

            return basketItems;
        }

        #endregion
    }
}
