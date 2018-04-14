﻿CREATE TABLE Sources.Book
(
    BookKey INT NOT NULL
        CONSTRAINT PK_Book PRIMARY KEY,
    BookName NVARCHAR(500) NOT NULL
        CONSTRAINT UX_Book_BookName
        UNIQUE,
    AlternateBookName NVARCHAR(500) NULL,
    BookSlug CHAR(50) NOT NULL
        CONSTRAINT UX_Book_BookSlug
        UNIQUE,
    SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME(SysStartTime, SysEndTime)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = Sources.Book_History) );
