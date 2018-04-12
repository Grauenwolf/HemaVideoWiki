CREATE TABLE Tags.Guard
(
    GuardKey INT NOT NULL
        CONSTRAINT PK_Guard PRIMARY KEY,
    GuardName NVARCHAR(100) NOT NULL
        CONSTRAINT UX_Guard_GuardName
        UNIQUE,
    AlternateGuardName NVARCHAR(100) NULL
);
