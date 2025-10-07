# AutoLux: Pre-Owned Luxury Car Management System

## Project Overview

**AutoLux** is a bespoke web application designed to streamline the management of a pre-owned luxury car inventory for a premium dealership.  

The system addresses the complexity and time-consuming nature of managing high-value vehicle inventory, sales, and customer interactions by providing a secure, streamlined platform with detailed specifications, brand-specific information, and powerful search capabilities.

The system is specifically designed for a **single administrative user** to manage all core activities.

* **Client:** Elite Auto Imports (Fictional)  
* **Project Duration:** 6 Weeks (Executed across 2 Agile Sprints of 3 weeks each)  
* **Team Size:** 6 Members  

---

## Technical Stack

AutoLux is a modern full-stack application leveraging the power of Angular and ASP.NET Core for a robust and responsive experience.

| Component | Technology | Details |
| :--- | :--- | :--- |
| **Front-end** | **Angular** | Rich, responsive, and modular UI built with TypeScript and Angular CLI |
| **Back-end** | **ASP.NET Core Web API** | Handles secure API endpoints and business logic with a RESTful architecture |
| **Database** | **SQL Server** | Relational database managed through Entity Framework Core |
| **ORM** | **Entity Framework Core** | Implements a **Code-First Approach** for seamless schema generation |
| **Authentication** | **JWT Token Authentication** | Provides stateless, secure API access for the single admin user |
| **User Management** | **ASP.NET Core Identity Framework** | Used for secure password hashing, login, and user management |

---

## Core Features and Functionalities

The system provides a comprehensive set of features focused on inventory and brand management, alongside secure administrator access.

### Inventory Management (Cars)
* **CRUD Operations:** Full **Create, Read (List all, Get by ID), Update, Delete** functionality for car records.
* **Advanced Search:** Filter cars by **Model, Brand, Year, Price Range, Mileage Range, Color**, and **Availability (Sold/Available)**.
* **Detailed Records:** Each record includes fields like Model, Year, Price, Mileage, Color, Description, Image URL, Date Added, and Brand Reference.

### Brand Management
* **CRUD Operations:** Full management features to **Add, View, Edit, and Delete** brand details.
* **Brand Details:** Each record stores **Brand Name (Unique)** and **Country of Origin**.
* **Relationship:** Implements a **many-to-one relationship** where multiple cars belong to a single brand.

### Administrator Access
* **Secure Authentication:** Login/Logout functionality powered by JWT and ASP.NET Core Identity.
* **Authorization:** API endpoints are protected and accessible only after successful token validation.
* **User Management:** Allows password change and basic profile management for the administrator.

---

## Database Schema (Entity Framework Core - Code-First)

The system utilizes two primary tables—**Cars** and **Brands**—demonstrating a clean, maintainable one-to-many relationship.

### 1. Cars Table (Core Inventory)
| Field | Data Type | Key/Note |
| :--- | :--- | :--- |
| **Id** | INT | Primary Key |
| **Model** | NVARCHAR(MAX) | Car Model |
| **Year** | INT | Manufacturing Year |
| **Price** | DECIMAL(18,2) | Listed Price |
| **Mileage** | INT | Total distance driven (KM) |
| **Color** | NVARCHAR(MAX) | Exterior Color |
| **Description** | NVARCHAR(MAX) | Detailed specifications and notes |
| **ImageUrl** | NVARCHAR(MAX) | Car Image URL |
| **BrandId** | INT (FK) | Foreign Key linking to `Brands` Table |
| **DateAdded** | DATETIME | Record creation date |
| **IsSold** | BIT | Indicates whether the car is sold |

### 2. Brands Table
| Field | Data Type | Key/Note |
| :--- | :--- | :--- |
| **Id** | INT | Primary Key |
| **Name** | NVARCHAR(MAX) | Unique Brand Name |
| **CountryOfOrigin** | NVARCHAR(MAX) | Country where the brand originated |

---

## Project Team

| ID | Name | Role |
| :--- | :--- | :--- |
| 2417550 | Anshul Negi | **Team Lead** |
| 2416836 | Paridhi Khandelwal | Frontend Developer |
| 2417250 | Simarpreet Kaur | Frontend Developer |
| 2416197 | Cheemala Poojitha | Developer |
| 2417251 | Rishani Sharma | Developer |
| 2418041 | Barruri Sai Suhas Sharma | Backend Developer |

---

## Setup and Installation

The project can be set up locally using Node.js, Angular CLI, .NET 8 SDK, and SQL Server.

### Prerequisites

* Node.js (for Angular)
* .NET 8 SDK (for ASP.NET Core)
* SQL Server 2022 or SQL Server LocalDB
* Visual Studio / VS Code

---

### Back-end (ASP.NET Core Web API) Setup

1. **Clone the repository:**
    ```bash
    git clone https://github.com/saisuhas12/AutoLux.git
    cd AutoluxBackend
    ```

2. **Update Database Connection:**
   Edit `appsettings.json` and replace the connection string with your local SQL Server configuration.

3. **Run Migrations:**
    ```bash
    dotnet ef database update
    ```

4. **Start the API:**
    ```bash
    dotnet run
    ```

---

### Front-end (Angular) Setup

1. **Navigate to the UI directory:**
    ```bash
    cd Autolux-UI
    ```

2. **Install dependencies:**
    ```bash
    npm install
    ```

3. **Configure API URL:**
   Update the base API URL inside `src/environments/environment.ts`.

4. **Run the Angular application:**
    ```bash
    ng serve
    ```

5. **Access the app:**
   Open your browser and go to [http://localhost:4200](http://localhost:4200)

---

## Screenshots (Optional)

*(Add screenshots for the following interfaces once the app is deployed)*

- Login Page  
- Dashboard  
- Car Management  
- Brand Management  
- Search & Filter  

---

## License

This project was developed as part of an **academic case study** under the **Cognizant Internship Program**.  
All rights reserved © 2025 AutoLux Team.

---

## Acknowledgements

Special thanks to Cognizant mentors and coordinators for their continuous guidance and support throughout the project lifecycle.

---

## Repository Link

🔗 [https://github.com/saisuhas12/AutoLux](https://github.com/saisuhas12/AutoLux)
