using Ecommerce.Model.Dto;
using Ecommerce.Web.Controllers;
using Ecommerce.Web.Model;
using Ecommerce.Web.Repositories;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Test.WebControllers
{
    public class ProductControllerTest
    {
        private ProductController _productController;
        private Mock<IProductRepository> _iProductRepositoryMock = new Mock<IProductRepository>();

        public ProductControllerTest()
        {
            _productController = new ProductController(_iProductRepositoryMock.Object);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfProduct()
        {
            // Arrange
            var productDbSetMock = Builder<ProductViewModel>.CreateListOfSize(5).Build();
            _iProductRepositoryMock.Setup(repo => repo.GetProducts()).Returns(productDbSetMock);
            

            // Act
            var result = _productController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IList<ProductViewModel>>(viewResult.Model);
            Assert.Equal(5, model.Count);
        }

        [Fact]
        public void ProductDetail_ReturnsAViewResult_WithAProduct()
        {
            // Arrange
            var productDbSetMock = Builder<ProductViewModel>.CreateNew().Build();
            _iProductRepositoryMock.Setup(repo => repo.GetProductById(1)).Returns(productDbSetMock);


            // Act
            var result = _productController.ProductDetail(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

    }
}
