-- Quiz Management Systemm :

-- -- -- -- -- -- -- -- -- -- --
-- -- -- -- -- -- -- -- -- -- --

-- MST_User :

-- 1. SelectAll
-- EXEC PR_MST_User_SelectAll
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectAll]
AS
BEGIN
	SELECT [dbo].[MST_User].[UserID] , 
		   [dbo].[MST_User].[UserName] ,
		   [dbo].[MST_User].[Password] , 
		   [dbo].[MST_User].[Email] ,
		   [dbo].[MST_User].[Mobile] , 
		   [dbo].[MST_User].[IsActive] ,
		   [dbo].[MST_User].[IsAdmin] ,
		   [dbo].[MST_User].[Created] , 
		   [dbo].[MST_User].[Modified]
	FROM [dbo].[MST_User]
	ORDER BY [dbo].[MST_User].[UserName]
END


-- 2. SelectByPK
-- EXEC PR_MST_User_SelectByPK 101
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectByPK]
@UserID INT
AS
BEGIN
	SELECT [dbo].[MST_User].[UserID] , 
	       [dbo].[MST_User].[UserName] ,
		   [dbo].[MST_User].[Password] , 
		   [dbo].[MST_User].[Email] ,
		   [dbo].[MST_User].[Mobile] , 
		   [dbo].[MST_User].[IsActive] ,
		   [dbo].[MST_User].[IsAdmin] ,
		   [dbo].[MST_User].[Created] , 
		   [dbo].[MST_User].[Modified]
	FROM   [dbo].[MST_User]
	WHERE  [dbo].[MST_User].[UserID] = @UserID
END


-- 3.Insert
--EXEC PR_MST_User_Insert 'UserName', 'Password', 'Email', 'Moblie'
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Insert]
	@UserName	NVARCHAR(100),
	@Password	NVARCHAR(100),
	@Email		NVARCHAR(100),
	@Mobile		NVARCHAR(100)
AS
BEGIN
	INSERT INTO [dbo].[MST_User] (
									[dbo].[MST_User].[UserName] ,
								    [dbo].[MST_User].[Password] , 
								    [dbo].[MST_User].[Email] ,
								    [dbo].[MST_User].[Mobile] ,
								    [dbo].[MST_User].[Modified]
								 )
	VALUES (@UserName, @Password, @Email, @Mobile,GETDATE())
END


-- 4. Update
-- EXEC PR_MST_User_Update 'UserName', 'Password', 'Email', 'Moblie', 1, 1
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Update]
	@UserID		INT,
	@UserName	NVARCHAR(100),
	@Password	NVARCHAR(100),
	@Email		NVARCHAR(100),
	@Mobile		NVARCHAR(100)
AS
BEGIN
	UPDATE [dbo].[MST_User]
	SET [dbo].[MST_User].[UserName]	= @UserName ,
		[dbo].[MST_User].[Password] = @Password ,
		[dbo].[MST_User].[Email]	= @Email ,
		[dbo].[MST_User].[Mobile]	= @Mobile ,
		[dbo].[MST_User].[Modified]	= GETDATE()
	WHERE [dbo].[MST_User].[UserID] = @UserID
END


-- 5. Delete
-- EXEC PR_MST_User_Delete 101
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Delete] 
@UserID INT 
AS
BEGIN
	DELETE FROM [dbo].[MST_User] 
	WHERE [dbo].[MST_User].[UserID] = @UserID
END


-- 6. SelectByUsernameAndPassword
-- EXEC PR_MST_User_SelectByUsernamePassword 'UserName','Password'
CREATE OR ALTER  PROCEDURE [dbo].[PR_MST_User_SelectByUsernamePassword]
	@UserName	VARCHAR(100),
	@Password	VARCHAR(100)
AS
BEGIN
	SELECT [dbo].[MST_User].[UserName],
		   [dbo].[MST_User].[Password],
		   [dbo].[MST_User].[UserID]
	FROM [dbo].[MST_User] 
	WHERE ( [dbo].[MST_User].[UserName]  = @UserName
		    OR [dbo].[MST_User].[Email]  = @UserName 
			OR [dbo].[MST_User].[Mobile] = @UserName)
	AND [dbo].[MST_User].[Password] = @Password
END


