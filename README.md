# Publishing House Management System API

## Project Description

This project is a small publishing book house management system API. It includes three authorized accounts: Senior Operator, Operator, and Manager. The two main models are Author and Product, which have a many-to-many relationship. Managers have access only to the Product model, while the other two authorized accounts have access only to the Author model. The project supports CRUD operations for both models, allowing users to create Authors and Products and assign them to each other.

## Installation Instructions

### Prerequisites

- .NET SDK
- Visual Studio

### Setup

1. **Clone the Repository**

    ```sh
    git clone [repository_url]
    cd PublishingHouse
    ```

2. **Install Dependencies**

    The project uses ASP.NET Web API, EF Core, AutoMapper, ASP.NET Identity, and Swagger UI. All necessary dependencies will be installed automatically.

3. **Apply Database Migrations**

    ```sh
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

4. **Run the Application**

    Start the project with Visual Studio by pressing F5.

## Usage

The API provides the following functionalities:

- **CRUD Operations for Authors and Products**: Create, read, update, and delete authors and products.
- **Assign Authors to Products**: Manage the many-to-many relationship between authors and products.

## Configuration

- **appsettings.json**: Contains configuration settings such as connection strings and other application-specific settings.

## Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- AutoMapper
- ASP.NET Identity
- Swagger UI

## License

This project is licensed under the MIT License.

## Contact Information

For any inquiries or issues, please contact Nika Gogelia at gogelianika1@gmail.com.
