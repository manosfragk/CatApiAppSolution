# **Cat API Service**

This project is an ASP.NET Core Web API that fetches 25 cat images from the TheCat API, stores them in a SQL Server database, and provides endpoints for retrieving this data with paging and filtering support. The service also stores tags representing the cats' temperaments.

## **Features**
- Fetch 25 cat images from the external API and store them in a SQL Server database.
- Store cat details including width, height, and associated tags (temperament/breed).
- Retrieve cats with paging and tag-based filtering.
- RESTful API with easy-to-use endpoints.

## **Prerequisites**

Before you begin, ensure you have met the following requirements:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server Express installed and running.
- [Docker](https://www.docker.com/get-started) (if you want to run the application in containers).
- [Postman](https://www.postman.com/) or [Swagger](https://swagger.io/) to test the API (optional).
- IDE such as [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/).

---

## **Installation and Setup (Running Locally)**

### **1. Clone the Repository**

```bash
git clone https://github.com/manosfragk/CatApiAppSolution.git
cd CatApiAppSolution
```

### **2. Configure the Database Connection**

1. Open the `appsettings.json` file and locate the `"ConnectionStrings"` section:
    ```json
    "ConnectionStrings": {
          "DefaultConnection": "Server=localhost,1433;Database=CatApiDB;User Id=sa;Password=m3r3d@230592!;Trusted_Connection=False;Encrypt=False"
    }
    ```

2. Replace `your-server` with your SQL Server instance name. If you're using SQL Server Express, it might be something like `localhost\SQLEXPRESS`.

3. Optionally, you can configure SQL Server authentication (if not using `Trusted_Connection=True`).

### **3. Install Dependencies**

Navigate to the project root and run the following command to restore NuGet packages:
```bash
dotnet restore
```

### **4. Set Up the Database**

1. **Add Migrations**: 
   Generate migrations for the database:
   ```bash
   dotnet ef migrations add InitialCreate
   ```

2. **Apply Migrations**: 
   Apply the migrations to the database to create the necessary tables:
   ```bash
   dotnet ef database update
   ```

This will create the database schema for the `CatEntity` and `TagEntity`.

### **5. Build and Run the Application**

1. **Build the Application**:
   ```bash
   dotnet build
   ```

2. **Run the Application**:
   ```bash
   dotnet run
   ```

The application will start, and you can access it at `https://localhost:7151` (by default). Swagger UI will be available at `https://localhost:7151/swagger`.

---

## **Running the Application with Docker**

You can run both the API and the SQL Server database in Docker containers using **Docker Compose**.

### **1. Build the Docker Image**

First, make sure Docker is installed and running. Then, from the project root directory (where the `Dockerfile` is located), build the Docker image for the application:

```bash
docker build -t catapp .
```

### **2. Start the Application Using Docker Compose**

Run the following command to start both the **API** and **SQL Server** containers:

```bash
docker-compose up
```

### **3. Stopping and Cleaning Up Using Docker Compose**

To stop the Docker containers, press CTRL + C in the terminal where docker-compose up is running, or run the following command:

```bash
docker-compose down
```

## **Using the API**

### **1. Fetch and Store Cats**
To fetch 25 cat images from the external API and store them in your database, send a `POST` request to:

```
POST /api/cats/fetch
```

### **2. Retrieve Cat by ID**
To retrieve a specific cat by its database ID, send a `GET` request to:

```
GET /api/cats/{id}
```

Replace `{id}` with the actual cat's ID.

### **3. Retrieve Cats with Paging**
To retrieve cats with paging support, send a `GET` request to:

```
GET /api/cats?page=1&pageSize=10
```

This retrieves the first 10 cats. Adjust the `page` and `pageSize` parameters as needed.

### **4. Retrieve Cats by Tag with Paging**
To retrieve cats filtered by a specific tag (e.g., playful) with paging support, send a `GET` request to:

```
GET /api/cats?tag=playful&page=1&pageSize=10
```

This retrieves the first 10 cats that have the tag "playful."


## **Testing**

You can test the API endpoints using:
- **Postman**: Import the endpoints and test by sending requests.
- **Swagger**: Navigate to `https://localhost:7151/swagger` (or `http://localhost:8080/swagger` when using Docker) to access the Swagger UI and interact with the API.

## **Project Structure**

The project follows a layered architecture to keep concerns separated:

- **Controllers**: Contains the API controllers.
- **Models**: Contains the entity models for the database (CatEntity, TagEntity).
- **Services**: Contains the business logic for fetching, storing, and retrieving cat data.
- **Data**: Contains the DbContext for interacting with the SQL Server database.

## **Technologies Used**

- **.NET 8**: Web API framework.
- **Entity Framework Core**: ORM for database access.
- **SQL Server**: Database for storing cats and tags.
- **Refit**: To interact with the external Cat API.
- **Swagger**: For API documentation.
- **Docker**: For containerizing the application and database.

## **License**

This project is licensed under the MIT License.
