CREATE VIEW Interpretations.VideoDetail
AS
SELECT v.VideoKey,
       v.SectionKey,
       v.VideoServiceVideoId,
       v.CustomUrl,
       v.StartTime,
       v.CreatedByUserKey,
       v.CreatedDate,
       s.BookKey,
       v.VideoServiceKey,
       vs.VideoServiceName,
       vs.VideoServiceUrlFormat,
       vs.VideoServiceEmbedFormat
FROM Interpretations.Video v
    INNER JOIN Sources.Section s
        ON s.SectionKey = v.SectionKey
    INNER JOIN Interpretations.VideoService vs
        ON vs.VideoServiceKey = v.VideoServiceKey;