-- -- -- -- -- -- -- -- -- -- --
-- -- -- -- -- -- -- -- -- -- --

-- MST_Quiz :

-- 1. SelectAll
-- EXEC PR_MST_Quiz_SelectAll 12
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_SelectAll] 
@UserID INT
AS
BEGIN
	SELECT [dbo].[MST_Quiz].[QuizID],
		   [dbo].[MST_Quiz].[QuizName],
		   [dbo].[MST_Quiz].[TotalQuestions],
		   [dbo].[MST_Quiz].[QuizDate],
		   [dbo].[MST_Quiz].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_User].[Password],
		   [dbo].[MST_User].[Email],
		   [dbo].[MST_User].[Mobile],
		   [dbo].[MST_User].[isActive],
		   [dbo].[MST_User].[isAdmin],
		   [dbo].[MST_Quiz].[Created], 
		   [dbo].[MST_Quiz].[Modified]
	FROM [dbo].[MST_Quiz] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_Quiz].[UserID] = [dbo].[MST_User].[UserID]
	WHERE [dbo].[MST_User].[UserID] = @UserID
	ORDER BY [dbo].[MST_User].[UserName]
END


-- 2. SelectByPK
-- EXEC PR_MST_Quiz_SelectByPK 1
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_SelectByPK] 
@QuizID INT ,
@UserID INT
AS
BEGIN
	SELECT [dbo].[MST_Quiz].[QuizID],
		   [dbo].[MST_Quiz].[QuizName],
		   [dbo].[MST_Quiz].[TotalQuestions],
		   [dbo].[MST_Quiz].[QuizDate],
		   [dbo].[MST_Quiz].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_User].[Password],
		   [dbo].[MST_User].[Email],
		   [dbo].[MST_User].[Mobile],
		   [dbo].[MST_User].[isActive],
		   [dbo].[MST_User].[isAdmin],
		   [dbo].[MST_Quiz].[Created], 
		   [dbo].[MST_Quiz].[Modified]
	FROM [dbo].[MST_Quiz] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_Quiz].[UserID] = [dbo].[MST_User].[UserID]
	WHERE [dbo].[MST_Quiz].[QuizID] = @QuizID
	AND [dbo].[MST_User].[UserID] = @UserID
END


-- 3. Insert
-- EXEC PR_MST_Quiz_Insert 'QuizName', 10, '2025-01-25', 101
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Insert]
	@QuizName		NVARCHAR(100),
	@TotalQuestions	INT,	
	@QuizDate		DATETIME,
	@UserID			INT
AS 
BEGIN
	INSERT INTO [dbo].[MST_Quiz](
									[dbo].[MST_QUIZ].[QuizName],
									[dbo].[MST_QUIZ].[TotalQuestions],
									[dbo].[MST_QUIZ].[QuizDate],
									[dbo].[MST_QUIZ].[UserID],
									[dbo].[MST_QUIZ].[Modified]
								) 
	VALUES (@QuizName, @TotalQuestions, @QuizDate, @UserID, GETDATE())
END


-- 4. Update
-- EXEC PR_MST_Quiz_Update 1, 'QuizName', 10, '2025-01-25', 101
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Update]
	@QuizID			INT,
	@QuizName		NVARCHAR(100),
	@TotalQuestions INT,	
	@QuizDate		DATETIME,
	@UserID			INT
AS
BEGIN
	UPDATE [dbo].[MST_Quiz]
	SET	[dbo].[MST_Quiz].[QuizName]       =	@QuizName,
		[dbo].[MST_Quiz].[TotalQuestions] = @TotalQuestions,
		[dbo].[MST_Quiz].[QuizDate]		  = @QuizDate,
		[dbo].[MST_Quiz].[UserID]		  = @UserID,
		[dbo].[MST_Quiz].[Modified]		  = GETDATE()
	WHERE [dbo].[MST_Quiz].[QuizID]		  = @QuizID
	AND   [dbo].[MST_Quiz].[UserID]       = @UserID
END


-- 5. Delete
-- EXEC PR_MST_Quiz_Delete 1
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Delete]
	@QuizID INT ,
	@UserID INT
AS
BEGIN
	DELETE FROM [dbo].[MST_Quiz] 
	WHERE [dbo].[MST_Quiz].[QuizID] = @QuizID
	AND [dbo].[MST_Quiz].[UserID] = @UserID
