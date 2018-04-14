﻿CREATE TABLE Tags.Footwork
(
    FootworkKey INT NOT NULL
        CONSTRAINT PK_Footwork PRIMARY KEY,
    FootworkName NVARCHAR(50) NOT NULL
        CONSTRAINT UK_Measure_FootworkName
        UNIQUE,
    AlternateFootworkName NVARCHAR(50) NULL,
    SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME(SysStartTime, SysEndTime)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = Tags.Footwork_History));
GO
GRANT SELECT ON Tags.Footwork TO HemaWeb;


