DECLARE @Guard TABLE
(
    GuardKey INT NOT NULL,
    GuardName NVARCHAR(100) NOT NULL,
    AlternateGuardName NVARCHAR(100) NOT NULL
);

INSERT INTO @Guard
(
    GuardKey,
    GuardName,
    AlternateGuardName
)
VALUES
(1, 'Guardia Alta', 'High Guard'),
(2, 'Guardia di Testa', 'Head Guard'),
(3, 'Guardia di Faccia', 'Face Guard'),
(4, 'Guardia di Sopra il Braccio', 'Overarm Guard'),
(5, 'Guardia di Soto il Braccio', 'Underarm Guard'),
(6, 'Guardia di Porta di Fero Stretta', 'Narrow Iron Gate'),
(7, 'Guardia di Porta di Fero Larga', 'Wide Iron Gate'),
(8, 'Chinghiara Porta di Fero', 'Wild Boar Iron Gate'),
(9, 'Coda Lunga e Alta', 'Long and High Tail'),
(10, 'Coda Lunga e Stretta', 'Long and Narrow Tail');

MERGE INTO Tags.Guard t
USING @Guard s
ON t.GuardKey = s.GuardKey
WHEN NOT MATCHED THEN
    INSERT
    (
        GuardKey,
        GuardName,
        AlternateGuardName
    )
    VALUES
    (s.GuardKey, s.GuardName, s.AlternateGuardName)
WHEN MATCHED AND t.GuardName != s.GuardName
                 OR t.AlternateGuardName != s.AlternateGuardName THEN
    UPDATE SET GuardName = s.GuardName,
               AlternateGuardName = s.AlternateGuardName
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
