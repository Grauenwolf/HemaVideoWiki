
DECLARE @BookKey INT = 2;



/***** Book *****/
DECLARE @Book TABLE
(
    BookKey INT NOT NULL,
    BookName NVARCHAR(500) NOT NULL,
    AlternateBookName NVARCHAR(500) NOT NULL,
    Slug CHAR(50) NOT NULL
);

INSERT INTO @Book
(
    BookKey,
    BookName,
    AlternateBookName,
    Slug
)
VALUES
(@BookKey, N'MS I.33', N'Walpurgis Fechtbuch', 'MSI.33');

MERGE INTO Sources.Book t
USING @Book s
ON t.BookKey = s.BookKey
WHEN NOT MATCHED THEN
    INSERT
    (
        BookKey,
        BookName,
        AlternateBookName,
        BookSlug
    )
    VALUES
    (s.BookKey, s.BookName, s.AlternateBookName, s.Slug)
WHEN MATCHED AND s.BookName <> t.BookName
                 OR s.AlternateBookName <> t.AlternateBookName
                 OR s.Slug <> t.BookSlug THEN
    UPDATE SET t.BookName = s.BookName,
               t.AlternateBookName = s.AlternateBookName,
               t.BookSlug = s.Slug;

DECLARE @AlternateBookName TABLE
(
    BookKey INT NOT NULL,
    AlternateBookName NVARCHAR(500) NOT NULL
);

INSERT INTO @AlternateBookName
(
    BookKey,
    AlternateBookName
)
VALUES
(@BookKey, N'Walpurgis Fechtbuch'),
(@BookKey, N'Liber de Arte Dimicatoria '),
(@BookKey, N'The Tower Fechtbuch');


MERGE INTO Sources.AlternateBookName t
USING @AlternateBookName s
ON s.BookKey = t.BookKey
   AND s.AlternateBookName = t.AlternateBookName
WHEN NOT MATCHED THEN
    INSERT
    (
        BookKey,
        AlternateBookName
    )
    VALUES
    (s.BookKey, s.AlternateBookName)
WHEN NOT MATCHED BY SOURCE AND t.BookKey = @BookKey THEN
    DELETE;

/**** AUTHOR *******/
DECLARE @Author TABLE
(
    AuthorKey INT NOT NULL,
    AuthorName NVARCHAR(500) NOT NULL,
    AuthorSlug CHAR(50) NOT NULL
);

INSERT INTO @Author
(
    AuthorKey,
    AuthorName,
    AuthorSlug
)
VALUES
(2, N'Clerus Lutegerus', 'Lutegerus');



MERGE INTO Sources.Author t
USING @Author s
ON s.AuthorKey = t.AuthorKey
WHEN NOT MATCHED THEN
    INSERT
    (
        AuthorKey,
        AuthorName,
        AuthorSlug
    )
    VALUES
    (s.AuthorKey, s.AuthorName, s.AuthorSlug)
WHEN MATCHED AND t.AuthorName <> s.AuthorName THEN
    UPDATE SET t.AuthorName = s.AuthorName,
               t.AuthorSlug = s.AuthorSlug;

DECLARE @BookAuthor TABLE
(
    BookKey INT NOT NULL,
    AuthorKey INT NOT NULL
);

INSERT INTO @BookAuthor
(
    BookKey,
    AuthorKey
)
VALUES
(   @BookKey, -- BookKey - int
    1         -- AuthorKey - int
    );

MERGE INTO Sources.BookAuthor t
USING @BookAuthor s
ON s.BookKey = t.BookKey
   AND s.AuthorKey = t.AuthorKey
WHEN NOT MATCHED THEN
    INSERT
    (
        BookKey,
        AuthorKey
    )
    VALUES
    (s.BookKey, s.AuthorKey);

/************ SECTIONS *************/
DECLARE @Section TABLE
(
    SectionKey INT NOT NULL PRIMARY KEY,
    BookKey INT NOT NULL,
    ParentSectionKey INT NULL,
    SectionName NVARCHAR(250) NOT NULL,
    PageReference NVARCHAR(50) NULL,
    DisplayOrder FLOAT NOT NULL
);

