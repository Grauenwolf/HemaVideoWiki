DECLARE @Footwork TABLE
(
    FootworkKey INT NOT NULL,
    FootworkName NVARCHAR(50) NOT NULL
);

INSERT INTO @Footwork
(
    FootworkKey,
    FootworkName
)
VALUES
(1, 'Pass left foot forward'),
(2, 'Pass right foot forward');

MERGE INTO Tags.Footwork t
USING @Footwork s
ON t.FootworkKey = s.FootworkKey
WHEN NOT MATCHED THEN
    INSERT
    (
        FootworkKey,
        FootworkName
    )
    VALUES
    (s.FootworkKey, s.FootworkName)
WHEN MATCHED AND t.FootworkName != s.FootworkName THEN
    UPDATE SET t.FootworkName = s.FootworkName
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
