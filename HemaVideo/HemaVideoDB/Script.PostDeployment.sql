/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF
(
    SELECT COUNT(*) FROM Interpretations.VideoService
) = 0
    INSERT INTO Interpretations.VideoService
    (
        VideoServiceKey,
        VideoServiceName,
        VideoServiceUrlFormat,
        VideoServiceEmbedFormat
    )
    VALUES
    (   0,        -- VideoServiceKey - int
        'Custom', -- VideoServiceName - varchar(50)
        NULL, NULL),
    (   1,         -- VideoServiceKey - int
        'YouTube', -- VideoServiceName - varchar(50)
        NULL, NULL),
    (   2,       -- VideoServiceKey - int
        'Vimeo', -- VideoServiceName - varchar(50)
        NULL, NULL);