INSERT INTO @Section
(
    SectionKey,
    BookKey,
    ParentSectionKey,
    SectionName,
    PageReference,
    DisplayOrder
)
VALUES
(1001, 2, NULL, 'The 7 Wards - 1r-1v', NULL, 1),
(1002, 2, NULL, '1st Ward - Underarm', NULL, 2),
(1003, 2, 1002, '1st Ward - Halfshield - Fall Under, Overbind and Schiltslac', '2r-2v', 1),
(1004, 2, 1002, '1st Ward - Halfshield - Fall Under - Overbind - Mutate and Nucken', '3r-4r', 2),
(1005, 2, 1002, '1st Ward - Krucke - Overbind - Grapple the Arms - Schiltslac Counter', '4r-5r', 3),
(1006, 2, 1002, 'Krucke - 1st Ward - Enter with Thrust', '5r-5v', 4),
(1007, 2, 1002, 'Krucke - Krucke Bind - Thrust and Mind the Head ', '5v-6v', 5),
(1008, 2, 1002, '1st Ward - Langort Obsessio - Overbind and Schiltslac', '6v-7r', 6),
(1009, 2, 1002, '1st Ward - Langort Obsessio - Underbind, Mutate, and Nucken', '7v-8r', 7),
(1010, 2, NULL, '2nd Ward - Right Shoulder', NULL, 3),
(1011, 2, 1010, '2nd Ward - Schutzen', '2nd Ward', 1),
(1012, 2, 1010, 'Schutzen', '2nd Ward', 2),
(1013, 2, 1010, '2nd Ward - Halfshield - Cut to Separate Sword and Shield - Stichslac Counter', '10v-11r', 3),
(1014, 2, 1010, 'Halfshield - 1st Ward - Enter with Strike', '11v', 4),
(1015, 2, NULL, '3rd Ward - Left Shoulder', NULL, 4),
(1016, 2, 1015, '3rd Ward - Schutzen', '3rd Ward', 1),
(1017, 2, 1015, 'Schutzen', '3rd Ward', 2),
(1018, 2, 1015, '3rd Ward - Halfshield - Fall Under Sword and Shield - Overbind and Schiltslac', '13r-14r', 3),
(1019, 2, 1015, '3rd Ward - Langort Obsessio - Overbind etc', '14r-14v', 4),
(1020, 2, NULL, '4th Ward - Overhead', NULL, 5),
(1021, 2, 1020, '4th Ward - Halfshield - Fall Under Sword and Shield - Overbind and Schiltslac', '14v', 1),
(1022, 2, 1020, '4th Ward - 1st Ward Obsessio - Halfshield - Fall under Sword and Shield - Overbind and Schiltslac',
 '15r-16r', 2),
(1023, 2, 1020, '1st Ward - Langort Obsessio - Grab Sword - Schiltslac to Hand - Defend the Head', '16r-16v', 3),
(1024, 2, NULL, '6th Ward - Breast', NULL, 6),
(1025, 2, 1024, '6th Ward - Halfshield - Thrust to the Left - Overbind etc', '17r', 1),
(1026, 2, NULL, '7th Ward - Langort (Longpoint)', NULL, 7),
(1027, 2, 1026, '7th Ward - Overbind Right - Schutzen - Schiltslac Counter', '17v-18r', 1),
(1028, 2, 1026, '7th Ward - Overbind Right - Arm Grapple - Kick Counter - pull back to 6th Ward', '18v-19r', 2),
(1029, 2, 1026, '7th Ward - Underbind Left - Strike to Head - Counter', '19r-19v', 3),
(1030, 2, 1026, '7th Ward - Overbind Left - Thrust to the left', '20r', 4),
(1031, 2, 1026, '7th Ward - Overbind Right - Flee to the Side - Follows with Cut to Head', '20v', 5),
(1032, 2, 1026, 'Upper langort - Bind to the Left - Overbind Right - Thrust Underneath - Cover - Thrust to the Right',
 '21r-21v', 6),
(1033, 2, 1026, 'Vidilpoge - Bind on the Arm - Turn the Hand and take their Sword', '22r', 7),
(1034, 2, 1026, 'Vidilpoge - Bind on the Arm - Overbind Right, Schiltslac', '22v-23r', 8),
(1035, 2, 1026, '7th Ward - Overbind Left, Thrust Underneath', '23r', 9),
(1036, 2, NULL, 'Priest Special Longpoint (PSL)', NULL, 8),
(1037, 2, 1036, 'Halfshield - PSL - Fall Under Sword and Shield - Overbind and Separate Sword and Shield', '23v-24r', 1),
(1038, 2, 1036, 'PSL - Halfshield - Fall Under Sword and Shield - Strikes Without Schiltslac - Thrust Counter',
 '24v-25r', 2),
