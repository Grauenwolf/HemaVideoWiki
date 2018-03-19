CREATE TABLE Sources.Author
(
    AuthorKey INT NOT NULL 
        CONSTRAINT PK_Author PRIMARY KEY,
    AuthorName NVARCHAR(500) NOT NULL,
    CONSTRAINT UX_Author_AuthorName
        UNIQUE (AuthorName)
);
