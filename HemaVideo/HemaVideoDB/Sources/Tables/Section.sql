CREATE TABLE Sources.Section
(
    SectionKey INT NOT NULL IDENTITY PRIMARY KEY,
    BookKey INT NOT NULL
        CONSTRAINT FK_Section_BookKey
        REFERENCES Sources.Book (BookKey),
    ParentSectionKey INT NULL
        CONSTRAINT FK_SectionKey_ParentSectionKey
        REFERENCES Sources.Section (SectionKey)
);
