USE [runnerdevice]
GO
/****** Object:  StoredProcedure [dbo].[createMessage]    Script Date: 4/11/2018 10:44:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[createMessage]
	@message nvarchar(max),@user nvarchar(50)
	,@clientid nvarchar(max),@messageType nvarchar(50),@nickName nvarchar(50),@replay bit
  as 
  declare @id uniqueidentifier =NEWID();
  declare @userid nvarchar(128)='';
  declare @dt datetime = getutcdate();
  declare @contactid uniqueidentifier =null;
  declare @leadid uniqueidentifier =null;
  --declare @isErr bit=0, @errDesc nvarchar(max)=N'',@errTitle nvarchar(100)= N'',@isWarning bit=0,@errCode  nvarchar(100)=N'';

  begin
  SET XACT_ABORT ON
	BEGIN TRY 
	 BEGIN TRANSACTION; 
		select @userid=[Id] from [dbo].[User] where [UserName]=@user;
		if(@userid is  null)
		begin 
			select @userid
		end
		else
		begin
			select @contactid=[Id] from [dbo].[User] where [UserName]=@clientid;
			if(@contactid is null)
			begin
				select @leadid= [LeadId] from [dbo].[Balador_Leads] l where l.username=@clientid;
				if(@leadid is null)
					begin
					set @leadid=NEWID();
						INSERT INTO [dbo].[Balador_Leads]
								   ([LeadId] ,[NickName] ,username,MessageType,clientId,[Owner]
								   ,[CreatedOn]  ,[ModifiedOn])
							 VALUES
								   (@leadid,@nickName ,@clientid,@messageType,@clientid,@userid,
								   @dt,   @dt)
					end
			end
			INSERT INTO [dbo].[Balador_ClientMessage]
					   ([Id] ,[ContactId]  ,LeadId,[ClientId]
					   ,[Messsage] ,[AccountId]   ,[CreatedOn]
					   ,[ModifiedOn],[Status]  ,[MesssageType]
					   ,[ConversationId]   ,[JobId]  ,[Replay]
					   ,[Archive],[NickName],[Retry],[Direction])
				 VALUES
					   (@id  ,@contactid ,@leadid,@clientid
					   ,@message   ,@userid
					   ,@dt	   ,@dt
					   ,0  ,@messageType
					   ,@id   ,null ,@replay   ,0   ,@nickName ,0,0)
    
		end
		select @id;
	 COMMIT TRANSACTION;
	END TRY 
	BEGIN CATCH  
	   -- print @@TRANCOUNT
		 IF XACT_STATE() <> 0
		BEGIN
			ROLLBACK TRANSACTION;
		END
		   -- Call the procedure to raise the original error.
		 EXEC usp_RethrowError;
  
	END CATCH;  
  end
