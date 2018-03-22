CREATE TABLE Sources.AlternateBookName
(
    AlternateBookNameKey INT NOT NULL IDENTITY
        CONSTRAINT PK_AlternateBookName PRIMARY KEY,
    BookKey INT NOT NULL
        CONSTRAINT FK_AlternateBookName_BookKey
        REFERENCES Sources.Book (BookKey),
    AlternateBookName NVARCHAR(500) NOT NULL,
	CONSTRAINT UX_AlternateBookName UNIQUE (BookKey, AlternateBookName)
);
