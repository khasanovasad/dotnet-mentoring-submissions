CREATE TRIGGER [dbo].[InsertIntoEmployeeTrigger]
	ON [dbo].[Employee]
	AFTER INSERT
	AS INSERT INTO [dbo].[Company] (Name, AddressId)
	   VALUES (
		(SELECT CompanyName FROM [dbo].[Employee] WHERE Id=(SELECT Id FROM inserted)),
		(SELECT AddressId FROM [dbo].[Employee] WHERE Id=(SELECT Id FROM inserted)))