END


-- -- -- -- -- -- -- -- -- -- --
-- -- -- -- -- -- -- -- -- -- --

-- MST_Question :

-- 1. SelectAll
-- EXEC PR_MST_Question_SelectAll
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_SelectAll] 
AS
BEGIN
	SELECT [dbo].[MST_QUESTION].[QuestionId],
			   [dbo].[MST_Question].[QuestionText],
			   [dbo].[MST_Question].[QuestionLevelID],
			   [dbo].[MST_QuestionLevel].[QuestionLeveL],
			   [dbo].[MST_Question].[OptionA],
			   [dbo].[MST_Question].[OptionB],
			   [dbo].[MST_Question].[OptionC],
			   [dbo].[MST_Question].[OptionD],
			   [dbo].[MST_Question].[CorrectOption],
			   [dbo].[MST_Question].[QuestionMarks],
			   [dbo].[MST_Question].[IsActive],
			   [dbo].[MST_Question].[UserID],
			   [dbo].[MST_User].[UserName],
			   [dbo].[MST_User].[Password],
			   [dbo].[MST_User].[Email],
			   [dbo].[MST_User].[Mobile],
			   [dbo].[MST_User].[isActive],
			   [dbo].[MST_User].[isAdmin],
			   [dbo].[MST_Question].[Created],
			   [dbo].[MST_Question].[Modified]
	FROM [dbo].[MST_Question] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_Question].[UserID] = [dbo].[MST_User].[UserID]
	INNER JOIN [dbo].[MST_QuestionLevel]
	ON[dbo].[MST_QUESTION].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
	ORDER BY [dbo].[MST_User].[UserName]
END


-- 2. SelecctByPK
-- EXEC PR_MST_Question_SelectByPK 1
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_SelectByPK]
	@QuestionID INT 
AS
BEGIN
	SELECT [dbo].[MST_Question].[QuestionId],
			   [dbo].[MST_Question].[QuestionText],
			   [dbo].[MST_Question].[QuestionLevelID],
			   [dbo].[MST_QuestionLevel].[QuestionLeveL],
			   [dbo].[MST_Question].[OptionA],
			   [dbo].[MST_Question].[OptionB],
			   [dbo].[MST_Question].[OptionC],
			   [dbo].[MST_Question].[OptionD],
			   [dbo].[MST_Question].[CorrectOption],
			   [dbo].[MST_Question].[QuestionMarks],
			   [dbo].[MST_Question].[IsActive],
			   [dbo].[MST_Question].[UserID],
			   [dbo].[MST_User].[UserName],
			   [dbo].[MST_User].[Password],
			   [dbo].[MST_User].[Email],
			   [dbo].[MST_User].[Mobile],
			   [dbo].[MST_User].[isActive],
			   [dbo].[MST_User].[isAdmin],
			   [dbo].[MST_Question].[Created],
			   [dbo].[MST_Question].[Modified]
	FROM [dbo].[MST_Question] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_Question].[UserID] = [dbo].[MST_User].[UserID]
	INNER JOIN [dbo].[MST_QuestionLevel]
	ON[dbo].[MST_QUESTION].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
	WHERE [dbo].[MST_Question].[QuestionId] = @QuestionID
END


-- 3. Insert
-- EXEC PR_MST_Question_Insert 'QestionText', 2, 'OptionA', 'OptionB', 'OptionC', 'OptionD', 'CorrectOption', 5, 1, 101
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Insert]
	@QuestionText		NVARCHAR(100),
	@QuestionLevelID	INT,
	@OptionA			NVARCHAR(100),
	@OptionB			NVARCHAR(100),
	@OptionC			NVARCHAR(100),
	@OptionD			NVARCHAR(100),
	@CorrectOption		NVARCHAR(100),
	@QuestionMarks		INT,
	@UserID				INT
