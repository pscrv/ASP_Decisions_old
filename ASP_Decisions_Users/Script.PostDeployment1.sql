/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


MERGE INTO [LOOKUPRole] AS Target 
USING (VALUES 
        ('Admin','Can Edit, Update, Delete',1,1), 
        ('Member','Read only',1,1)
) 
AS Source (RoleName,RoleDescription,RowCreatedSYSUserID,RowModifiedSYSUserID) 
ON Target.RoleName = Source.RoleName 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (RoleDescription,RowCreatedSYSUserID,RowModifiedSYSUserID) 
VALUES (RoleDescription,RowCreatedSYSUserID,RowModifiedSYSUserID);

