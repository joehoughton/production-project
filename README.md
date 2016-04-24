# Production Project

### Frameworks - Libraries
  - ASP.NET Web API 
  - Entity Framework
  - DI: Autofac
  - Database Migration: DbUp
  - JavaScript: Backbone, Marionette, Require, Handlebars, Grunt
  - Package Manager: Bower
  - Testing: NUnit, Specflow
  
### Requirements
To run the application locally you must have the following installed:
* Ruby
* Node Js
* SQL Server 
* IIS
* Visual Studio 2013 (or above)

### Installation
 1. Build solution to restore NuGet packages
 2. Rebuild solution
 3. Change the connection strings and specify a database name at production-project.Domain/App.config and     production-project.Web/Web.config
 4. Create the local database in SQL Server, matching the name provided in each connection string
 5. Right click production-project.Database > Debug > Start new instance to run migration scripts
 6. Create a local site in IIS and set the port number to 6662
 7. Select the site and point the physical path to the location of production-project.Web on your machine
 
### Running the Application
Using the command line, from the root of the project:
```sh
grunt build && grunt serve
```
