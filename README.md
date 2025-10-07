# AutoLux: Pre-Owned Luxury Car Management System

## Project Overview

**AutoLux** is a bespoke web application designed to streamline the management of a pre-owned luxury car inventory for a premium dealership. [cite_start]The system addresses the complexity and time-consuming nature of managing high-value vehicle inventory, sales, and customer interactions by providing a secure, streamlined platform with detailed specifications, brand-specific information, and powerful search capabilities[cite: 8, 9, 11].

[cite_start]The system is specifically designed for a **single administrative user** to manage all core activities[cite: 10, 14, 49].

* [cite_start]**Client:** Elite Auto Imports (Fictional) [cite: 3]
* [cite_start]**Project Duration:** 6 Weeks (Executed across 2 Agile Sprints of 3 weeks each) [cite: 4, 21, 66, 67]
* [cite_start]**Team Size:** 6 Members [cite: 5]

---

## Technical Stack

[cite_start]AutoLux is a modern full-stack application leveraging the power of Angular and ASP.NET Core for a robust and responsive experience[cite: 12].

| Component | Technology | Details |
| :--- | :--- | :--- |
| **Front-end** | **Angular** | [cite_start]Rich, responsive user interface [cite: 12, 16] |
| **Back-end** | **ASP.NET Core Web API** | [cite_start]Robust and secure server-side logic [cite: 12, 17] |
| **Database** | **SQL Server** | [cite_start]Managed via Entity Framework Core [cite: 18, 19] |
| **ORM** | **Entity Framework Core** | [cite_start]Utilizes a **Code-First Approach** for database interaction [cite: 13, 19] |
| **Authentication** | **JWT Token Authentication** | [cite_start]Secure access and stateless API authentication [cite: 13, 20, 51, 52] |
| **User Management** | **ASP.NET Core Identity Framework** | [cite_start]Used for user registration, password hashing, and single admin management [cite: 13, 20, 50] |

---

## Core Features and Functionalities

[cite_start]The system provides a comprehensive set of features focused on inventory and brand management, alongside secure administrator access[cite: 53].

### Inventory Management (Cars)
* [cite_start]**CRUD Operations:** Full **Create, Read (List all, Get by ID), Update, Delete** functionality for car records[cite: 55].
* [cite_start]**Advanced Search:** Ability to search cars by: **Model, Brand Name, Year, Price Range, Mileage Range, Color, and availability (Sold/Available)**[cite: 59, 130].
* [cite_start]**Detailed Records:** Each car record includes fields for Model, Year, Price, Mileage, Color, Description, Image URL, Date Added, and a link to its Brand[cite: 27, 28, 29, 31, 33, 34, 36, 37, 38, 39].

### Brand Management
* [cite_start]**CRUD Operations:** Full **Create, Read (List all, Get by ID), Update, Delete** functionality for brand records[cite: 56].
* [cite_start]**Brand Details:** Records include the Brand Name (unique) and Country of Origin[cite: 44, 45].
* [cite_start]**Relationship:** A **many-to-one relationship** exists, where multiple cars can belong to a single brand[cite: 46].

### Administrator Access
* [cite_start]**Secure Authentication:** **Login/Logout** functionality using JWT Token authentication[cite: 61, 71, 76, 80].
* [cite_start]**Authorization:** Ensures protected API endpoints are only accessible after successful token validation[cite: 77, 144].
* [cite_start]**User Management:** Basic features for a single administrator, including **Password Change**[cite: 62, 145].

---

## Database Schema (Entity Framework Core - Code-First)

[cite_start]The system utilizes two core tables with a simple, effective relationship[cite: 23].

### 1. Cars Table (Core Inventory)
| Field | Data Type | Key/Note |
| :--- | :--- | :--- |
| **Id** | INT | [cite_start]Primary Key [cite: 25] |
| **Model** | [cite_start]NVARCHAR(MAX) | [cite: 27] |
| **Year** | [cite_start]INT | [cite: 28] |
| **Price** | [cite_start]DECIMAL(18,2) | [cite: 29] |
| **Mileage** | [cite_start]INT | [cite: 31] |
| **Color** | [cite_start]NVARCHAR(MAX) | [cite: 33] |
| **Description** | [cite_start]NVARCHAR(MAX) | [cite: 34] |
| **ImageUrl** | [cite_start]NVARCHAR(MAX) | [cite: 36] |
| **BrandId** | INT | [cite_start]**Foreign Key** (Links to `Brands` Table) [cite: 37] |
| **DateAdded** | [cite_start]DATETIME | [cite: 38] |
| **IsSold** | BIT | [cite_start]Indicates availability [cite: 39] |

### 2. Brands Table
| Field | Data Type | Key/Note |
| :--- | :--- | :--- |
| **Id** | INT | [cite_start]Primary Key [cite: 42] |
| **Name** | NVARCHAR(MAX) | [cite_start]Unique [cite: 44] |
| **CountryOfOrigin** | [cite_start]NVARCHAR(MAX) | [cite: 45] |

---

## Project Team

| ID | Name | Role |
| :--- | :--- | :--- |
| 2417550 | Anshul Negi | [cite_start]**Team Lead** [cite: 6] |
| 2416836 | Paridhi Khandelwal | [cite_start]Member [cite: 6] |
| 2417250 | Simarpreet Kaur | [cite_start]Member [cite: 6] |
| 2416197 | Cheemala Poojitha | [cite_start]Member [cite: 6] |
| 2417251 | Rishani Sharma | [cite_start]Member [cite: 6] |
| 2418041 | BARRURI SAI SUHAS SHARMA | [cite_start]Member [cite: 6] |

---

## Setup and Installation

*(Insert detailed instructions here for setting up the Angular front-end, the ASP.NET Core Web API back-end, and the SQL Server database. This section should cover prerequisites like Node.js, .NET SDK, and SQL Server setup.)*

### Prerequisites

* Node.js (for Angular)
* .NET 7/8 SDK (for ASP.NET Core)
* SQL Server or SQL Server LocalDB

### Back-end (ASP.NET Core Web API) Setup

1.  **Clone the repository:**
    ```bash
    git clone [Your-Repo-URL]
    cd AutoLux-Backend
    ```
2.  **Update Database Connection:** Open `appsettings.json` and configure your SQL Server connection string.
3.  **Run Migrations:** Apply the Entity Framework Core migrations to create the database schema.
    ```bash
    dotnet ef database update
    ```
4.  **Run the API:**
    ```bash
    dotnet run
    ```

### Front-end (Angular) Setup

1.  **Navigate to the client directory:**
    ```bash
    cd ../AutoLux-Frontend
    ```
2.  **Install dependencies:**
    ```bash
    npm install
    ```
3.  **Configure API URL:** Update the API base URL in the Angular environment file (`environment.ts` or `environment.prod.ts`) to match your running back-end.
4.  **Run the application:**
    ```bash
    ng serve -o
    ```

---

## License

*(Insert your project license information here, e.g., MIT, Apache 2.0)*
