# BookStoreManagement

BookStoreManagement is a simple ASP.NET Core Web API project that demonstrates how to build a CRUD (Create, Read, Update, Delete) API using MongoDB as the database.

## Project Structure

```
BookStoreManagement/
│
├── Controllers/
│   └── BooksController.cs        # API endpoints for managing books
│
├── Models/
│   ├── Books.cs                  # Book model with MongoDB mapping
│   └── BookstoreDatabaseSettings.cs # Configuration settings for MongoDB
│
├── Services/
│   └── BookServices.cs           # Service layer for MongoDB operations
│
├── appsettings.json              # Application configuration
├── appsettings.Development.json  # Development-specific configuration
```

## Configuration

Update the `appsettings.json` file with your MongoDB connection details:

```json
{
  "BookstoreDatabaseSettings": {
    "ConnectionString": "<your-mongodb-connection-string>",
    "DatabaseName": "BookstoreDb",
    "BooksCollectionName": "Books"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## API Endpoints

### Books API

Base route: `/api/books`

| HTTP Method | Route             | Description                         |
| ----------- | ----------------- | ----------------------------------- |
| GET         | `/api/books`      | Retrieve all books                  |
| GET         | `/api/books/{id}` | Retrieve a single book by Id        |
| POST        | `/api/books`      | Create a new book                   |
| POST        | `/api/books/bulk` | Create multiple books (bulk insert) |
| PUT         | `/api/books/{id}` | Update a book by Id Completely/Partially|
| DELETE      | `/api/books/{id}` | Delete a book by Id                 |

## Results

Below are the results from running the API. All images are available in the `Results/` folder of the repository.

### BookStoreAPI-SwaggerUI
![BookStoreAPI-SwaggerUI](https://github.com/Chandan-Ubale/BookStoreManagement/blob/main/Results/BookStoreApi.png?raw=true)
### Get All Books
![Get All Books](https://github.com/Chandan-Ubale/BookStoreManagement/blob/main/Results/GetData.png?raw=true)

### Get Book by Id
![Get Book by Id](https://github.com/Chandan-Ubale/BookStoreManagement/blob/main/Results/GetDataById.png?raw=true)

### Create a Book
![Create a Book](https://github.com/Chandan-Ubale/BookStoreManagement/blob/main/Results/PostSingle.png?raw=true)

### Bulk Insert Books
![Bulk Insert Books](https://github.com/Chandan-Ubale/BookStoreManagement/blob/main/Results/PostBulkData.png?raw=true)

### Update a Book (Partial)
![Update a Book](https://github.com/Chandan-Ubale/BookStoreManagement/blob/main/Results/PutDataByIdPartial.png?raw=true)

### Update a Book (Full)
![Update a Book](https://github.com/Chandan-Ubale/BookStoreManagement/blob/main/Results/PutDataById.png?raw=true)

### Delete a Book
![Delete a Book](https://github.com/Chandan-Ubale/BookStoreManagement/blob/main/Results/DeleteById.png?raw=true)
