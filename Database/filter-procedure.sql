CREATE PROCEDURE SelectFilteredTerms @Input nvarchar(50)
AS

BEGIN

SELECT * FROM
(
	SELECT Id, [Name] FROM 
	(
		SELECT TOP(5) t.Id, t.[Name], w.[Count]
		FROM Terms t 
		INNER JOIN Weights w ON w.TermId = t.Id AND w.Input = @Input
		ORDER BY w.[Count] DESC
	) s

	UNION 
	
	(
		SELECT TOP(5) ID, [Name] 
		FROM Terms
		WHERE CHARINDEX(@Input, [Name]) = 1
		ORDER BY [Name] ASC
	)

	UNION 

	(
		SELECT TOP(20) ID, [Name] 
		FROM Terms
		WHERE CHARINDEX(@Input, [Name]) > 1
		ORDER BY [Name] ASC
	)
) t
 
END