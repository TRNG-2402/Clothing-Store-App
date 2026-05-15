# Clothing-Store-App Overview
Group 2: Part 2 of the C# project.

This full-stack E-commerce Clothing Store App allows users to browse clothing products by category, filter and paginate results, manage a personal cart, and place simulated orders. The backend exposes a RESTful API secured with JWT authentication, and the frontend is a React Single Page Application that consumes those endpoints.

Core environments and technologies: C# w/ EntityFramework, ASP.NET for API/Backend, SQL Server for Database, JWT Authentication, React Frontend, CSS.

# Features: 
A Product Recommender can provide users with product suggestions based on their location's weather information and the current selection of available items. This feature makes a call to an external API and, based on the returned weather data, decides which clothing items would be best suited to the user's immediate needs.

Users can register an account by entering a name, unique email, and password. Login with JWT token is issued on success. User profiles can be viewed and updated. User order history can be viewed. User accounts can be deleted.

Products can be added, deleted, and updated by admins. Anyone can browse products using category filter, price range filter, search, and pagination. Each product has its own individual product detail page that can be viewed.

Cart belongs to a user. A user's cart can have products added to it, subtracted from it, and it can have its item quantities updated. On checkout, the user's cart would be cleared out.

Orders can be placed when viewing a cart's contents. All past orders for a user can be viewed and sorted. Each order has a more detailed order view that contains that order's items.

Categories can be browsed by anyone. Admins can add, update, and delete categories. Each product has a category associated with it. A category can be clicked on in order to filter products by that category.

Sales can be created, updated, and deleted by admins. Sales have a start date and an end date. Active sales apply a discount percentage to products within a given category. Sale price is reflected on product listings.

# API Endpoints
POST /api/auth/register - Public, register a new user

POST /api/auth/login - Public, login and receive JWT

GET /api/user/{id} - User, get user profile

GET /api/user/{id}/orders - User, get all orders for a user

PATCH /api/user/{id} - User, update name or email

GET /api/product - Public, get all products

GET /api/product/{id} - Public, get product by ID

POST /api/product - Admin, create a product

PUT /api/product/{id} - Admin, update a product

DELETE /api/product/{id} - Admin, delete a product

GET /api/category - Public, get all categories

GET /api/category/{id} - Public, get category by ID

GET /api/category/{id}/products - Public, get products in a category

POST /api/category - Admin, create a category

PUT /api/category/{id} - Admin, update a category

DELETE /api/category/{id} - Admin, delete a category

GET /api/order/{id} - User, get order by ID

POST /api/order - User, place a new order

PATCH /api/order/{id} - User, update order status

GET /api/cart/{userId} - User, get user's cart

POST /api/cart/{userId}/items - User, add item to cart

PATCH /api/cart/{userId}/items/{productId} - User, update cart item quantity

DELETE /api/cart/{userId}/items/{productId} - User, remove item from cart

DELETE /api/cart/{userId} - User, clear entire cart

GET /api/sale - Public, get all sales

GET /api/sale/active - Public, get currently active sales

GET /api/sale/{id} - Public, get sale by ID

POST /api/sale - Admin, create a sale

PUT /api/sale/{id} - Admin, update a sale

DELETE /api/sale/{id} - Admin, delete a sale
