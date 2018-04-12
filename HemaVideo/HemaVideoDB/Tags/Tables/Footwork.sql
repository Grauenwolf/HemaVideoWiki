CREATE TABLE Tags.Footwork
(
    FootworkKey INT NOT NULL
        CONSTRAINT PK_Footwork PRIMARY KEY,
    FootworkName NVARCHAR(50) NOT NULL
        CONSTRAINT UK_Measure_FootworkName
        UNIQUE,
    AlternateFootworkName NVARCHAR(50) NULL
);
