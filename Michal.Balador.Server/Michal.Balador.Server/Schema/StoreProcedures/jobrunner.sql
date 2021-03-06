USE [runnerdevice]
GO
/****** Object:  StoredProcedure [dbo].[jobrunner]    Script Date: 4/13/2018 8:57:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[jobrunner]
  as 
  declare @jobid uniqueidentifier =NEWID(); declare @d int;
  begin
  SET XACT_ABORT ON
	BEGIN TRY 
	 BEGIN TRANSACTION; 
 declare @spid int =@@SPID;
  --select @d=count(*) from [dbo].[Balador_JobSender] where status=1--prod
  select @d=count(*) from [dbo].[Balador_JobSender] where status=0--test

   if(@d is not null and @d>0)
	  begin
		SELECT '' as ID,'' as Email, @jobid as JobId,'' as NickName,'' as  [MessagesType]
	  end
	else
	begin
		INSERT INTO [dbo].[Balador_JobSender]
					([Id] ,[CreatedOn] ,[ModifiedOn] ,[Status])
			 VALUES
				   (@jobid,GETUTCDATE(),GETUTCDATE(),0);

		update [dbo].[Balador_ClientMessage]
		set jobid=@jobid,[Status]=1,ModifiedOn=GETUTCDATE()
		where [Status]=0 and jobid is null;

	
		SELECT U.ID as AccountId,U.Email, @jobid as JobId,U.NickName as [Name], u.UserName as  [UserName],@spid as Spid,
		 substring(
        (
            Select ','+ST1.MesssageType  AS [text()]
            From dbo.Balador_ClientMessage ST1
            Where ST1.AccountId = U.Id
            GROUP BY ST1.MesssageType
            For XML PATH ('')
        ), 2, 1000) [MessagesType]
		
		 FROM [User] U
		 where EXISTS (SELECT BC.Id From dbo.Balador_ClientMessage BC
								WHERE BC.AccountId=U.Id AND BC.[Status]=1 and bc.JobId =@jobid);

	 end
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
