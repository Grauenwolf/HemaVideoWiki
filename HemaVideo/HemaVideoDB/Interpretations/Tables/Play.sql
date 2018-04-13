CREATE TABLE Interpretations.Play
(
    PlayKey INT NOT NULL IDENTITY
        CONSTRAINT PK_Play PRIMARY KEY,
    SectionKey INT NOT NULL
        REFERENCES Sources.Section (SectionKey),
    VariantName NVARCHAR(200) NULL CONSTRAINT C_Play_VariantName CHECK (LEN(VariantName) >0),
    CreatedByUserKey INT NOT NULL
        REFERENCES dbo.AspNetUsers (UserKey),
    CreatedDate DATETIME2(7) NOT NULL
        CONSTRAINT D_Play_CreatedDate
            DEFAULT (GETUTCDATE()),
    ModifiedByUserKey INT NOT NULL
        REFERENCES dbo.AspNetUsers (UserKey),
    ModifiedDate DATETIME2(7) NOT NULL
        CONSTRAINT D_Play_ModifiedDate
            DEFAULT (GETUTCDATE()),
    AGuardKey INT NULL
        CONSTRAINT FK_Play_AGuard
        REFERENCES Tags.Guard (GuardKey),
    PGuardKey INT NULL
        CONSTRAINT FK_Play_PGuard
        REFERENCES Tags.Guard (GuardKey),
    AGuardModifierKey INT NULL
        CONSTRAINT FK_Play_AGuardModifier
        REFERENCES Tags.GuardModifier (GuardModifierKey),
    PGuardModifierKey INT NULL
        CONSTRAINT FK_Play_PGuardModifier
        REFERENCES Tags.GuardModifier (GuardModifierKey),
    MeasureKey INT NULL
        CONSTRAINT FK_Play_Measure
        REFERENCES Tags.Measure (MeasureKey),
);
