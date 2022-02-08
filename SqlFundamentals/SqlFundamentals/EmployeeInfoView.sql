CREATE VIEW EmployeeInfoView 
AS
SELECT dbo.Employee.Id AS EmployeeId, (CASE WHEN dbo.Employee.EmployeeName IS NULL OR
                  dbo.Employee.EmployeeName = '' THEN CONCAT(dbo.Person.FirstName, CONCAT(' ', dbo.Person.LastName)) ELSE dbo.Employee.EmployeeName END) AS EmployeeFullName, 
                  { fn CONCAT({ fn CONCAT({ fn CONCAT(dbo.Address.ZipCode, '_') }, { fn CONCAT(dbo.Address.State, ',') }) }, { fn CONCAT({ fn CONCAT(dbo.Address.City, '-') }, dbo.Address.Street) }) } AS EmployeeFullAddress, 
                  { fn CONCAT({ fn CONCAT(dbo.Company.Name, '(') }, { fn CONCAT(dbo.Employee.Position, ')') }) } AS EmployeeCompanyInfo
FROM     dbo.Address INNER JOIN
                  dbo.Company ON dbo.Address.Id = dbo.Company.AddressId INNER JOIN
                  dbo.Employee ON dbo.Address.Id = dbo.Employee.AddressId INNER JOIN
                  dbo.Person ON dbo.Employee.PersonId = dbo.Person.Id