﻿CREATE TABLE Translations.SectionTranslation
(
    SectionKey INT NOT NULL
        CONSTRAINT FK_SectionTranslation_Section
        REFERENCES Sources.Section (SectionKey),
    TranslatorKey INT NOT NULL
        CONSTRAINT FK_SectionTranslation_Translator
        REFERENCES Translations.Translator (TranslatorKey),
    Translation NVARCHAR(MAX) NOT NULL
        CONSTRAINT C_SectionTranslation_Translation CHECK (LEN(Translation) > 0),
    CreatedByUserKey INT NOT NULL
        REFERENCES dbo.AspNetUsers (UserKey),
    CreatedDate DATETIME2(7) NOT NULL
        CONSTRAINT D_SectionTranslation_CreatedDate
            DEFAULT (GETUTCDATE()),
    ModifiedByUserKey INT NOT NULL
        REFERENCES dbo.AspNetUsers (UserKey),
    ModifiedDate DATETIME2(7) NOT NULL
        CONSTRAINT D_SectionTranslation_ModifiedDate
            DEFAULT (GETUTCDATE()),
    SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME(SysStartTime, SysEndTime),
    CONSTRAINT PK_SectionTranslation
        PRIMARY KEY (
                        SectionKey,
                        TranslatorKey
                    )
);