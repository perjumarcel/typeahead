CREATE TABLE Terms
(
    Id INT PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR (50) NOT NULL, -- biggest world word length is 45
    CONSTRAINT Terms_Name_CK CHECK (LEN([Name]) >= 3), -- as the search input requires minimun length of 3, having terms shorter then it is useless as it will not be displayed.
    CONSTRAINT AK_Name UNIQUE([Name])
)

CREATE TABLE Weights
(
    Input  NVARCHAR(50) NOT NULL,
    TermId INT FOREIGN KEY REFERENCES Terms(Id),
    Count  INT NOT NULL DEFAULT 1,
    CONSTRAINT Weights_PK PRIMARY KEY (Input, TermId),
    CONSTRAINT Weights_Input_CK CHECK (LEN(Input) >= 3) 
)