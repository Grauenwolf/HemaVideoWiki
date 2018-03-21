CREATE VIEW Interpretations.VideoDetail
AS
SELECT v.VideoKey,
       v.SectionKey,
       v.YouTubeId,
       v.Url,
       v.StartTime,
       v.CreatedByUserKey,
       v.CreatedDate,
       s.BookKey
FROM Interpretations.Video v
    INNER JOIN Sources.Section s
        ON s.SectionKey = v.SectionKey;