AS
BEGIN
	INSERT INTO [dbo].[MST_Question](
									   [dbo].[MST_Question].[QuestionText],
									   [dbo].[MST_Question].[QuestionLevelID],
									   [dbo].[MST_Question].[OptionA],
									   [dbo].[MST_Question].[OptionB],
									   [dbo].[MST_Question].[OptionC],
									   [dbo].[MST_Question].[OptionD],
									   [dbo].[MST_Question].[CorrectOption],
									   [dbo].[MST_Question].[QuestionMarks],
									   [dbo].[MST_Question].[UserID],
									   [dbo].[MST_Question].[Modified]
								   )
	VALUES(
		    @QuestionText,
		    @QuestionLevelID,
		    @OptionA,
		    @OptionB,
			@OptionC,
			@OptionD,
		    @CorrectOption,
			@QuestionMarks,
			@UserID,
			GETDATE()
		 )
END

-- 4. Update
-- EXEC PR_MST_Question_Update 1, 'QestionText', 2, 'OptionA', 'OptionB', 'OptionC', 'OptionD', 'CorrectOption', 5, 1, 101
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Update]
	@QuestionID			INT,
	@QuestionText		NVARCHAR(100),
	@QuestionLevelID	INT,
	@OptionA			NVARCHAR(100),
	@OptionB			NVARCHAR(100),
	@OptionC			NVARCHAR(100),
	@OptionD			NVARCHAR(100),
	@CorrectOption		NVARCHAR(100),
	@QuestionMarks		INT,
	@UserID				INT
AS
BEGIN
	UPDATE [dbo].[MST_Question]
	SET	[dbo].[MST_Question].[QuestionText]    =	@QuestionText,
		[dbo].[MST_Question].[QuestionLevelID] =	@QuestionLevelID,
		[dbo].[MST_Question].[OptionA]		   =	@OptionA,
		[dbo].[MST_Question].[OptionB]		   =	@OptionB,
	    [dbo].[MST_Question].[OptionC]	       =	@OptionC,
	    [dbo].[MST_Question].[OptionD]		   =	@OptionD,
		[dbo].[MST_Question].[CorrectOption]   =	@CorrectOption,
		[dbo].[MST_Question].[QuestionMarks]   =	@QuestionMarks,
		[dbo].[MST_Question].[UserID]          =	@UserID,
		[dbo].[MST_Question].[Modified]        =	GETDATE()
	WHERE [dbo].[MST_Question].[QuestionID]    =	@QuestionID
END


-- 5. Delete
-- EXEC PR_MST_Question_Delete 1
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Delete]
	@QuestionID INT 
AS
BEGIN
	DELETE FROM [dbo].[MST_Question] 
	WHERE [dbo].[MST_Question].[QuestionID] = @QuestionID
END


-- -- -- -- -- -- -- -- -- -- --
-- -- -- -- -- -- -- -- -- -- --

-- MST_QuestionLevel :

-- 1. SelectAll
-- EXEC PR_MST_QestionLevel_SelectAll
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QestionLevel_SelectAll] 
AS
BEGIN
	SELECT [dbo].[MST_QuestionLevel].[QuestionLevelID],
		   [dbo].[MST_QuestionLevel].[QuestionLevel],
		   [dbo].[MST_User].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_User].[Email],
		   [dbo].[MST_User].[Mobile],
		   [dbo].[MST_User].[isActive],
		   [dbo].[MST_User].[isAdmin],
		   [dbo].[MST_QuestionLevel].[Created] , 
		   [dbo].[MST_QuestionLevel].[Modified]
	FROM [dbo].[MST_QuestionLevel]
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_QuestionLevel].[UserID] = [dbo].[MST_User].[UserID]
	ORDER BY [dbo].[MST_User].[UserName]
END


-- 2. SelectByPK
-- EXEC PR_MST_QestionLevel_SelectByPK
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QestionLevel_SelectByPK] 
	@QuestionLevelID INT 
AS
BEGIN
	SELECT [dbo].[MST_QuestionLevel].[QuestionLevelID],
		   [dbo].[MST_QuestionLevel].[QuestionLevel],
		   [dbo].[MST_User].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_User].[Email],
		   [dbo].[MST_User].[Mobile],
		   [dbo].[MST_User].[isActive],
		   [dbo].[MST_User].[isAdmin],
		   [dbo].[MST_QuestionLevel].[Created] , 
		   [dbo].[MST_QuestionLevel].[Modified]
	FROM [dbo].[MST_QuestionLevel]
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_QuestionLevel].[UserID] = [dbo].[MST_User].[UserID]
	WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID
END


-- 3. Insert
-- EXEC PR_MST_QestionLevel_Insert 'Medium', 13
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QestionLevel_Insert]
	@QuestionLevel		NVARCHAR(100),
	@UserID				INT
