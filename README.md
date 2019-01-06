# Project Specification

**“Pizza Lab”** is a web application for a Pizza restaurant. The client side is a single page app, built with **Angular 6**. The server is built on **ASP.NET CORE 2.2** and it is using **SQL Server** for storing the data. **Redux** is used as a state management library in the front-end app. Redux is a predictable state container for JavaScript apps. It helps you write applications that behave consistently, run in different environments (client, server, and native), and are easy to test. The application consists of users, pizza products and orders. Each user can register, login and logout. Users can also search for the pizza they are looking for, view each pizza details and make orders. Admins can add, edit and delete pizza entries and approve orders.

# Functionality 

##### •	User Login 
    - o Login in current application using email and password of already registered user. 
##### •	User Register 
    - o Register a new user by providing email, password and username. 
##### •	User Logout 
    - o Logouts from the application. 
##### •	Home 
    - o List top-six pizzas by user likes
    - o Add movie comments  
    - o Add movie vote. Each user can only vote once (1 – 10 scale) 
    - o Add movie review. 
##### •	Menu
    - o List all pizzas. Nine per page ordered alphabetically.  
    - o Search pizzas by their name
    - o Add pizza to the cart or view details
##### •	Pizza Details
    - o Show pizza details
    - o Add pizza review
    - o Each user can like/unlike the pizza
##### •	Cart
    - o Users add pizzas to the cart
    - o Users select quantity of the chosen product
    - o Users have option to remove product from the cart or refresh the quantity to one
    - o Users have option to checkout or to continue shopping
##### •	My orders
    - o List user orders
    - o Navigate to order details
##### •	Order Details
    - o Shows full order details
##### •	Pizza add 
    - o Admin route only
    - o Create a new pizza entry and save it to the database
##### •	Pizza edit 
    - o Admin route only
    - o Edit existing pizza entry and save it to the database
##### •	Pizza delete
    - o Admin route only
    - o Remove pizza entry from the database
##### •	Pending orders 
    - o Admin route only
    - o View all pending orders
    - o Navigate to order details
    - o Approve order
##### •	Approved orders 
    - o Admin route only
    - o View all approved orders
    - o Navigate to order details
##### •	Category add 
    - o Admin route only
    - o Create a new category and save it to the database
##### •	Ingredient add 
    - o Admin route only
    - o Create a new ingredient and save it to the database
