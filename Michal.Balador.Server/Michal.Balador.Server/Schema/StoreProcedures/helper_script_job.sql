USE [runnerdevice]
GO
delete from [dbo].[Balador_JobSender];
UPDATE [dbo].[Balador_ClientMessage]  SET [JobId] =null ,[Status]=0;
 DELETE FROM [dbo].[Balador_Leads]
GO