AS
BEGIN
	INSERT INTO [dbo].[MST_QuestionLevel]( 
											[dbo].[MST_QuestionLevel].[QuestionLevel],
											[dbo].[MST_QuestionLevel].[UserID],
											[dbo].[MST_QuestionLevel].[Modified]
										 )
	VALUES (@QuestionLevel, @UserID, GETDATE())
END


-- 4. Update
-- EXEC PR_MST_QestionLevel_Update 3, 'Hard', 101
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QestionLevel_Update]
	@QuestionLevelID	INT,
	@QuestionLevel		NVARCHAR(100),
	@UserID				INT
AS
BEGIN
	UPDATE [dbo].[MST_QuestionLevel]
	SET	[dbo].[MST_QuestionLevel].[QuestionLevel] = @QuestionLevel,
		[dbo].[MST_QuestionLevel].[UserID] = @UserID,
		[dbo].[MST_QuestionLevel].[Modified] = GETDATE()
	WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID
END


-- 5. Delete
-- EXEC PR_MST_QestionLevel_Delete 2
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QestionLevel_Delete] 
	@QuestionLevelID INT 
AS
BEGIN
	DELETE FROM [dbo].[MST_QuestionLevel] 
	WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID
END


-- -- -- -- -- -- -- -- -- -- --
-- -- -- -- -- -- -- -- -- -- --

-- MST_QuizWiseQuestions :

-- 1. SelectAll
-- EXEC PR_MST_QuizWiseQuestions_SelectAll
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_SelectAll] 
AS
BEGIN
	SELECT [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
		   [dbo].[MST_QuizWiseQuestions].[QuizID],
		   [dbo].[MST_Quiz].[QuizName],
		   [dbo].[MST_Quiz].[TotalQuestions],
		   [dbo].[MST_Quiz].[QuizDate],
		   [dbo].[MST_QuizWiseQuestions].[QuestionID],
		   [dbo].[MST_Question].[QuestionId],
		   [dbo].[MST_Question].[QuestionText],
		   [dbo].[MST_Question].[OptionA],
		   [dbo].[MST_Question].[OptionB],
		   [dbo].[MST_Question].[OptionC],
		   [dbo].[MST_Question].[OptionD],
		   [dbo].[MST_Question].[CorrectOption],
		   [dbo].[MST_Question].[QuestionMarks],
		   [dbo].[MST_Question].[IsActive],
		   [dbo].[MST_QuizWiseQuestions].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_User].[Password],
		   [dbo].[MST_User].[Email],
		   [dbo].[MST_User].[Mobile],
		   [dbo].[MST_User].[isActive],
		   [dbo].[MST_User].[isAdmin],
		   [dbo].[MST_QuizWiseQuestions].[Created],
		   [dbo].[MST_QuizWiseQuestions].[Modified]
	FROM [dbo].[MST_QuizWiseQuestions] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_QuizWiseQuestions].[UserID] = [dbo].[MST_User].[UserID]
	INNER JOIN [dbo].[MST_Question] 
	ON [dbo].[MST_QuizWiseQuestions].[QuestionID] = [dbo].[MST_Question].[QuestionID]
	INNER JOIN [dbo].[MST_Quiz] 
	ON [dbo].[MST_QuizWiseQuestions].[QuizID] = [dbo].[MST_Quiz].[QuizID]
	ORDER BY [dbo].[MST_User].[UserName]
END



-- 2. SelectByPK
-- EXEC PR_MST_QuizWiseQuestions_SelectByPK 10
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_SelectByPK] 
	@QuizWiseQuestionsID INT 
