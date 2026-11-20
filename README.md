# Umuganda Management System

A C# Windows Forms desktop application for managing Umuganda community work activities in Rwanda. The system supports coordinator authentication, village management, activity planning and tracking, attendance reporting, and a cell-level statistics dashboard.

---

## Features

- **Coordinator Registration & Login** — Secure account creation and login with SHA-256 password hashing.
- **Village Management** — Add, update, and delete villages within a coordinator's assigned cell.
- **Activity Management** — Plan, update, and delete Umuganda activities. Filter by village, activity type, date, or status.
- **Activity Status Tracking** — Enforce status transitions: `PLANNED → ONGOING → ACCOMPLISHED` or `→ POSTPONED → PLANNED`.
- **Activity Reports** — Submit attendance and work description reports for accomplished activities.
- **Dashboard** — Live summary of villages, activities by status, upcoming planned activities, and submitted reports.
- **Profile Management** — Update personal information and change password.

---

## Project Structure

```
UmugandaManagementSystem/
│
├── Forms/
│   ├── LoginForm.cs               # Login screen
│   ├── RegisterForm.cs            # New coordinator registration
│   ├── DashboardForm.cs           # Main dashboard with statistics
│   ├── VillageManagementForm.cs   # CRUD for villages
│   ├── ActivityManagementForm.cs  # Plan, update, filter, delete activities
│   ├── ActivityReportForm.cs      # Submit activity reports
│   └── ProfileForm.cs             # View and edit coordinator profile
│
├── Models/
│   ├── Coordinator.cs
│   ├── Village.cs
│   ├── Cell.cs
│   ├── Activity.cs
│   ├── ActivityType.cs
│   ├── ActivityStatus.cs          # Enum: PLANNED, ONGOING, ACCOMPLISHED, POSTPONED
│   ├── ActivityReport.cs
│   └── DashboardSummary.cs
│
├── Repositories/
│   ├── AuthRepository.cs          # Register, login, update profile, change password
│   ├── CellRepository.cs          # Fetch all cells
│   ├── VillageRepository.cs       # CRUD for villages
│   ├── ActivityTypeRepository.cs  # Fetch all activity types
│   ├── ActivityRepository.cs      # CRUD + filtering for activities
│   ├── ActivityReportRepository.cs# Submit and view activity reports
│   └── DashboardRepository.cs     # Aggregate statistics per cell
│
├── DatabaseConnection.cs          # MySQL connection helper
├── Program.cs                     # Application entry point
└── App.config                     # Application configuration
```

---

## Technology Stack

| Component      | Details                        |
|----------------|-------------------------------|
| Language       | C# (.NET Framework 4.7.2)     |
| UI Framework   | Windows Forms (WinForms)      |
| Database       | MySQL                         |
| MySQL Driver   | MySql.Data (NuGet)            |
| Cryptography   | SHA-256 (System.Security.Cryptography) |
| IDE            | Visual Studio                 |

---

## Database Setup

The application connects to a local MySQL database. Create the database and tables using the schema below.

### Connection String

Located in `DatabaseConnection.cs`:

```
Server=127.0.0.1;Port=3306;Database=umuganda_management_system;Uid=root;Pwd=;
```

Update the `Pwd` field to match your MySQL root password.

---

### SQL Schema

