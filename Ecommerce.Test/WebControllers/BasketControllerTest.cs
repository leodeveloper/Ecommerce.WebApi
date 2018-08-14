using Ecommerce.Web.Controllers;
using Ecommerce.Web.Model;
using Ecommerce.Web.Repositories;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ecommerce.Test.WebControllers
{
    public class BasketControllerTest
    {

        private BasketController _basketController;
        private Mock<IBasketRepository> _iBasketRepositoryMock = new Mock<IBasketRepository>();

        public BasketControllerTest()
        {
            _basketController = new BasketController(_iBasketRepositoryMock.Object);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfBasketItem()
        {
            // Arrange
            var basketDbSetMock = Builder<BasketItemViewModel>.CreateListOfSize(5).Build();
            _iBasketRepositoryMock.Setup(repo => repo.GetBasketItem(1)).Returns(basketDbSetMock);


            //// Act
           var result = _basketController.Index();

            //// Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IList<BasketItemViewModel>>(viewResult.Model);
            Assert.Equal(5, model.Count);
        }

        [Fact]
        public void ChangeQuantity_Returns_RedirectToActionResult()
        {
            // Arrange
            var basketDbSetMock = Builder<BasketItemViewModel>.CreateListOfSize(5).Build();
            _iBasketRepositoryMock.Setup(repo => repo.GetBasketItem(1)).Returns(basketDbSetMock);


            //// Act
            var result = _basketController.ChangeQuantity(1, 4);

            //// Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);           
            Assert.Equal("Index", viewResult.ActionName);
        }

        [Fact]
        public void AddItemIntoBasket_Returns_RedirectToActionResult()
        {
            // Arrange
            var basketDbSetMock = Builder<BasketItemViewModel>.CreateListOfSize(5).Build();
            _iBasketRepositoryMock.Setup(repo => repo.GetBasketItem(1)).Returns(basketDbSetMock);


            //// Act
            var result = _basketController.AddItemIntoBasket(new AddBasketItemViewModel { Id=1,Quantity=6,UserId=1 });

            //// Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);
        }

        [Fact]
        public void DeleteItemIntoBasket_Returns_RedirectToActionResult()
        {
            // Arrange
            var basketDbSetMock = Builder<BasketItemViewModel>.CreateListOfSize(5).Build();
            _iBasketRepositoryMock.Setup(repo => repo.GetBasketItem(1)).Returns(basketDbSetMock);


            //// Act
            var result = _basketController.DeleteItemFromBasket(1);

            //// Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);
        }

        [Fact]
        public void DeleteAllItemIntoBasket_Returns_RedirectToActionResult()
        {
            // Arrange
            var basketDbSetMock = Builder<BasketItemViewModel>.CreateListOfSize(5).Build();
            _iBasketRepositoryMock.Setup(repo => repo.GetBasketItem(1)).Returns(basketDbSetMock);


            //// Act
            var result = _basketController.DeleteAllBasketItems(1);

            //// Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);
        }
    }
}
