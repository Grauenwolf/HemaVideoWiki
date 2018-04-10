CREATE TABLE Tags.GuardModifier
(
    GuardModifierKey INT NOT NULL
        CONSTRAINT PK_GuardModifier PRIMARY KEY,
    GuardModifierName NVARCHAR(100) NOT NULL
        CONSTRAINT UX_GuardModifier_GuardModifierName
        UNIQUE
);
