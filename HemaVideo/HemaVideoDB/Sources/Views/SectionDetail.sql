CREATE VIEW Sources.SectionDetail
AS
SELECT s.SectionKey,
       s.BookKey,
       s.ParentSectionKey,
       s.SectionName,
       s.PageReference,
       s.DisplayOrder,
       b.BookName,
       (
           SELECT COUNT(*)
           FROM Interpretations.Video v
           WHERE v.SectionKey = s.SectionKey
       ) AS VideoCount
FROM Sources.Section s
    INNER JOIN Sources.Book b
        ON b.BookKey = s.BookKey;
