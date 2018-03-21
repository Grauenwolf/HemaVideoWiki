CREATE TABLE Interpretations.Video
(
    VideoKey INT NOT NULL IDENTITY
        CONSTRAINT PK_Video PRIMARY KEY,
    SectionKey INT NOT NULL
        CONSTRAINT FK_Video_SectionKey
        REFERENCES Sources.Section (SectionKey),
    VideoServiceKey INT NOT NULL REFERENCES Interpretations.VideoService(VideoServiceKey),
    VideoServiceVideoId VARCHAR(11) NULL,
    CustomUrl NVARCHAR(500) NULL,
    StartTime TIME NULL,
    CreatedByUserKey INT NOT NULL
        REFERENCES dbo.AspNetUsers (UserKey),
    CreatedDate DATETIME2(7) NOT NULL
        CONSTRAINT D_Video_CreateDate
            DEFAULT (GETUTCDATE())
);

GO

EXEC sys.sp_addextendedproperty @name = N'MS_Description',
                                @value = N'The service-specific video key or ID',
                                @level0type = N'SCHEMA',
                                @level0name = N'Interpretations',
                                @level1type = N'TABLE',
                                @level1name = N'Video',
                                @level2type = N'COLUMN',
                                @level2name = 'VideoServiceVideoId';