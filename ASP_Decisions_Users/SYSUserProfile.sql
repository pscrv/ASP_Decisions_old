CREATE TABLE [dbo].[SYSUserProfile](  
    [SYSUserProfileID] [int] IDENTITY(1,1) NOT NULL,  
    [SYSUserID] [int] NOT NULL,  
    [FirstName] [varchar](50) NOT NULL,  
    [LastName] [varchar](50) NOT NULL,  
    [Gender] [char](1) NOT NULL,  
    [RowCreatedSYSUserID] [int] NOT NULL,  
    [RowCreatedDateTime] [datetime] DEFAULT GETDATE(),  
    [RowModifiedSYSUserID] [int] NOT NULL,  
    [RowModifiedDateTime] [datetime] DEFAULT GETDATE(),  
    PRIMARY KEY (SYSUserProfileID)  
    )  
GO  
  
ALTER TABLE [dbo].[SYSUserProfile]  WITH CHECK ADD FOREIGN KEY([SYSUserID])  
REFERENCES [dbo].[SYSUser] ([SYSUserID])  
GO 