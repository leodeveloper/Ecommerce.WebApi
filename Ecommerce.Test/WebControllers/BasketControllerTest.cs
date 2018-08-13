using Ecommerce.Web.Controllers;
using Ecommerce.Web.Model;
using Ecommerce.Web.Repositories;
using FizzWare.NBuilder;
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
            //_iBasketRepositoryMock.Setup(repo => repo.GetBasketItem()).Returns(basketDbSetMock);


            //// Act
            //var result = _productController.Index();

            //// Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IList<ProductViewModel>>(viewResult.Model);
            //Assert.Equal(5, model.Count);
        }
    }
}