AS
BEGIN
	SELECT [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
		   [dbo].[MST_QuizWiseQuestions].[QuizID],
		   [dbo].[MST_Quiz].[QuizName],
		   [dbo].[MST_Quiz].[TotalQuestions],
		   [dbo].[MST_Quiz].[QuizDate],
		   [dbo].[MST_QuizWiseQuestions].[QuestionID],
		   [dbo].[MST_Question].[QuestionId],
		   [dbo].[MST_Question].[QuestionText],
		   [dbo].[MST_Question].[OptionA],
		   [dbo].[MST_Question].[OptionB],
		   [dbo].[MST_Question].[OptionC],
		   [dbo].[MST_Question].[OptionD],
		   [dbo].[MST_Question].[CorrectOption],
		   [dbo].[MST_Question].[QuestionMarks],
		   [dbo].[MST_Question].[IsActive],
		   [dbo].[MST_QuizWiseQuestions].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_User].[Password],
		   [dbo].[MST_User].[Email],
		   [dbo].[MST_User].[Mobile],
		   [dbo].[MST_User].[isActive],
		   [dbo].[MST_User].[isAdmin],
		   [dbo].[MST_QuizWiseQuestions].[Created],
		   [dbo].[MST_QuizWiseQuestions].[Modified]
	FROM [dbo].[MST_QuizWiseQuestions] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_QuizWiseQuestions].[UserID] = [dbo].[MST_User].[UserID]
	INNER JOIN [dbo].[MST_Question] 
	ON [dbo].[MST_QuizWiseQuestions].[QuestionID] = [dbo].[MST_Question].[QuestionID]
	INNER JOIN [dbo].[MST_Quiz] 
	ON [dbo].[MST_QuizWiseQuestions].[QuizID] = [dbo].[MST_Quiz].[QuizID]
	WHERE  [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID
END


-- 3. Insert
-- EXEC PR_MST_QuizWiseQuestions_Insert 1, 10, 101
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_Insert]
	@QuizID			INT,
	@QuestionID		INT,
	@UserID			INT
AS
BEGIN
	INSERT INTO [dbo].[MST_QuizWiseQuestions](  
												[dbo].[MST_QuizWiseQuestions].[QuizID],
												[dbo].[MST_QuizWiseQuestions].[QuestionID],
												[dbo].[MST_QuizWiseQuestions].[UserID],
												[dbo].[MST_QuizWiseQuestions].[Modified]
											 )
	VALUES (@QuizID, @QuestionID, @UserID, GETDATE())
END


-- 4. Update
-- EXEC PR_MST_QuizWiseQuestions_Update 1, 2, 3, 101
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_Update]
	@QuizWiseQuestionsID	INT,
	@QuizID					INT,
	@QuestionID				INT,
	@UserID					INT
AS
BEGIN
	UPDATE [dbo].[MST_QuizWiseQuestions]
	SET	[dbo].[MST_QuizWiseQuestions].[QuizID]		 =	@QuizID,
		[dbo].[MST_QuizWiseQuestions].[QuestionID]	 =	@QuestionID,
		[dbo].[MST_QuizWiseQuestions].[UserID]		 =	@UserID,
		[dbo].[MST_QuizWiseQuestions].[Modified]	 =	GETDATE()
	WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID
END 


-- 5. Delete
-- EXEC PR_MST_QuizWiseQuestions_Delete 1
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_Delete] 
	@QuizWiseQuestionsID INT 
AS
BEGIN
	DELETE FROM [dbo].[MST_QuizWiseQuestions] 
	WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID
END


-- -- -- -- -- -- -- -- -- -- --
-- -- -- -- -- -- -- -- -- -- --

-- DropDown SP :

-- User :
CREATE OR ALTER PROCEDURE PR_MST_User_DropdownForUser
AS
BEGIN
	SELECT
		[dbo].[MST_User].[UserID],
		[dbo].[MST_User].[UserName]
	FROM [dbo].[MST_User]
END


-- Quiz :

CREATE OR ALTER PROCEDURE PR_MST_Quiz_DropdownForQuiz
AS
BEGIN
	SELECT
		[dbo].[MST_Quiz].[QuizID],
		[dbo].[MST_Quiz].[QuizName]
	FROM [dbo].[MST_Quiz]
END


-- Questions :

CREATE OR ALTER PROCEDURE PR_MST_Question_DropdownForQuestion
AS
BEGIN
	SELECT
		[dbo].[MST_Question].[QuestionID],
		[dbo].[MST_Question].[QuestionText]
	FROM [dbo].[MST_Question]
END


-- QuestionLevel :

CREATE OR ALTER PROCEDURE PR_MST_QuestionLevel_DropdownForQuestionLevel
AS
BEGIN
	SELECT
		[dbo].[MST_QuestionLevel].[QuestionLevelID],
		[dbo].[MST_QuestionLevel].[QuestionLevel]
	FROM [dbo].[MST_QuestionLevel]
