CREATE TABLE [dbo].[SYSUser](  
        [SYSUserID] [int] IDENTITY(1,1) NOT NULL,  
        [LoginName] [varchar](50) NOT NULL,  
        [PasswordEncryptedText] [varchar](200) NOT NULL,  
        [RowCreatedSYSUserID] [int] NOT NULL,  
        [RowCreatedDateTime] [datetime] DEFAULT GETDATE(),  
        [RowModifiedSYSUserID] [int] NOT NULL,  
        [RowMOdifiedDateTime] [datetime] DEFAULT GETDATE(),  
        PRIMARY KEY (SYSUserID)  
    )  
