CREATE PROCEDURE IncreaseTermWeight @TermId int, @Input nvarchar(50)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Weights
        WHERE TermId = @TermId 
			AND Input = @Input
    )
		BEGIN
			UPDATE Weights set[Count] = [Count] + 1
			WHERE TermId = @TermId 
				AND Input = @Input
		END
	ELSE
		BEGIN
            IF EXISTS (SELECT 1 FROM Terms
                WHERE Id = @TermId 
                    AND CHARINDEX(@Input, [Name]) > 0
            )
            BEGIN
			    INSERT INTO Weights (TermId, Input, [Count]) VALUES(@TermId, @Input, 1);
            END
		END
END