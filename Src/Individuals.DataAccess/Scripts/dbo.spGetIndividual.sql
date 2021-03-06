USE [IndividualsDB]
GO
/****** Object:  StoredProcedure [dbo].[spGetIndividual]    Script Date: 20/04/2019 21:07:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetIndividual]
@Id BIGINT
AS
BEGIN
	
	SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED


	IF(OBJECT_ID('tempdb..#IndividualsResult') IS NOT NULL)
		DROP TABLE #IndividualsResult
	CREATE TABLE #IndividualsResult
	(
		Id INT,
		FirstName VARCHAR(32),
		LastName NVARCHAR(255),
		Gender INT,
		PersonalId NVARCHAR(11),
		BirthDate DATETIME,
		CityId BIGINT,
		City NVARCHAR(255),
		ImagePath NVARCHAR(MAX)
     )
	
	;WITH _Individuals AS 
	(
		SELECT I.Id,
		       I.FirstName,
		       I.LastName,
		       I.Gender,
		       I.PersonalNumber as PersonalId,
		       I.DateOfBirth as BirthDate,
		       C.Id as CityId,
		       C.Name as City,
		       I.ImagePath
		FROM dbo.Individuals I 
		INNER JOIN dbo.Cities C on I.CityId = C.Id
		WHERE I.Id = @Id
	
	)
	INSERT INTO #IndividualsResult
	SELECT Id,
	       FirstName,
	       LastName,
	       Gender,
		   PersonalId,
	       BirthDate,
	       CityId,
	       City,
		   ImagePath
	FROM _Individuals
	OPTION (RECOMPILE)	


	 IF(OBJECT_ID('tempdb..#PhoneNumbersResult') IS NOT NULL)
		DROP TABLE #PhoneNumbersResult
	CREATE TABLE #PhoneNumbersResult
	(
		PhoneNumberId BIGINT,
		PhoneNumberType INT,
		PhoneNumber NVARCHAR(255)
     )

	;WITH _PhoneNumber AS 
	(
		SELECT P.Id as PhoneNumberId,
		       P.NumberType as PhoneNumberType,
		       P.Number as PhoneNumber
		FROM dbo.PhoneNumbers P 
		INNER JOIN dbo.Individuals I on P.IndividualId = I.Id
		WHERE I.Id = @Id
	
	)
	INSERT INTO #PhoneNumbersResult
	SELECT PhoneNumberId,
	       PhoneNumberType,
	       PhoneNumber
	FROM _PhoneNumber
	OPTION (RECOMPILE)	



	 IF(OBJECT_ID('tempdb..#ConnectedIndividualsResult') IS NOT NULL)
		DROP TABLE #ConnectedIndividualsResult
	CREATE TABLE #ConnectedIndividualsResult
	(
		Id INT,
		FirstName VARCHAR(32),
		LastName NVARCHAR(255),
		Gender INT,
		PersonalId NVARCHAR(11),
		BirthDate DATETIME,
		ImagePath NVARCHAR(MAX),
		ConnectionType INT
     )

	;WITH _ConnectedIndividuals AS 
	(
		SELECT I2.Id,
		       I2.FirstName,
		       I2.LastName,
		       I2.Gender,
		       I2.PersonalNumber as PersonalId,
		       I2.DateOfBirth as BirthDate,
		       I2.ImagePath,
			   CI.ConnectionType
		FROM dbo.Individuals I
		INNER JOIN dbo.ConnectedIndividuals CI ON (I.Id = CI.ConnectedFromIndividualId OR I.Id = CI.ConnectedToIndividualId)
		INNER JOIN dbo.Individuals I2 ON ( CI.ConnectedToIndividualId = I2.Id OR CI.ConnectedFromIndividualId = I2.Id)
		where I.Id =@Id AND I2.Id <> @Id
	
	)
	INSERT INTO #ConnectedIndividualsResult
	SELECT Id,
	       FirstName,
	       LastName,
	       Gender,
	       PersonalId,
	       BirthDate,
	       ImagePath,
	       ConnectionType
	FROM _ConnectedIndividuals
	OPTION (RECOMPILE)	

	SELECT * FROM #IndividualsResult

	IF(OBJECT_ID('tempdb..#IndividualsResult') IS NOT NULL)
		DROP TABLE #IndividualsResult

    SELECT * FROM #ConnectedIndividualsResult

	IF(OBJECT_ID('tempdb..#ConnectedIndividualsResult') IS NOT NULL)
		DROP TABLE #ConnectedIndividualsResult

    SELECT * FROM #PhoneNumbersResult

	IF(OBJECT_ID('tempdb..#PhoneNumbersResult') IS NOT NULL)
		DROP TABLE #PhoneNumbersResult

END
