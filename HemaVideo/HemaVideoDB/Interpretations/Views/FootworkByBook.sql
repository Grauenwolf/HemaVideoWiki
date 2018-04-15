CREATE VIEW Interpretations.FootworkByBook
AS
SELECT A.BookKey,
       COUNT(A.PlayKey) AS PlayCount,
       A.[Key],
       A.Name,
       NULL AS AlternateName
FROM
(
    SELECT pd.BookKey,
           psd.PlayKey,
           psd.FootworkKey AS [Key],
           psd.FootworkName AS Name
    FROM Interpretations.PlayStepDetail psd
        INNER JOIN Interpretations.PlayDetail pd
            ON psd.PlayKey = pd.PlayKey
    WHERE psd.FootworkKey IS NOT NULL
) A
GROUP BY A.BookKey,
		 A.[Key],
         A.Name;
		 GO
GRANT SELECT ON Interpretations.FootworkByBook TO HemaWeb;