END




-- -- -- -- -- -- -- -- -- -- --
-- -- -- -- -- -- -- -- -- -- --

-- Search SP :

-- Quiz
CREATE OR ALTER PROCEDURE PR_MST_QuizWiseQuestions_SelectByQuizID
    @QuizID INT
AS
BEGIN
    SELECT [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
		   [dbo].[MST_QuizWiseQuestions].[QuizID],
		   [dbo].[MST_Quiz].[QuizName],
		   [dbo].[MST_Quiz].[TotalQuestions],
		   [dbo].[MST_Quiz].[QuizDate],
		   [dbo].[MST_QuizWiseQuestions].[QuestionID],
		   [dbo].[MST_Question].[QuestionId],
		   [dbo].[MST_Question].[QuestionText],
		   [dbo].[MST_Question].[OptionA],
		   [dbo].[MST_Question].[OptionB],
		   [dbo].[MST_Question].[OptionC],
		   [dbo].[MST_Question].[OptionD],
		   [dbo].[MST_Question].[CorrectOption],
		   [dbo].[MST_Question].[QuestionMarks],
		   [dbo].[MST_Question].[IsActive],
		   [dbo].[MST_QuizWiseQuestions].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_User].[Password],
		   [dbo].[MST_User].[Email],
		   [dbo].[MST_User].[Mobile],
		   [dbo].[MST_User].[isActive],
		   [dbo].[MST_User].[isAdmin],
		   [dbo].[MST_QuizWiseQuestions].[Created],
		   [dbo].[MST_QuizWiseQuestions].[Modified]
	FROM [dbo].[MST_QuizWiseQuestions] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_QuizWiseQuestions].[UserID] = [dbo].[MST_User].[UserID]
	INNER JOIN [dbo].[MST_Question] 
	ON [dbo].[MST_QuizWiseQuestions].[QuestionID] = [dbo].[MST_Question].[QuestionID]
	INNER JOIN [dbo].[MST_Quiz] 
	ON [dbo].[MST_QuizWiseQuestions].[QuizID] = [dbo].[MST_Quiz].[QuizID]
    WHERE [dbo].[MST_QuizWiseQuestions].[QuizID] = @QuizID
END


-- QuestionLevel AND Question :
CREATE OR ALTER PROCEDURE PR_MST_Question_SelectByFilters
    @QuestionLevelID INT = NULL,
    @QuestionID INT = NULL
