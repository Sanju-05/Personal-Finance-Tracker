# 💰 Personal Finance Tracker

**PersonalFinanceTracker** is a web-based application developed as part of a job assignment to help users manage their income and expenses. The app allows users to:

- Register and log in
- Log income and expense transactions
- View transactions in a table format
- See summaries visually using a pie chart

---

## 📦 Repository

GitHub Repo: [https://github.com/Sanju-05/Personal-Finance-Tracker](https://github.com/Sanju-05/Personal-Finance-Tracker)

---

## 🚀 Getting Started

Follow the steps below to set up and run the project locally.

---

### 1. Clone the Repository

```bash
git clone https://github.com/Sanju-05/Personal-Finance-Tracker.git
cd Personal-Finance-Tracker
```

### 2. Create a Local Database
Open your SQL Server or local DBMS (e.g., SSMS, Azure Data Studio).

Create a new database with the following name:
```bash
finance_tracker_db
```

### 3. Update Database Connection Details
Open the appsettings.json file.

Locate the "ConnectionStrings" section and update it with your local SQL Server configuration.

```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=finance_tracker_db;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
Replace YOUR_SERVER_NAME with your actual SQL Server instance name.

### 4. Run the Project
Open the solution in Visual Studio.

Build the project.

Run the project.

✅ The necessary tables and schema will be created automatically on the first run using Entity Framework migrations.

### 5. Use the Application
Register a new user.

Log in with your credentials.

Add income or expense transactions.

View transactions as a list or grouped in tables.

See a visual summary using a pie chart.

## 📌 Summary
This project implements functionality to log income and expenses and provides a comprehensive summary view through visual charts. It's a great starting point for building personal or enterprise-level finance tracking systems.
