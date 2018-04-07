CREATE VIEW Sources.BookWeaponDetail
AS
SELECT DISTINCT
       b.BookKey,
       b.BookName,
       s.PrimaryWeaponKey,
       w1.WeaponName AS PrimaryWeaponName,
       s.SecondaryWeaponKey,
       w2.WeaponName AS SecondaryWeaponName
FROM Sources.Section s
    INNER JOIN Sources.Weapon w1
        ON w1.WeaponKey = s.PrimaryWeaponKey
    LEFT JOIN Sources.Weapon w2
        ON w2.WeaponKey = s.SecondaryWeaponKey
    INNER JOIN Sources.Book b
        ON b.BookKey = s.BookKey;