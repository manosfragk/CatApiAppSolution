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

2. Replace `Server` with your SQL Server instance name.

### **3. Install Dependencies**

Navigate to the project root and run the following command to restore NuGet packages:

```bash
dotnet restore
```

### **4. Build and Run the Application**

1. **Build the Application**:
   ```bash
   dotnet build
   ```

2. **Run the Application**:

Since migrations are automatically applied at startup, you do not need to manually apply them. Simply run the application using the following command:

   ```bash
   dotnet run
   ```
This will automatically create the necessary database schema if it doesn't exist and run any pending migrations.

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

This command will:
- Start the **SQL Server** container.
- Start the **API** container and ensure it can connect to the **SQL Server**.

The API will be available at [http://localhost:8080](http://localhost:8080), and you can access the Swagger UI at [http://localhost:8080/swagger](http://localhost:8080/swagger).

### **3. Running the API Locally with Dockerized SQL Server**

If you want to run the API locally (outside Docker) but use the SQL Server container for your database, you can run just the SQL Server container using:

```bash
docker-compose up db
```

This will start the SQL Server container, and you can run your API using `dotnet run` in your development environment. In this case, ensure your **appsettings.Development.json** has the correct connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=CatApiAppDb;User Id=sa;Password=m3r3d@230592!;Trusted_Connection=False;Encrypt=False"
  }
}
```

This allows your local API to connect to the Dockerized SQL Server.

### **4. Stopping and Cleaning Up Using Docker Compose**

To stop the Docker containers, press **CTRL + C** in the terminal where `docker-compose up` is running, or run the following command in a new terminal:

```bash
docker-compose down
```

This will stop and remove both the API and SQL Server containers but leave the persistent data in the `sqlserverdata` volume.

### **5. Rebuilding After Code Changes**

If you make changes to the code and want to rebuild the application, you can:

1. **Rebuild the Docker image**:

    ```bash
    docker-compose build
    ```

2. **Restart the containers**:

    ```bash
    docker-compose up
    ```

By following these steps, you can switch easily between running the full application in Docker or running only the database container while developing the API locally.


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
