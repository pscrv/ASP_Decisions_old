	CREATE TABLE [dbo].[LOOKUPRole](  
        [LOOKUPRoleID] [int] IDENTITY(1,1) NOT NULL,  
        [RoleName] [varchar](100) DEFAULT '',  
        [RoleDescription] [varchar](500) DEFAULT '',  
        [RowCreatedSYSUserID] [int] NOT NULL,  
        [RowCreatedDateTime] [datetime]  DEFAULT GETDATE(),  
        [RowModifiedSYSUserID] [int] NOT NULL,  
        [RowModifiedDateTime] [datetime] DEFAULT GETDATE(),  
    PRIMARY KEY (LOOKUPRoleID)  
        )