```sql
CREATE DATABASE IF NOT EXISTS umuganda_management_system;
USE umuganda_management_system;

-- Cells (administrative units)
CREATE TABLE cells (
    cell_id     INT AUTO_INCREMENT PRIMARY KEY,
    cell_number INT NOT NULL,
    cell_name   VARCHAR(100) NOT NULL
);

-- Coordinators
CREATE TABLE coordinators (
    coordinator_id INT AUTO_INCREMENT PRIMARY KEY,
    first_name     VARCHAR(100) NOT NULL,
    last_name      VARCHAR(100) NOT NULL,
    phone          VARCHAR(10)  NOT NULL,
    cell_id        INT NOT NULL,
    FOREIGN KEY (cell_id) REFERENCES cells(cell_id)
);

-- User accounts
CREATE TABLE users (
    user_id        INT AUTO_INCREMENT PRIMARY KEY,
    coordinator_id INT NOT NULL UNIQUE,
    username       VARCHAR(100) NOT NULL UNIQUE,
    password_hash  VARCHAR(255) NOT NULL,
    FOREIGN KEY (coordinator_id) REFERENCES coordinators(coordinator_id)
);

-- Villages
CREATE TABLE villages (
    village_id   INT AUTO_INCREMENT PRIMARY KEY,
    village_name VARCHAR(100) NOT NULL,
    village_code VARCHAR(50)  NOT NULL,
    leader_name  VARCHAR(100) NOT NULL,
    cell_id      INT NOT NULL,
    FOREIGN KEY (cell_id) REFERENCES cells(cell_id)
);

-- Activity types
CREATE TABLE activity_types (
    activity_type_id INT AUTO_INCREMENT PRIMARY KEY,
    type_name        VARCHAR(100) NOT NULL
);

-- Activities
CREATE TABLE activities (
    activity_id      INT AUTO_INCREMENT PRIMARY KEY,
    activity_date    DATE         NOT NULL,
    objective        TEXT         NOT NULL,
    status           ENUM('PLANNED','ONGOING','ACCOMPLISHED','POSTPONED') NOT NULL DEFAULT 'PLANNED',
    village_id       INT NOT NULL,
    activity_type_id INT NOT NULL,
    coordinator_id   INT NOT NULL,
    FOREIGN KEY (village_id)       REFERENCES villages(village_id),
    FOREIGN KEY (activity_type_id) REFERENCES activity_types(activity_type_id),
    FOREIGN KEY (coordinator_id)   REFERENCES coordinators(coordinator_id)
);

-- Activity reports
CREATE TABLE activity_reports (
    report_id                    INT AUTO_INCREMENT PRIMARY KEY,
    activity_id                  INT  NOT NULL UNIQUE,
    submitted_by_coordinator_id  INT  NOT NULL,
    submission_date              DATETIME NOT NULL,
    attendee_count               INT  NOT NULL,
    work_description             TEXT NOT NULL,
    FOREIGN KEY (activity_id)                 REFERENCES activities(activity_id),
    FOREIGN KEY (submitted_by_coordinator_id) REFERENCES coordinators(coordinator_id)
);

-- Seed: example cells
INSERT INTO cells (cell_number, cell_name) VALUES
(1, 'Kacyiru Cell'),
(2, 'Kimihurura Cell'),
(3, 'Remera Cell');

-- Seed: example activity types
INSERT INTO activity_types (type_name) VALUES
('Road Clearing'),
('Tree Planting'),
('Drainage Cleaning'),
('Garbage Collection'),
('School Maintenance');
```

---

## Getting Started

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.7.2
- MySQL Server 5.7 or later
- NuGet packages (restored automatically on build):
  - `MySql.Data`
  - `BouncyCastle.Cryptography`

### Steps

1. **Clone the repository**
   ```
   git clone https://github.com/<your-username>/UmugandaManagementSystem.git
   ```

2. **Set up the database**  
   Run the SQL schema above in MySQL Workbench or the MySQL CLI.

3. **Configure the connection string**  
   Open `DatabaseConnection.cs` and set your MySQL password in the connection string.

4. **Open the solution**  
   Open `UmugandaManagementSystem.slnx` in Visual Studio.

5. **Restore NuGet packages**  
   Visual Studio restores packages automatically on build, or go to **Tools → NuGet Package Manager → Restore**.

6. **Run the application**  
   Press `F5`. The registration form opens first; create an account to get started.

---

## Security Notes

- Passwords are hashed with **SHA-256** before storage. They are never saved or transmitted in plain text.
- All database queries use **parameterized commands** to prevent SQL injection.
- Each coordinator can only manage villages and activities within their own assigned cell.

---

## Activity Status Flow

```
PLANNED ──► ONGOING ──► ACCOMPLISHED
   ▲             │
   │             ▼
   └──── POSTPONED ◄────┘
```

- Only `PLANNED` activities can be edited or deleted.
- A `POSTPONED` activity must be given a future date when rescheduled back to `PLANNED`.
- An `ACCOMPLISHED` activity cannot change status.

---

## License

This project is for educational and community use.
