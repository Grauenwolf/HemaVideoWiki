CREATE TABLE Tags.Weapon
(
    WeaponKey INT NOT NULL
        CONSTRAINT PK_Weapon PRIMARY KEY,
    WeaponName NVARCHAR(100) NOT NULL
        CONSTRAINT UX_Weapon_WeaponName
        UNIQUE
);
