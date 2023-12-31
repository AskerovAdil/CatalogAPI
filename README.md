# Directory Structure
The project structure consists of three main folders: API/CatalogAPI, DataAccess/DataAccess, and Domain/Domain. The DataAccess folder contains the Entity Framework Core DbContext class and the EntityBaseRepository class, which is a generic repository class that provides basic CRUD operations for entities. The Domain folder contains the entity classes, and the API folder contains the controllers and services

The EntityBaseRepository class is a generic repository class that provides basic CRUD operations for entities. It takes a type parameter T that specifies the entity type. The class implements the IEntityBaseRepository interface, which defines the basic CRUD operations. The class constructor takes an instance of the ApplicationDbContext class, which is the Entity Framework Core DbContext class. The class provides methods for adding, updating, deleting, and retrieving entities from the database.

### Demo swagger
<img src="https://github.com/AskerovAdil/CatalogAPI/blob/master/swaggerdemo.png" />

## Authors

* **Askerov Adil** - *.NET Developer* - [Askerov Adil](https://github.com/AskerovAdil) - *API Server*

 
