# 🏥 Radiology Hospital Information System (HIS) Web Application
This is a full-stack web application simulating a Hospital Information System (HIS) tailored for the Radiology Department, developed as part of a semester project. The application enables patients, doctors, and administrators to interact with radiology services digitally—streamlining appointment management, profile access, scan uploads, and administrative oversight.

## 🚀 Features
👥 User Roles
Patients: Can register, log in, manage profiles, book appointments, view scan results.

Doctors: Can log in, view appointments, upload scan results, and manage patient interactions.

Admins: Have full oversight through a dedicated dashboard with access to statistics, contact forms, and administrative controls.

📄 Core Functionalities
✅ User authentication and role-based access (login/signup)

✅ Profile management with image uploads

✅ Appointment scheduling between doctors and patients

✅ Scan file uploads and retrieval (e.g., X-rays, MRIs)

✅ Admin dashboard with statistics

✅ Contact form for inquiries

✅ Responsive front-end for mobile and desktop access

✅ PostgreSQL-based backend database schema tailored for Radiology

| Layer        | Technology                    |
| ------------ | ----------------------------- |
| Backend      | ASP.NET Web API (.NET 7+)     |
| Database     | PostgreSQL (via Neon/pgAdmin) |
| Frontend     | HTML, CSS, JavaScript         |
| UI Framework | Bootstrap                     |
| Hosting      | (Vercel)  |
| Versioning   | Git + GitHub                  |

## Project Structure

RadiologyHIS/
├── backend/                 # ASP.NET Web API (Controllers, Services, Models)
├── frontend/
│   ├── Register/            # Login and Signup UI
│   └── Patient/             # Patient dashboard UI
├── wwwroot/                 # Static file serving (profile pics, scans)
└── README.md

## 🛠️ Setup Instructions
### 🧩 Prerequisites
.NET SDK

PostgreSQL or Neon

pgAdmin

Any code editor (e.g., VS Code, Rider)

## 🔧 Backend Setup
### 1- Clone the repository
  git clone https://github.com/ZEY0D/radiology-website.git
  cd RadiologyHIS/backend

### 2- Configure PostgreSQL connection in appsettings.json
  "ConnectionStrings": {
    "DefaultConnection": "Host=...;Port=5432;Database=...;Username=...;Password=..."
  }

### 3- Run the Api
  dotnet run

## 🌐 Frontend Setup
  1-Open frontend/Register/index.html or frontend/Patient/dashboard.html in your browser.

  2-Ensure the backend API is running on http://localhost:5204 or update endpoints accordingly.


