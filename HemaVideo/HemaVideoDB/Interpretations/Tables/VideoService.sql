CREATE TABLE Interpretations.VideoService
(
    VideoServiceKey INT NOT NULL PRIMARY KEY,
    VideoServiceName VARCHAR(50) NOT NULL,
    VideoServiceUrlFormat VARCHAR(500) NULL,
    VideoServiceEmbedFormat VARCHAR(500) NULL
);
