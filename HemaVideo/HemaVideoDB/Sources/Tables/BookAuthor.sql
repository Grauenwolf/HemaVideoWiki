CREATE TABLE Sources.BookAuthor
(
    BookKey INT NOT NULL 
        CONSTRAINT FK_BookAuthor_BookKey
        REFERENCES Sources.Book (BookKey),
    AuthorKey INT NOT NULL
        CONSTRAINT FK_BookAuthor_AuthorKey
        REFERENCES Sources.Author (AuthorKey),
    CONSTRAINT PK_BookAuthor
        PRIMARY KEY
        (
            BookKey,
            AuthorKey
        )
);
