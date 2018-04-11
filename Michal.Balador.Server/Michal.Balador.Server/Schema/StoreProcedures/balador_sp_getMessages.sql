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

alter PROCEDURE balador_sp_getMessages
	-- Add the parameters for the stored procedure here
	@jobid uniqueidentifier ,@messassnger nvarchar(100),@accountid  nvarchar(50),@clientid nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT sc.Id as Id,sc.Messsage as [Message] FROM [dbo].[Balador_ClientMessage] sc
		where [Status]=1 and [JobId]=@jobid  and sc.AccountId=@accountid and ClientId= @clientid and MesssageType=@messassnger
										 
	

END
GO
