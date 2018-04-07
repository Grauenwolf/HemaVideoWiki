DECLARE @Weapon TABLE
(
    WeaponKey INT NOT NULL PRIMARY KEY,
    WeaponName NVARCHAR(100) NOT NULL
);

INSERT INTO @Weapon
(
    WeaponKey,
    WeaponName
)
VALUES
(1, 'Rapier'),
(2, 'Sidesword'),
(3, 'Arming Sword'),
(4, 'Longsword'),
(5, 'Rapier and Buckler'),
(6, 'Sidesword and Buckler'),
(7, 'Arming Sword and Buckler'),
(8, 'Dagger'),
(9, 'Wrestling'),
(10, 'Rapier and Dagger'),
(12, 'Sidesword and Dagger'),
(13, 'Pike'),
(14, 'Halberd'),
(15, 'Quarterstaff'),
(16, 'Dussack'),
(17, 'Rapier and Cloak'),
(18, 'Sidesword and Cloak'),
(19, 'Partisan'),
(20, 'Greatsword'),
(21, 'Sidesword and Rotella'),
(22, 'Rapier and Rotella'),
(23, 'Partisan and Rotella');


MERGE INTO Sources.Weapon t
USING @Weapon s
ON t.WeaponKey = s.WeaponKey
WHEN NOT MATCHED THEN
    INSERT
    (
        WeaponKey,
        WeaponName
    )
    VALUES
    (s.WeaponKey, s.WeaponName)
WHEN MATCHED AND t.WeaponName = s.WeaponName THEN
    UPDATE SET WeaponName = s.WeaponName;