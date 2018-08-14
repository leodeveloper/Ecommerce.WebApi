using Ecommerce.Model.Dto;
using Ecommerce.Service.Interface;
using Ecommerce.WebApi.Controllers;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Test.ApiControllers
{
    public class ShoppingCartControllerTest
    {
        private ShoppingCartController _shoppingCartController;
        private Mock<IBasketService> _iBasketServiceMock = new Mock<IBasketService>();

        public ShoppingCartControllerTest()
        {
            _shoppingCartController = new ShoppingCartController(_iBasketServiceMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsBasketItemList()
        {
            // Arrange     
            var basketItemDbSetMock = Builder<BasketItem>.CreateListOfSize(5).Build();
            _iBasketServiceMock.Setup(m => m.GetBasketItemsAsync(1)).Returns(Task.FromResult(basketItemDbSetMock));

            // Act
            var result = await _shoppingCartController.GetBasketItems(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as IList<BasketItem>;
            Assert.NotNull(content);

            Assert.Equal(5, content.Count);
        }

        [Fact]
        public async Task Post_Basket_Item()
        {
            // Arrange            
            var getresult = await Task.FromResult<BasketItem>(Builder<BasketItem>.CreateNew().Build());

            // Act
            var result = await _shoppingCartController.Post(getresult);

            // Assert
            Assert.NotNull(result);
            var objectResult = result as CreatedResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as BasketItem;
            Assert.NotNull(content);
        }

        [Fact]
        public async Task Put_Change_Basket_Item_Quanitity()
        {
            // Arrange     
            var basketItemDbSetMock = Builder<BasketItem>.CreateListOfSize(5).Build();
            _iBasketServiceMock.Setup(m => m.ChangeBasketItemQuantityAsync(1, 2)).Returns(Task.FromResult(basketItemDbSetMock));

            // Act
            var result = await _shoppingCartController.ChangeItemQuantity(1, 2);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as IList<BasketItem>;
            Assert.NotNull(content);
        }

        [Fact]
        public async Task Delete_Basket()
        {
            // Arrange     
            var basketItemDbSetMock = Builder<BasketItem>.CreateListOfSize(5).Build();
            _iBasketServiceMock.Setup(m => m.ClearBasketAsync(1)).Returns(Task.FromResult(basketItemDbSetMock));

            // Act
            var result = await _shoppingCartController.ClearBasket(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as IList<BasketItem>;
            Assert.NotNull(content);
            
        }

        [Fact]
        public async Task Delete_Item_From_Basket()
        {
            // Arrange     
            var basketItemDbSetMock = Builder<BasketItem>.CreateListOfSize(5).Build();
            _iBasketServiceMock.Setup(m => m.DeleteBasketItemByIdAsync(1)).Returns(Task.FromResult(basketItemDbSetMock));

            // Act
            var result = await _shoppingCartController.DeleteItemFromBasket(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as IList<BasketItem>;
            Assert.NotNull(content);

        }
    }
}
