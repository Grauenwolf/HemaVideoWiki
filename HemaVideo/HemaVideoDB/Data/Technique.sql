DECLARE @Technique TABLE
(
    TechniqueKey INT NOT NULL,
    TechniqueName NVARCHAR(100) NOT NULL,
    AlternateTechniqueName NVARCHAR(100) NULL
);

INSERT INTO @Technique
(
    TechniqueKey,
    TechniqueName,
    AlternateTechniqueName
)
VALUES
(1, 'Mandritto', 'Forehand cut'),
(2, 'Riverso', 'Backhand cut'),
(3, 'Fendente', 'Vertical downward cut'),
(4, 'Stoccata', 'Thrust'),
(5, 'Falso', 'False-edge cut'),
(6, 'Stramazzone', 'Write cut'),
(7, 'Montante', 'Mast cut');

MERGE INTO Tags.Technique t
USING @Technique s
ON t.TechniqueKey = s.TechniqueKey
WHEN NOT MATCHED THEN
    INSERT
    (
        TechniqueKey,
        TechniqueName
    )
    VALUES
    (s.TechniqueKey, s.TechniqueName)
WHEN MATCHED AND t.TechniqueName != s.TechniqueName THEN
    UPDATE SET t.TechniqueName = s.TechniqueName
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
