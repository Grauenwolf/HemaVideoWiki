CREATE TABLE Tags.Target
(
    TargetKey INT NOT NULL
        CONSTRAINT PK_Target PRIMARY KEY,
    TargetName NVARCHAR(50) NOT NULL
        CONSTRAINT UK_Measure_TargetName
        UNIQUE
);
