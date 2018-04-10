CREATE TABLE Sources.SectionWeaponMap
(
	SectionWeaponMapKey INT NOT NULL IDENTITY CONSTRAINT PK_SectionWeaponMap PRIMARY KEY,
	SectionKey INT NOT NULL 
        CONSTRAINT FK_SectionWeaponMap_SectionKey
        REFERENCES Sources.Section (SectionKey),
	PrimaryWeaponKey INT NOT NULL CONSTRAINT FK_SectionWeaponMap_PrimaryWeapon REFERENCES Tags.Weapon(WeaponKey),
	SecondaryWeaponKey INT NULL CONSTRAINT FK_SectionWeaponMap_SecondaryWeapon REFERENCES Tags.Weapon(WeaponKey)
)
