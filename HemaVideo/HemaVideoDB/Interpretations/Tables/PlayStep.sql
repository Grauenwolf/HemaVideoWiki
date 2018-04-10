﻿CREATE TABLE [dbo].[PlayStep]
(
	PlayKey INT NOT NULL ,
	TempoNumber INT NOT NULL,
	Actor CHAR(1) NOT NULL CONSTRAINT C_PlayStep_Actor CHECK (Actor IN ('A', 'P')),
	CONSTRAINT PK_PlayStep PRIMARY KEY (PlayKey, TempoNumber, Actor),
	FootworkKey INT NULL CONSTRAINT FK_PlayStep_Footwork REFERENCES Tags.Footwork(FootworkKey),
	TechniqueKey1 INT NULL CONSTRAINT FK_PlayStep_TechniqueKey1 REFERENCES Tags.Technique(TechniqueKey),
	TechniqueKey2 INT NULL CONSTRAINT FK_PlayStep_TechniqueKey2 REFERENCES Tags.Technique(TechniqueKey),
	TechniqueKey3 INT NULL CONSTRAINT FK_PlayStep_TechniqueKey3 REFERENCES Tags.Technique(TechniqueKey),
	Target1 INT NOT NULL CONSTRAINT FK_PlayStep_Target1 REFERENCES Tags.Target(TargetKey),
	Target2 INT NOT NULL CONSTRAINT FK_PlayStep_Target2 REFERENCES Tags.Target(TargetKey),
	Target3 INT NOT NULL CONSTRAINT FK_PlayStep_Target3 REFERENCES Tags.Target(TargetKey),
	GuardKey INT NOT NULL CONSTRAINT FK_PlayStep_Guard REFERENCES Tags.Guard(GuardKey),
	GuardModifierKey INT NOT NULL CONSTRAINT FK_PlayStep_GuardModifier REFERENCES Tags.GuardModifier(GuardModifierKey),
	IntermediateGuardKey INT NOT NULL CONSTRAINT FK_PlayStep_IntermediateGuard REFERENCES Tags.Guard(GuardKey),
	IntermediateGuardModifierKey INT NOT NULL CONSTRAINT FK_PlayStep_IntermediateGuardModifier REFERENCES Tags.GuardModifier(GuardModifierKey),
	Notes NVARCHAR(max) NULL
)
