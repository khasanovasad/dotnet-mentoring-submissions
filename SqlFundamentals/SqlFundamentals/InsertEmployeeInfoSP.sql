CREATE PROCEDURE [dbo].[InsertEmployeeInfoSP]
	@employeeName nvarchar(100) = NULL,
	@firstName nvarchar(50) = NULL,
	@lastName nvarchar(50) = NULL,
	@companyName nvarchar(20),
	@position nvarchar(30) = NULL,
	@street nvarchar(50),
	@city nvarchar(20) = NULL,
	@state nvarchar(50) = NULL,
	@zipCode nvarchar(50) = NULL
AS

-- checking the requirements
IF (@firstName IS NULL OR @firstName='' OR @firstName=' ') AND 
	(@lastName IS NULL OR @lastName='' OR @lastName=' ') AND 
	(@employeeName IS NULL OR @employeeName='' OR @employeeName=' ')
	BEGIN
		RETURN 0
	END

-- inserting into [dbo].[Address]
	INSERT INTO [dbo].[Address] (Street, City, State, ZipCode)
	VALUES (@street, @city, @state, @zipCode);

-- inserting into [dbo].[Person]
IF (@firstName IS NULL OR @firstName='' OR @firstName=' ') AND 
	(@lastName IS NULL OR @lastName='' OR @lastName=' ')
	BEGIN
		INSERT INTO [dbo].[Person] (FirstName, LastName)
		VALUES (@employeeName, @employeeName);
	END

IF (@firstName IS NULL OR @firstName='' OR @firstName=' ') AND 
	(@lastName IS NOT NULL OR @lastName!='' OR @lastName!=' ')
	BEGIN
		INSERT INTO [dbo].[Person] (FirstName, LastName)
		VALUES (@employeeName, @lastName);
	END

IF (@firstName IS NOT NULL OR @firstName!='' OR @firstName!=' ') AND 
	(@lastName IS NULL OR @lastName='' OR @lastName=' ')
	BEGIN
		INSERT INTO [dbo].[Person] (FirstName, LastName)
		VALUES (@firstName, @employeeName);
	END

-- inserting into [dbo].[Company]
	INSERT INTO [dbo].[Company] (Name, AddressId)
	VALUES (@companyName, (SELECT Id FROM [dbo].[Address] WHERE Street=@street));

-- inserting into [dbo].[Company]
	INSERT INTO [dbo].[Employee] (AddressId, PersonId, CompanyName, Position, EmployeeName)
	VALUES (
		(SELECT Id FROM [dbo].[Address] WHERE Street=@street),
		(SELECT Id FROM [dbo].[Person] WHERE FirstName=@firstName AND LastName=@lastName),
		@companyName, @position, @employeeName)
RETURN 0
