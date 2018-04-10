CREATE TABLE Tags.Technique
(
    TechniqueKey INT NOT NULL
        CONSTRAINT PK_Technique PRIMARY KEY,
    TechniqueName NVARCHAR(100) NOT NULL
        CONSTRAINT UX_Technique_TechniqueName
        UNIQUE,
		    AlternateTechniqueName NVARCHAR(100) NULL

);


