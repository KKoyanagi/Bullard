USE master;
GO

IF EXISTS(SELECT * from sys.databases WHERE name = 'BullardDB')
BEGIN
	DROP DATABASE BullardDB;
END

CREATE DATABASE BullardDB;
GO

USE BullardDB;
GO

CREATE TABLE dbo.Employees
(
	Emp_Id		INT 	NOT NULL	PRIMARY KEY, 
	AccountName	[NVARCHAR](50)		NOT NULL,
	FirstName	[NVARCHAR](50)		NOT NULL, 
	LastName	[NVARCHAR](50)		NOT NULL, 
	Email		[NVARCHAR](50)		NOT NULL, 
	Phone		[VARCHAR](50)		NOT NULL
);
GO

INSERT INTO dbo.Employees VALUES (1,'donaldduck','Donald','Duck','duck@disney.com','(916)123-1234');
GO

INSERT INTO dbo.Employees VALUES (2,'mickeymouse','Mickey','Mouse','mouse@disney.com','(916)123-1234');
GO

INSERT INTO dbo.Employees VALUES (3,'frankreynolds','Frank','Reynolds','warthog@alwayssunny.com','(916)123-1234');
GO

INSERT INTO dbo.Employees VALUES (4,'bobburger','Bob','Burger','bob@burger.com','(916)123-1234');
GO


CREATE TABLE dbo.WorkWeeks
(
	Week_Id		INT		NOT NULL 	IDENTITY (1,1) PRIMARY KEY,
	StartDate	DATE	NOT NULL,
	EndDate		DATE	NOT NULL
);
GO

INSERT INTO dbo.WorkWeeks VALUES (convert(datetime,'02/05/2017'),convert(datetime,'02/11/2017'));
GO

INSERT INTO dbo.WorkWeeks VALUES (convert(datetime,'02/12/2017'),convert(datetime,'02/18/2017'));
GO

INSERT INTO dbo.WorkWeeks VALUES (convert(datetime,'02/19/2017'),convert(datetime,'02/25/2017'));
GO

CREATE TABLE dbo.Projects
(
	Project_Id	INT		NOT NULL	IDENTITY (1,1) PRIMARY KEY,
	Project_Num	INT		NOT NULL	UNIQUE,
	Location	[VARCHAR](50),
	Address 	[VARCHAR](50)
);
GO

INSERT INTO dbo.Projects VALUES (123,'Sacramento State','123 Fake St. Sacramento CA, 95864');
GO

INSERT INTO dbo.Projects VALUES (234, 'Paddys Pub', '123 Fake St. Sacramento CA, 95864');
GO

INSERT INTO dbo.Projects VALUES (5001, 'Golden 1 Center', '123 Fake St. Sacramento CA, 95864');
GO

CREATE TABLE dbo.WorkDays
(
	WorkDay_Id	INT		NOT NULL	PRIMARY KEY,
	Day_Name	[VARCHAR](10)		NOT NULL,
);
GO

INSERT INTO dbo.WorkDays VALUES (1, 'Sunday');
GO

INSERT INTO dbo.WorkDays VALUES (2, 'Monday');
GO

INSERT INTO dbo.WorkDays VALUES (3, 'Tuesday');
GO

INSERT INTO dbo.WorkDays VALUES (4, 'Wednesday');
GO

INSERT INTO dbo.WorkDays VALUES (5, 'Thursday');
GO

INSERT INTO dbo.WorkDays VALUES (6, 'Friday');
GO

INSERT INTO dbo.WorkDays VALUES (7, 'Saturday');
GO

CREATE TABLE dbo.Timesheets
(
	Timesheet_Id	INT		NOT NULL	IDENTITY(1,1) PRIMARY KEY,
	Week_Id		INT		FOREIGN KEY REFERENCES dbo.WorkWeeks(Week_Id),
	Emp_Id		INT		FOREIGN KEY REFERENCES dbo.Employees(Emp_Id),
	Approved	BIT		NOT NULL	DEFAULT 0,
	Submitted	BIT 	NOT NULL	DEFAULT 0,
	DateSubmitted	DATE	DEFAULT 	convert(datetime,'01/01/2000')
);
GO