(1039, 2, 1036, 'PSL - Rare Halfshield - Thrust Left - Overbind etc', '25r-26r', 3),
(1040, 2, 1036, 'PSL Obsessio - 3rd Ward - Schutzen', '3rd Ward', 4),
(1041, 2, 1036, '4th Ward - PSL Obsessio - Schutzen - Overbind', '26v', 5),
(1042, 2, 1036, '5th Ward - PSL Obsessio - Overbind Right etc', '27r', 6),
(1043, 2, NULL, '5th Ward - Right Side', NULL, 9),
(1044, 2, 1043, '5th Ward - Halfshield - Thrust Left - Schiltslac Counter', '27v-28r', 1),
(1045, 2, 1043, '5th Ward - Halfshield Obsessio - Enters with Thrust - Schiltslac Counter', '28r-29r', 2),
(1046, 2, 1043, '5th Ward - Obsessio Quandam Rara - Enters with Thrust - Counter Thrust to Left', '29r-29v', 3),
(1047, 2, NULL, 'Misc.', NULL, 10),
(1048, 2, 1047, '4th Ward - PSL Obsessio - Halfshield - Overbind Right, Schiltslac', '30r-30v', 1),
(1049, 2, 1047, 'PSL Obsessio - 4th Ward - Schutzen and enter with Thrust', '31r-31v', 2),
(1050, 2, 1047, '1st Ward - Walpurgis Obsessio - Schutzen (Walpurgis) - Overbind Right, Schiltslac', '32r-32v', 3);


MERGE INTO Sources.Section t
USING @Section s
ON t.SectionKey = s.SectionKey
WHEN NOT MATCHED THEN
    INSERT
    (
        SectionKey,
        BookKey,
        ParentSectionKey,
        SectionName,
        DisplayOrder,
        PageReference
    )
    VALUES
    (s.SectionKey, s.BookKey, s.ParentSectionKey, s.SectionName, s.DisplayOrder, s.PageReference)
WHEN MATCHED THEN
    UPDATE SET t.BookKey = s.BookKey,
               t.ParentSectionKey = s.ParentSectionKey,
               t.SectionName = s.SectionName,
               t.PageReference = s.PageReference,
               t.DisplayOrder = s.DisplayOrder
WHEN NOT MATCHED BY SOURCE AND t.BookKey = @BookKey THEN
    DELETE;


DECLARE @Video TABLE
(
    SectionKey INT NOT NULL,
    VideoServiceKey INT NOT NULL,
    VideoServiceVideoId VARCHAR(11) NULL,
    CreatedByUserKey INT NOT NULL
        DEFAULT (1)
);

/********** VIDEOS *************/

INSERT INTO @Video
(
    SectionKey,
    VideoServiceKey,
    VideoServiceVideoId
)
VALUES
(1001, 1, 'tt27o4ry0y0'),
(1003, 1, 'FTZiAfC16NM'),
(1003, 1, 'SKyNyYnQZW4'),
(1003, 1, 'jxonUvrHw9M'),
(1003, 1, '8lZkmob8p9k'),
(1003, 1, 'VFIXhcUEIEU'),
(1003, 1, 'otU-sQhwsA4'),
(1003, 1, 'TcgzUFQYujY'),
(1004, 1, 'jxonUvrHw9M'),
(1004, 1, '9H__2ehaXbw'),
(1005, 1, 'jxonUvrHw9M'),
(1006, 1, 'jxonUvrHw9M'),
(1011, 1, '1or1ErXImBU'),
(1011, 1, 'qNzjrNNDF3s'),
(1017, 1, 'k8D1rXp8MFg'),
(1048, 1, 'zLCIMG42e98'),
(1049, 1, 'GX616ru3seE');

MERGE INTO Interpretations.Video t
USING @Video s
ON s.SectionKey = t.SectionKey
   AND s.VideoServiceKey = t.VideoServiceKey
   AND s.VideoServiceVideoId = t.VideoServiceVideoId
WHEN NOT MATCHED THEN
    INSERT
    (
        SectionKey,
        VideoServiceKey,
        VideoServiceVideoId,
        CustomUrl,
        StartTime,
        CreatedByUserKey
    )
    VALUES
    (s.SectionKey, s.VideoServiceKey, s.VideoServiceVideoId, NULL, NULL, s.CreatedByUserKey);
