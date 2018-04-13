USE [runnerdevice]
GO
/****** Object:  StoredProcedure [dbo].[balador_sp_getMessages]    Script Date: 4/13/2018 11:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[balador_sp_getMessages]
	-- Add the parameters for the stored procedure here
	@jobid uniqueidentifier ,@messassnger nvarchar(100),@accountid  nvarchar(50),@clientid nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	 declare @spid int =@@SPID;
	SELECT   sc.ClientId as ClientIdId,sc.Messsage as Message,sc.Id as RecordId,@spid as Spid, @accountid as Accountid ,@jobid AS JobId
	
	 FROM [dbo].[Balador_ClientMessage] sc
		where [Status]=1 and [JobId]=@jobid  and sc.AccountId=@accountid and ClientId= @clientid and MesssageType=@messassnger
										 
	

END
