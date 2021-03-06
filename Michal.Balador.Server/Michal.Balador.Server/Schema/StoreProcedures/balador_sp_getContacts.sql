USE [runnerdevice]
GO
/****** Object:  StoredProcedure [dbo].[balador_sp_getContacts]    Script Date: 4/15/2018 11:33:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---EXEC [dbo].[jobrunner]

--EXEC [dbo].[balador_sp_getContacts] '981C0449-C5C2-4853-8F0F-83968FA64124','com.baladorPlant$MockHttpSender','1bfa6f8e-0000-0000-0000-b573b2c8f820'
ALTER PROCEDURE [dbo].[balador_sp_getContacts]
	-- Add the parameters for the stored procedure here
	@jobid uniqueidentifier ,@messassnger nvarchar(100),@accountid  nvarchar(50)
AS
BEGIN  
declare @spid int =@@SPID;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.


	SET NOCOUNT ON;
	select l.leadid as LeadId,l.clientid as clientid, N'' as ContactId
					,@jobid as JobId, @messassnger as MesssageType,@spid as spid,
	cast(0 as bit) IsAutorize,[NickName] as NickName, @accountid as Accountid 
		from [dbo].[Balador_Leads] l
			where  l.leadid in (SELECT leadid
								  FROM [dbo].[Balador_ClientMessage] sc
								  where [Status]=1 and [JobId]=@jobid and MesssageType=@messassnger and sc.AccountId=@accountid and leadid is not null and contactid is null
										group by leadid) 
	union
	select null  as LeadId, u.username as clientid, u.id as ContactId
					,@jobid as JobId, @messassnger as MesssageType,@spid as spid,
	cast(1 as bit) IsAutorize,[NickName] as NickName, @accountid as Accountid 
		from [dbo].[User] u
			where  u.Id in (SELECT contactid
								  FROM [dbo].[Balador_ClientMessage] sc
								  where [Status]=1 and [JobId]=@jobid and MesssageType=@messassnger and sc.AccountId=@accountid and leadid is  null and contactid is not null
										group by contactid) 


END
