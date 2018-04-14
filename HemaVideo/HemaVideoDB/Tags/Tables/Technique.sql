﻿CREATE TABLE Tags.Technique
(
    TechniqueKey INT NOT NULL
        CONSTRAINT PK_Technique PRIMARY KEY,
    TechniqueName NVARCHAR(100) NOT NULL
        CONSTRAINT UX_Technique_TechniqueName
        UNIQUE,
		    AlternateTechniqueName NVARCHAR(100) NULL,
    SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME(SysStartTime, SysEndTime)

)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = Tags.Technique_History) );
