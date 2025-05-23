# 📦 PackageTracking

## 🚀 Overview

PackageTracking is a **study project** designed to explore and implement modern software development best practices while managing package tracking efficiently! **Powered by .NET 9**, this project serves as a hands-on learning experience, integrating concepts like **CQRS, Entity Framework Core, secure authentication, and cloud integration**.

It is an evolving system, with plans to develop **new entities, controllers, roles, and policies**, alongside implementing **automated testing, Azure deployment, blob storage, and CI/CD pipelines**.

## 🏗 Patterns and Concepts

- **🛠 CQRS (Command Query Responsibility Segregation)** – Enhances performance by separating read and write operations.  
- **🗄 Entity Framework Core** – Simplifies database interactions using .NET objects.  
- **📊 Pagination** – Efficiently handles large datasets.  
- **🧩 Dependency Injection (DI)** – Improves modularity and testability.  
- **🔒 Secure Authorization & Identity** – Manages users, roles, and access control seamlessly.  

## 📂 Project Structure

- **📝 Domain** – Defines core entities, constants, and interfaces.  
- **💾 Infrastructure** – Houses repositories, authorization, database context, and policy management.  
- **⚙️ Application** – Handles business logic with commands, queries, and role-based policies.  
- **🌐 API** – Exposes endpoints, controllers, and authentication mechanisms.  

## 🎯 Getting Started

1. **Clone the repository**:  
   ```sh  
   git clone https://github.com/santosvda/PackageTracking.API.git  
   cd PackageTracking.API 
   ```  

2. **Configure the database**:  
   - Update the connection string in `appsettings.json`.  
   - Apply database migrations:  
     ```sh  
     dotnet ef database update  
     ```  

3. **Run the application**:  
   ```sh  
   dotnet run --project PackageTracking.API  
   ```  

4. **Access the API**:  
   - Available at `https://localhost:5001`  

## 🛣 Roadmap & Upcoming Features

- ✅ **Automated Testing** – Unit & integration tests to ensure reliability.  
- ☁️ **Azure Deployment** – Cloud-ready for seamless hosting.  
- 📂 **Blob Storage** – Secure file handling with Azure Blob Storage.  
- 🔄 **CI/CD Pipelines** – Automated build, test, and deployment workflows.  
- 🏗 **New Entities & Controllers** – Expanding business logic with new domain models.  
- 🔑 **Roles & Policies** – Implementing advanced authorization mechanisms.  

## 🤝 Contributing

This is a learning project, and contributions are welcome! 🚀  

### How to contribute:  
1. **Open an issue** – Discuss bugs or feature ideas.  
2. **Fork the repo & create a feature branch** – Keep changes focused.  
3. **Submit a pull request** – Provide clear descriptions of your changes.  

#
🚀 Happy coding!
