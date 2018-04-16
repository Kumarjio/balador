USE [runnerdevice]
GO
/****** Object:  StoredProcedure [dbo].[jobrunner]    Script Date: 4/12/2018 11:18:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter  proc [dbo].[job_complete]
@jobid uniqueidentifier
  as 
  
  begin
  SET XACT_ABORT ON
	BEGIN TRY 
	 BEGIN TRANSACTION; 
 
  --select @d=count(*) from [dbo].[Balador_JobSender] where status=1--prod
  
		update [dbo].Balador_JobSender
		set [Status]=1,ModifiedOn=GETUTCDATE()
		where id=@jobid;

	
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
