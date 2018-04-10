DECLARE @Measure TABLE
(
    MeasureKey INT NOT NULL,
    MeasureName NVARCHAR(50) NOT NULL,
    AlternateMeasureName NVARCHAR(50) NOT NULL
);

INSERT INTO @Measure
(
    MeasureKey,
    MeasureName,
    AlternateMeasureName
)
VALUES
(1, 'Larga', 'Wide'),
(2, 'Stretta', 'Narrow');

MERGE INTO Tags.Measure t
USING @Measure s
ON t.MeasureKey = s.MeasureKey
WHEN NOT MATCHED THEN
    INSERT
    (
        MeasureKey,
        MeasureName,
        AlternateMeasureName
    )
    VALUES
    (s.MeasureKey, s.MeasureName, s.AlternateMeasureName)
WHEN MATCHED AND t.MeasureName != s.MeasureName
                 OR t.AlternateMeasureName != s.AlternateMeasureName THEN
    UPDATE SET MeasureName = s.MeasureName,
               AlternateMeasureName = s.AlternateMeasureName
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
