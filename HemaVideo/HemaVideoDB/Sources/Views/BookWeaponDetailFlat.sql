CREATE VIEW Sources.BookWeaponDetailFlat
AS
SELECT DISTINCT
       A.BookKey,
       A.WeaponKey,
       A.WeaponName,
	   b.BookName
FROM
(
    SELECT s.BookKey,
           w.WeaponKey,
           w.WeaponName
    FROM Sources.Section s
        INNER JOIN Sources.Weapon w
            ON w.WeaponKey = s.PrimaryWeaponKey
    UNION
    SELECT s.BookKey,
           w.WeaponKey,
           w.WeaponName
    FROM Sources.Section s
        INNER JOIN Sources.Weapon w
            ON w.WeaponKey = s.SecondaryWeaponKey
) A
INNER JOIN Sources.Book b ON b.BookKey = A.BookKey
;
