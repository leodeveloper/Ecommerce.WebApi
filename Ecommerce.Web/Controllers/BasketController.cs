using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Web.Model;
using Ecommerce.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class BasketController : Controller
    {

        private readonly IBasketRepository _iBasketRepository;
        
        public BasketController(IBasketRepository iBasketRepository)
        {
            _iBasketRepository = iBasketRepository;
        }        

        public IActionResult Index()
        {
            IList<BasketItemViewModel>  basketViewModel = _iBasketRepository.GetBasketItem(UserContext.getUserId);
            return View(basketViewModel);
        }

        [HttpPost]
        public IActionResult AddItemIntoBasket(AddBasketItemViewModel model)
        {
            BasketItemViewModel basketViewModel = _iBasketRepository.AddItemintoBasket(model.Id, model.Quantity, model.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult ChangeQuantity(int basketItemId, int quantity)
        {
            IList<BasketItemViewModel> basketViewModel = _iBasketRepository.UpdateBasketItemQuantity(basketItemId, quantity);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItemFromBasket(int basketItemId)
        {
            IList<BasketItemViewModel> basketViewModel = _iBasketRepository.DeleteItemFromBasket(basketItemId);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAllBasketItems(int userId)
        {
            IList<BasketItemViewModel> basketViewModel = _iBasketRepository.DeleteAllBasketItems(userId);
            return RedirectToAction("Index");
        }       
    }

    /// <summary>
    /// there is no user login and authentication implemented, once we have the proper login this methos will absolute
    /// </summary>
    public class UserContext
    {
        public static int getUserId { get { return 1; } }
    }
}