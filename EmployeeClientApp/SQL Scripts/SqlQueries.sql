create table AuditEmployees
(
  AuditEmpID int Identity(1,1) primary key,
  Id int,
  EmployeeCode varchar(100),
  EmployeeName varchar(100),
  EmployeeEmail varchar(20),
  IsActive bit,
  UpdatedBy nvarchar(128),
  UpdatedOn datetime
)

CREATE TRIGGER AuditRecord on Employees
AFTER UPDATE, INSERT,DELETE
AS
BEGIN
  INSERT INTO AuditEmployees 
  (Id, EmployeeCode, EmployeeName,EmployeeEmail,IsActive, UpdatedBy, UpdatedOn )
  SELECT i.Id, i.EmployeeCode, i.EmployeeName,i.EmployeeEmail,i.IsActive, SUSER_SNAME(), GETDATE()
  FROM  Employees t 
  INNER JOIN inserted i on t.id=i.Id 
END

