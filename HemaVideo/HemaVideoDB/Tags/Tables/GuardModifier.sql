﻿CREATE TABLE Tags.GuardModifier
(
    GuardModifierKey INT NOT NULL
        CONSTRAINT PK_GuardModifier PRIMARY KEY,
    GuardModifierName NVARCHAR(100) NOT NULL
        CONSTRAINT UX_GuardModifier_GuardModifierName
        UNIQUE,
    SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME(SysStartTime, SysEndTime)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = Tags.GuardModifier_History) );
GO
GRANT SELECT ON Tags.GuardModifier TO HemaWeb;