AS
BEGIN
    SELECT [dbo].[MST_Question].[QuestionId],
			   [dbo].[MST_Question].[QuestionText],
			   [dbo].[MST_Question].[QuestionLevelID],
			   [dbo].[MST_QuestionLevel].[QuestionLevel],
			   [dbo].[MST_Question].[OptionA],
			   [dbo].[MST_Question].[OptionB],
			   [dbo].[MST_Question].[OptionC],
			   [dbo].[MST_Question].[OptionD],
			   [dbo].[MST_Question].[CorrectOption],
			   [dbo].[MST_Question].[QuestionMarks],
			   [dbo].[MST_Question].[IsActive],
			   [dbo].[MST_Question].[UserID],
			   [dbo].[MST_User].[UserName],
			   [dbo].[MST_User].[Password],
			   [dbo].[MST_User].[Email],
			   [dbo].[MST_User].[Mobile],
			   [dbo].[MST_User].[isActive],
			   [dbo].[MST_User].[isAdmin],
			   [dbo].[MST_Question].[Created],
			   [dbo].[MST_Question].[Modified]
	FROM [dbo].[MST_Question] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_Question].[UserID] = [dbo].[MST_User].[UserID]
	INNER JOIN [dbo].[MST_QuestionLevel]
	ON[dbo].[MST_QUESTION].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
    WHERE 
        (@QuestionLevelID IS NULL OR [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID)
        AND (@QuestionID IS NULL OR QuestionID = @QuestionID)
END


-- Dashboard :
CREATE OR ALTER PROCEDURE PR_Dashabord_SelecltAll
AS
BEGIN
	SELECT 
		  [dbo].[MST_QuizWiseQuestions].[QuizID],
		  [dbo].[MST_Quiz].[QuizName],
		  [dbo].[MST_QuizWiseQuestions].[QuestionID],
		  [dbo].[MST_Question].[QuestionText],
		  [dbo].[MST_Question].[QuestionMarks],
		  [dbo].[MST_Question].[CorrectOption],
		  [dbo].[MST_Quiz].[TotalQuestions],
		  [dbo].[MST_Quiz].[QuizDate]
	FROM [dbo].[MST_QuizWiseQuestions]
	JOIN [dbo].[MST_User]
	ON [dbo].[MST_User].[UserID]=[dbo].[MST_QuizWiseQuestions].[UserID]
	JOIN [dbo].[MST_Quiz]
	ON [dbo].[MST_Quiz].[QuizID]=[dbo].[MST_QuizWiseQuestions].[QuizID]
	JOIN [dbo].[MST_Question]
	ON [dbo].[MST_Question].[QuestionID]= [dbo].[MST_QuizWiseQuestions].[QuestionID]
END


-- Count :

-- Quiz :
CREATE OR ALTER PROCEDURE PR_MST_Quiz_Count
AS
BEGIN
	SELECT COUNT([dbo].[MST_Quiz].[QuizID])
	FROM [dbo].[MST_Quiz]
END


-- Question :
CREATE OR ALTER PROCEDURE PR_MST_Question_Count
AS
BEGIN
	SELECT COUNT([dbo].[MST_Question].[QuestionID])
	FROM [dbo].[MST_Question]
END


-- QuestionLevel :
CREATE OR ALTER PROCEDURE PR_MST_QuestionLevel_Count
AS
BEGIN
	SELECT COUNT([dbo].[MST_QuestionLevel].[QuestionLevelID])
	FROM [dbo].[MST_QuestionLevel]
END


-- Easy_Question :
CREATE OR ALTER PROCEDURE PR_MST_Question_CountEasy
AS
BEGIN
	SELECT COUNT([dbo].[MST_Question].[QuestionID])
	FROM [dbo].[MST_Question]
	WHERE [dbo].[MST_Question].[QuestionLevelID] = 5
END


-- Medium_Question :
CREATE OR ALTER PROCEDURE PR_MST_Question_CountMedium
AS
BEGIN
	SELECT COUNT([dbo].[MST_Question].[QuestionID])
	FROM [dbo].[MST_Question]
	WHERE [dbo].[MST_Question].[QuestionLevelID] = 1
END


-- Hard_Question :
CREATE OR ALTER PROCEDURE PR_MST_Question_CountHard
AS
BEGIN
	SELECT COUNT([dbo].[MST_Question].[QuestionID])
	FROM [dbo].[MST_Question]
	WHERE [dbo].[MST_Question].[QuestionLevelID] = 3
END


-- QuestionLevelWiseQuestionList :
CREATE OR ALTER PROCEDURE PR_MST_Question_QuestionLevelWiseSelect 
@QuestionLevelID INT
AS
BEGIN
	SELECT [dbo].[MST_Question].[QuestionId],
			   [dbo].[MST_Question].[QuestionText],
			   [dbo].[MST_Question].[QuestionLevelID],
			   [dbo].[MST_QuestionLevel].[QuestionLeveL],
			   [dbo].[MST_Question].[OptionA],
			   [dbo].[MST_Question].[OptionB],
			   [dbo].[MST_Question].[OptionC],
			   [dbo].[MST_Question].[OptionD],
			   [dbo].[MST_Question].[CorrectOption],
			   [dbo].[MST_Question].[QuestionMarks],
			   [dbo].[MST_Question].[IsActive],
			   [dbo].[MST_Question].[UserID],
			   [dbo].[MST_User].[UserName],
			   [dbo].[MST_User].[Password],
			   [dbo].[MST_User].[Email],
			   [dbo].[MST_User].[Mobile],
			   [dbo].[MST_User].[isActive],
			   [dbo].[MST_User].[isAdmin],
			   [dbo].[MST_Question].[Created],
			   [dbo].[MST_Question].[Modified]
	FROM [dbo].[MST_Question] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_Question].[UserID] = [dbo].[MST_User].[UserID]
	INNER JOIN [dbo].[MST_QuestionLevel]
	ON[dbo].[MST_QUESTION].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
	WHERE [dbo].[MST_Question].[QuestionLevelID] = @QuestionLevelID
END


