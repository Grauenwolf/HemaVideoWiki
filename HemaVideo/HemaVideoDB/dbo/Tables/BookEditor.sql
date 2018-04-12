CREATE TABLE dbo.BookEditor
(
    CONSTRAINT PK_BookEditor
        PRIMARY KEY (
                        BookKey,
                        UserKey
                    ),
    BookKey INT NOT NULL
        CONSTRAINT FK_BookEditor_Book
        REFERENCES Sources.Book (BookKey),
    UserKey INT NOT NULL
        CONSTRAINT FK_BookEditor_User
        REFERENCES dbo.AspNetUsers (UserKey)
);
