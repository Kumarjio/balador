-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE balador_sp_getContacts
	-- Add the parameters for the stored procedure here
	@jobid uniqueidentifier ,@messassnger nvarchar(100),@accountid  nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select l.leadid as LeadId,l.clientid as Id, null as ContactId,@jobid as JobId, @messassnger as MesssageType,
	cast(0 as bit) IsAutorize,[NickName] as NickName, @accountid as Accountid 
		from [dbo].[Balador_Leads] l
			where  l.leadid in (SELECT leadid
								  FROM [dbo].[Balador_ClientMessage] sc
								  where [Status]=1 and [JobId]=@jobid and MesssageType=@messassnger and sc.AccountId=@accountid and leadid is not null and contactid is null
										group by leadid) 
	union
	select null  as LeadId, u.username as clientid, u.id as ContactId,@jobid as JobId, @messassnger as MesssageType,
	 cast(1 as bit) IsAutorize,[NickName] as NickName, @accountid as Accountid 
		from [dbo].[User] u
			where  u.Id in (SELECT contactid
								  FROM [dbo].[Balador_ClientMessage] sc
								  where [Status]=1 and [JobId]=@jobid and MesssageType=@messassnger and sc.AccountId=@accountid and leadid is  null and contactid is not null
										group by contactid) 


END
GO
