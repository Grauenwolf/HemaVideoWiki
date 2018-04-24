CREATE TABLE Translations.BookTranslation
(
    BookKey INT NOT NULL
        CONSTRAINT FK_BookTranslation_Book
        REFERENCES Sources.Book (BookKey),
    TranslatorKey INT NOT NULL
        CONSTRAINT FK_BookTranslation_Translator
        REFERENCES Translations.Translator (TranslatorKey),
    Copyright NVARCHAR(MAX) NOT NULL
        CONSTRAINT C_BookTranslation_Copyright CHECK (LEN(Copyright) > 0),
    CreatedByUserKey INT NOT NULL
        REFERENCES dbo.AspNetUsers (UserKey),
    CreatedDate DATETIME2(7) NOT NULL
        CONSTRAINT D_BookTranslation_CreatedDate
            DEFAULT (GETUTCDATE()),
    ModifiedByUserKey INT NOT NULL
        REFERENCES dbo.AspNetUsers (UserKey),
    ModifiedDate DATETIME2(7) NOT NULL
        CONSTRAINT D_BookTranslation_ModifiedDate
            DEFAULT (GETUTCDATE()),
    SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME(SysStartTime, SysEndTime),
    CONSTRAINT PK_BookTranslation
        PRIMARY KEY (
                        BookKey,
                        TranslatorKey
                    )
);
