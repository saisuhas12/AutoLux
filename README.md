# AutoLux: Pre-Owned Luxury Car Management System

## Project Overview

**AutoLux** is a bespoke web application designed to streamline the management of a pre-owned luxury car inventory for a premium dealership.  
[cite_start]The system addresses the complexity and time-consuming nature of managing high-value vehicle inventory, sales, and customer interactions by providing a secure, streamlined platform with detailed specifications, brand-specific information, and powerful search capabilities[cite: 5].

[cite_start]The system is specifically designed for a **single administrative user** to manage all core activities[cite: 5].

* [cite_start]**Client:** Elite Auto Imports (Fictional)[cite: 5]  
* [cite_start]**Project Duration:** 6 Weeks (Executed across 2 Agile Sprints of 3 weeks each)[cite: 5]  
* [cite_start]**Team Size:** 6 Members[cite: 5]  

---

## Technical Stack

[cite_start]AutoLux is a modern full-stack application leveraging the power of Angular and ASP.NET Core for a robust and responsive experience[cite: 5].

| Component | Technology | Details |
| :--- | :--- | :--- |
| **Front-end** | **Angular** | [cite_start]Rich, responsive, and modular UI built with TypeScript and Angular CLI[cite: 5] |
| **Back-end** | **ASP.NET Core Web API** | [cite_start]Handles secure API endpoints and business logic with a RESTful architecture[cite: 5] |
| **Database** | **SQL Server** | [cite_start]Relational database managed through Entity Framework Core[cite: 5] |
| **ORM** | **Entity Framework Core** | [cite_start]Implements a **Code-First Approach** for seamless schema generation[cite: 5] |
| **Authentication** | **JWT Token Authentication** | [cite_start]Provides stateless, secure API access for the single admin user[cite: 5] |
| **User Management** | **ASP.NET Core Identity Framework** | [cite_start]Used for secure password hashing, login, and user management[cite: 5] |

---

## Core Features and Functionalities

[cite_start]The system provides a comprehensive set of features focused on inventory and brand management, alongside secure administrator access[cite: 5].

### Inventory Management (Cars)
* [cite_start]**CRUD Operations:** Full **Create, Read (List all, Get by ID), Update, Delete** functionality for car records[cite: 5].
* [cite_start]**Advanced Search:** Filter cars by **Model, Brand, Year, Price Range, Mileage Range, Color**, and **Availability (Sold/Available)**[cite: 5].
* [cite_start]**Detailed Records:** Each record includes fields like Model, Year, Price, Mileage, Color, Description, Image URL, Date Added, and Brand Reference[cite: 5].

### Brand Management
* [cite_start]**CRUD Operations:** Full management features to **Add, View, Edit, and Delete** brand details[cite: 5].
* [cite_start]**Brand Details:** Each record stores **Brand Name (Unique)** and **Country of Origin**[cite: 5].
* [cite_start]**Relationship:** Implements a **many-to-one relationship** where multiple cars belong to a single brand[cite: 5].

### Administrator Access
* [cite_start]**Secure Authentication:** Login/Logout functionality powered by JWT and ASP.NET Core Identity[cite: 5].
* [cite_start]**Authorization:** API endpoints are protected and accessible only after successful token validation[cite: 5].
* [cite_start]**User Management:** Allows password change and basic profile management for the administrator[cite: 5].

---

## Database Schema (Entity Framework Core - Code-First)

[cite_start]The system utilizes two primary tables—**Cars** and **Brands**—demonstrating a clean, maintainable one-to-many relationship[cite: 5].

### 1. Cars Table (Core Inventory)
| Field | Data Type | Key/Note |
| :--- | :--- | :--- |
| **Id** | INT | [cite_start]Primary Key[cite: 5] |
| **Model** | NVARCHAR(MAX) | [cite_start]Car Model[cite: 5] |
| **Year** | INT | [cite_start]Manufacturing Year[cite: 5] |
| **Price** | DECIMAL(18,2) | [cite_start]Listed Price[cite: 5] |
| **Mileage** | INT | [cite_start]Total distance driven (KM)[cite: 5] |
| **Color** | NVARCHAR(MAX) | [cite_start]Exterior Color[cite: 5] |
| **Description** | NVARCHAR(MAX) | [cite_start]Detailed specifications and notes[cite: 5] |
| **ImageUrl** | NVARCHAR(MAX) | [cite_start]Car Image URL[cite: 5] |
| **BrandId** | INT (FK) | [cite_start]Foreign Key linking to `Brands` Table[cite: 5] |
| **DateAdded** | DATETIME | [cite_start]Record creation date[cite: 5] |
| **IsSold** | BIT | [cite_start]Indicates whether the car is sold[cite: 5] |

### 2. Brands Table
| Field | Data Type | Key/Note |
| :--- | :--- | :--- |
| **Id** | INT | [cite_start]Primary Key[cite: 5] |
| **Name** | NVARCHAR(MAX) | [cite_start]Unique Brand Name[cite: 5] |
| **CountryOfOrigin** | NVARCHAR(MAX) | [cite_start]Country where the brand originated[cite: 5] |

---

## Project Team

| ID | Name | Role |
| :--- | :--- | :--- |
| 2417550 | Anshul Negi | [cite_start]**Team Lead**[cite: 5] |
| 2416836 | Paridhi Khandelwal | [cite_start]Frontend Developer[cite: 5] |
| 2417250 | Simarpreet Kaur | [cite_start]Frontend Developer[cite: 5] |
| 2416197 | Cheemala Poojitha | [cite_start]Developer[cite: 5] |
| 2417251 | Rishani Sharma | [cite_start]Developer[cite: 5] |
| 2418041 | Barruri Sai Suhas Sharma | [cite_start]Backend Developer[cite: 5] |

---

## Setup and Installation

[cite_start]The project can be set up locally using Node.js, Angular CLI, .NET 8 SDK, and SQL Server[cite: 5].

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

## Screenshots (Optional Section)
*(Add screenshots for the following interfaces once the app is deployed)*

- Login Page  
- Dashboard  
- Car Management  
- Brand Management  
- Search & Filter  

---

## License

[cite_start]This project was developed as part of an **academic case study** under the **Cognizant Internship Program**.  
All rights reserved © 2025 AutoLux Team[cite: 5].

---

## Acknowledgements

[cite_start]Special thanks to Cognizant mentors and coordinators for their continuous guidance and support throughout the project lifecycle[cite: 5].

---

## Repository Link

🔗 [https://github.com/saisuhas12/AutoLux](https://github.com/saisuhas12/AutoLux)