INSERT INTO dbo.Timesheets VALUES (1,1,DEFAULT,DEFAULT,DEFAULT);
GO

INSERT INTO dbo.Timesheets VALUES (1,2,DEFAULT,DEFAULT,DEFAULT);
GO

INSERT INTO dbo.Timesheets VALUES (1,3,DEFAULT,DEFAULT,DEFAULT);
GO

INSERT INTO dbo.Timesheets VALUES (1,4,DEFAULT,DEFAULT,DEFAULT);
GO

INSERT INTO dbo.Timesheets VALUES (2,1,DEFAULT,DEFAULT,DEFAULT);
GO

INSERT INTO dbo.Timesheets VALUES (2,2,DEFAULT,DEFAULT,DEFAULT);
GO

CREATE TABLE dbo.EmployeeDays
(
	EmployeeDay_Id	INT 	NOT NULL	IDENTITY (1,1) PRIMARY KEY,
	Timesheet_Id	INT FOREIGN KEY REFERENCES dbo.Timesheets(Timesheet_Id),
	Day_Id	INT		FOREIGN KEY REFERENCES dbo.WorkDays(WorkDay_Id),
	CONSTRAINT fk_EmployeeDayId	UNIQUE (Timesheet_Id, Day_Id)
);
GO

INSERT INTO dbo.EmployeeDays VALUES (1,1);
GO

INSERT INTO dbo.EmployeeDays VALUES (1,2);
GO

INSERT INTO dbo.EmployeeDays VALUES (1,3);
GO

INSERT INTO dbo.EmployeeDays VALUES (2,1);
GO

INSERT INTO dbo.EmployeeDays VALUES (2,2);
GO

INSERT INTO dbo.EmployeeDays VALUES (2,3);
GO

INSERT INTO dbo.EmployeeDays VALUES (3,1);
GO

INSERT INTO dbo.EmployeeDays VALUES (3,4);
GO

INSERT INTO dbo.EmployeeDays VALUES (3,5);
GO

CREATE TABLE dbo.ActivityCodes
(
	ActivityCode_Id		INT		NOT NULL PRIMARY KEY,
	ActivityDescription	[NVARCHAR](50)	NOT NULL
)

INSERT INTO dbo.ActivityCodes VALUES (03050, 'Basic Concrete Materials')
GO

INSERT INTO dbo.ActivityCodes VALUES (03100, 'Concrete Forms & Accessories')
GO

	
	
CREATE TABLE dbo.Jobs
(
	Job_Id		INT		NOT NULL IDENTITY (1,1) PRIMARY KEY,
	EmployeeDay_Id		INT FOREIGN KEY REFERENCES dbo.EmployeeDays(EmployeeDay_Id),
	Project_Id	INT		FOREIGN KEY REFERENCES dbo.Projects(Project_Id),
	ActivityCode	INT		NOT NULL,
	Hours	FLOAT		NOT NULL	CHECK(HOURS BETWEEN 0 AND 24),
	Mileage	INT		NOT NULL	DEFAULT 0,
	Lunch	FLOAT		NOT NULL	DEFAULT 0,
	--CONSTRAINT fk_JobId	UNIQUE (EmployeeDay_Id, Project_Id)
);

INSERT INTO dbo.Jobs VALUES (1,1, 03050, 4.0, DEFAULT, DEFAULT);
GO

INSERT INTO dbo.Jobs VALUES (1,2, 03050, 8.0, DEFAULT, DEFAULT);
GO

INSERT INTO dbo.Jobs VALUES (2,1,03050,8.0,DEFAULT, DEFAULT);
GO

INSERT INTO dbo.Jobs VALUES (3,1, 03050, 4.0, DEFAULT, DEFAULT);
GO

INSERT INTO dbo.Jobs VALUES (4,2, 03050, 8.0, DEFAULT, DEFAULT);
GO

INSERT INTO dbo.Jobs VALUES (5,1,03050,8.0,DEFAULT, DEFAULT);
GO
