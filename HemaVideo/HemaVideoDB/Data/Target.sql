DECLARE @Target TABLE
(
    TargetKey INT NOT NULL,
    TargetName NVARCHAR(50) NOT NULL
);

INSERT INTO @Target
(
    TargetKey,
    TargetName
)
VALUES
(1, 'Head'),
(2, 'Hands'),
(3, 'Rigth arm'),
(4, 'Left arm'),
(5, 'Legs'),
(14, 'Left Leg'),
(6, 'Right Leg'),
(7, 'Arms'),
(8, 'Chest'),
(9, 'Face'),
(10, 'Left Ear'),
(11, 'Right Ear'),
(12, 'Left Thigh'),
(13, 'Right Thigh');

MERGE INTO Tags.Target t
USING @Target s
ON t.TargetKey = s.TargetKey
WHEN NOT MATCHED THEN
    INSERT
    (
        TargetKey,
        TargetName
    )
    VALUES
    (s.TargetKey, s.TargetName)
WHEN MATCHED AND t.TargetName != s.TargetName THEN
    UPDATE SET t.TargetName = s.TargetName
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
