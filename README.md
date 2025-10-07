# 🏗️ IKEA System MVC Project

## 📝 Overview

**IKEA System MVC Project** is a web application built using **ASP.NET Core MVC** and **Entity Framework Core**.  
It provides full management of employees, departments, roles, and user-role assignments through a clean and responsive admin interface.  
The system also integrates **ASP.NET Identity** for user authentication and authorization.

---

## ⚙️ Main Features

- 🔹 Department Management (Create, Read, Update, Delete)  
- 🔹 Employee Management with image upload  
- 🔹 Role Management (Create, Edit, Delete, View)  
- 🔹 Assign and update roles for users dynamically  
- 🔹 Secure authentication system using ASP.NET Identity  
- 🔹 Responsive UI built with Bootstrap  
- 🔹 Database integration using SQL Server & EF Core  

---

## 🧩 Technologies Used

| Component | Technology |
|------------|-------------|
| Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core |
| Authentication | ASP.NET Identity |
| Database | SQL Server |
| Frontend | Bootstrap + Razor Views |
| IDE | Visual Studio / Visual Studio Code |

---

## 🚀 Getting Started


### 1️⃣ Clone the Repository

git clone https://github.com/yasmin2624/IKEA-System-MVC-Poject-.git
cd IKEA-System-MVC-Poject-



2️⃣ Configure Database Connection
Edit your appsettings.json file and update the connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=IKEAC44;Trusted_Connection=True;MultipleActiveResultSets=true"
}



3️⃣ Apply Migrations
Run the following commands in Visual Studio Package Manager Console or terminal:

Add-Migration InitialCreate
Update-Database



4️⃣ Run the Application

dotnet run
Or press F5 in Visual Studio.

---
## 📁 Project Structure

IKEA.PL/

 - Controllers/

    DepartmentController.cs
    
    EmployeeController.cs
    
    RoleController.cs
    
    UserRoleController.cs
    
    AccountController.cs
  
 - Views/
  
    Department/
    
    Employee/
    
    Role/
    
    UserRole/
  
 - ViewModels/
  
    DepartmentViewModels/
    
    EmployeeViewModels/
    
    RoleViewModels/
    
    UserRoleViewModels/
 
 - wwwroot/
  
    css/
  
    js/
  
 - Program.cs

---

### 🔐 User ↔ Role Assignment (Identity Integration)
The system uses ASP.NET Identity with these main tables:


- AspNetUsers

- AspNetRoles

AspNetUserRoles (relationship between users and roles)

Assigning a role to a user is done using:

- await _userManager.AddToRoleAsync(user, roleName);
To display user roles in the index page:

- await _userManager.IsInRoleAsync(user, roleName);

### 💡 Future Improvements
- 🔹Add permission-level management (per-role authorization)
- 🔹Implement AJAX-based updates for smoother UX
- 🔹Add search, filters, and pagination in tables
- 🔹Generate downloadable reports (PDF / Excel)
- 🔹Enhance UI consistency and responsive behavior


### 🏁 License
This project is created for educational purposes and internal training.
You may use it for learning, modification, and practice freely.
