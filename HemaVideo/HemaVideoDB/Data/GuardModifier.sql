﻿DECLARE @GuardModifier TABLE
(
    GuardModifierKey INT NOT NULL,
    GuardModifierName NVARCHAR(50) NOT NULL
);

INSERT INTO @GuardModifier
(
    GuardModifierKey,
    GuardModifierName
)
VALUES
(1, 'Rigth foot forward'),
(2, 'Left foot forward'),
(3, 'Narrow stance'),
(4, 'Wide stance'),
(5, 'Inside'),
(6, 'Outside');


MERGE INTO Tags.GuardModifier t
USING @GuardModifier s
ON t.GuardModifierKey = s.GuardModifierKey
WHEN NOT MATCHED THEN
    INSERT
    (
        GuardModifierKey,
        GuardModifierName
    )
    VALUES
    (s.GuardModifierKey, s.GuardModifierName)
WHEN MATCHED AND t.GuardModifierName != s.GuardModifierName THEN
    UPDATE SET t.GuardModifierName = s.GuardModifierName
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
