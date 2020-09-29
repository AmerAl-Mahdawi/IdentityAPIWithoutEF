# IdentityAPIWithoutEF
This is an API application that uses ASP.NET Core 3.1 Identity without relying on Entity Framework.

It consists of three projects:
* **IdentityAPIWithoutEF_DB:** This is a SQL Server project for creating a database with three tables (Users, Roles, and UserRoles) and all the needed Stored Procedures for this application.
* **IdentityAPIWithoutEF:** This project takes care of the application's APIs and all thier related stuff like Email Services, Customised Attributes, and Swagger Documintation.
* **IdentityAPIWithoutEF.Library:** This project implements all the needed Identity intefaces for this application and takes care of the communication with the database

# Reasons Behind this implementation:
## Why Removing Entity Framework
Creating a membership management system that has the right features for any application is not an easy task. ASP.NET Identity implemented most of these features and it is easy to be extended and customized. But it relies mainly on Entity Framework. Fortunately, it provides the ability to remove the Entity Framework integration by implementing some interfaces.
I am not a fan of using Entity Framework bacause it needs to have full access to the database and communicate using sql queries (**like Select * ...**). This (in my opinion) introduces many security issues as it is prefarable to use Stored Procedures instead in order to remove the possibility of sql injection threats.
In addition to that, testing and fixing bugs will not be easy without knowing how Entity Framework works. This means you need to have long experiance and good understanding on how Entity Framework works in order to be able to asolate issues and fix them.

## WHy Just API:
In order to build a real world system, it is better to separate the APIs project from the UI project. That is why this application has only APIs with Swagger documination page. So all the UI pages can be added into a separate UI project.

# What Services Included:
This is a simple application that has only one service integrated; email service. So when a new user register himself, the application will send an email with a link to confirm the provided email address. Clicking the link will update the user's information in the database whith this confirmation.
This service should be configured in the appsettings.json file

# Steps to Run The Application:
1. Right click **IdentityAPIWithoutEF_DB project** --> Publish --> Configure the SQL server information --> Publish
2. Configure appsettings.json file with the created database's ConnectionString, and configure the Email service at the button of this file
3. Set **IdentityAPIWithoutEF** as the startup project
4. Run the application
