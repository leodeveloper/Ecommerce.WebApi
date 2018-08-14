
Shopping cart basket web application, web api, web api client application, Asp.net core, asp.net core web api, entityframwork core in memory

This is prototype web api, web application and client application.

The solution consist following projects

Ecommerce.WebApi (Asp.net web api core)

Ecommerce.Web (Asp.net web application core)

Ecommerce.WebApi.Client (C# client liberary core)

Ecommerce.Model (C# Model Layer)

Ecommerce.Service (C# service Layer)

Ecommerce.Test (Xunit Test project)


# Ecommerce.WebApi (Asp.net web api core)

Settings

Ecommerce.WebApi Under Properties there is file called launchsettings.json

Index api    "launchUrl": "api/product/",
 
Enviroment     "environmentVariables": {"ASPNETCORE_ENVIRONMENT": "Development"},
 
Base Api Url        "applicationUrl": "http://localhost:61493"
 
 
Ecommerce.WebApi 

Database Settings is in the startup.cs

Under Configuration method, For in memory storage using Microsoft.EntityFrameworkCore.InMemory 

EF Core database providers do not have to be relational databases. InMemory is designed to be a general purpose database for testing, and is not designed to mimic a relational database.

services.AddDbContext<EnityFramWorkDbContext>(opt => opt.UseInMemoryDatabase("TestDb"));
      
Switch from in memory database to actuall database by change the single line of code.

In startup.cs these is a method calls AddTestData, this method is populate the data into Product table and BasketItem table

The default route for api is http://localhost:61492/api/product/

you can change the default route in startup.cs

# Swegger
Swagger is also configure in this web api project, the url for swagger is http://localhost:61492/swagger

# Curl 

Curl command for api end points 

curl -i -H "Content-Type: application/json" http://localhost:61492/api/Product/ProductDetail?productId=1

curl -i -H "Content-Type: application/json" http://localhost:61492/api/Product

curl -i -H "Content-Type: application/json" http://localhost:61492/api/Shoppingcart/GetBasketItems/1

curl -i -H "Content-Type: application/json" -X POST -d "{'ProductId':1,'Product':null,'Quantity':4} " http://localhost:61492/Api/ShoppingCart/

curl -i  -H "Content-Type: application/json" -X PUT -d "{'productid':1, 'quantity'=6}" http://localhost:61492/Api/ShoppingCart/ChangeItemQuantity/1/6

curl -i  -H "Content-Type: application/json" -X DELETE http://localhost:61492/Api/ShoppingCart/DeleteItemFromBasket/1

curl -i  -H "Content-Type: application/json" -X DELETE http://localhost:61492/Api/ShoppingCart/ClearBasket/1

# Ecommerce.Web (Asp.net web application core)

Front end client application for using the end point of Eccomerce.WebApi

there are three pages

1. Product page 

   http://localhost:58458/Product display list of product
   
   Screen shot 
   ![alt text](https://github.com/leodeveloper/shoppingcartbasket/blob/master/Product.PNG)
   
2. Product detail page

   http://localhost:58458/Product/ProductDetail?productId=2 display the product detail and also add this product in to basket
   
   Screen shot 
   ![alt text](https://github.com/leodeveloper/shoppingcartbasket/blob/master/ProductDetailPage.PNG)
   
3. Basket page 

   http://localhost:58458/Basket display the list of item in the basket also change the quantity of the basket item and remove the items    from the basket.
   
   Screen shot 
   ![alt text](https://github.com/leodeveloper/shoppingcartbasket/blob/master/Baket.PNG)

