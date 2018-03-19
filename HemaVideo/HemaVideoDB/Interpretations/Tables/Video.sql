CREATE TABLE Interpretations.Video
(
    VideoKey INT NOT NULL IDENTITY
        CONSTRAINT PK_Video PRIMARY KEY,
    SectionKey INT NOT NULL
        CONSTRAINT FK_Video_SectionKey
        REFERENCES Sources.Section (SectionKey),
    YouTubeId CHAR(11) NULL,
    Url NVARCHAR(500) NULL,
    StartTime TIME NULL,
    CreatedByUserKey INT NOT NULL
        REFERENCES dbo.AspNetUsers (UserKey),
    CreatedDate DATETIME2(7) NOT NULL
        CONSTRAINT D_Video_CreateDate
            DEFAULT (GETUTCDATE())
);
