CREATE TABLE Sources.Book
(
    BookKey INT NOT NULL
        CONSTRAINT PK_Book PRIMARY KEY,
    BookName NVARCHAR(500) NOT NULL
        CONSTRAINT UX_Book_BookName
        UNIQUE,
    AlternateBookName NVARCHAR(500) NULL,
    BookSlug CHAR(50) NOT NULL
        CONSTRAINT UX_Book_BookSlug
        UNIQUE
);
