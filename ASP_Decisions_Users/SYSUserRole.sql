CREATE TABLE [dbo].[SYSUserRole](  
    [SYSUserRoleID] [int] IDENTITY(1,1) NOT NULL,  
    [SYSUserID] [int] NOT NULL,  
    [LOOKUPRoleID] [int] NOT NULL,  
    [IsActive] [bit] DEFAULT (1),  
    [RowCreatedSYSUserID] [int] NOT NULL,  
    [RowCreatedDateTime] [datetime] DEFAULT GETDATE(),  
    [RowModifiedSYSUserID] [int] NOT NULL,  
    [RowModifiedDateTime] [datetime] DEFAULT GETDATE(),  
    PRIMARY KEY (SYSUserRoleID)  
)  
GO  
  
ALTER TABLE [dbo].[SYSUserRole]  WITH CHECK ADD FOREIGN KEY([LOOKUPRoleID])  
REFERENCES [dbo].[LOOKUPRole] ([LOOKUPRoleID])  
GO  
  
ALTER TABLE [dbo].[SYSUserRole]  WITH CHECK ADD FOREIGN KEY([SYSUserID])  
REFERENCES [dbo].[SYSUser] ([SYSUserID])  
GO  