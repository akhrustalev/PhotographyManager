CREATE PROCEDURE [dbo].[SearchPhotos]
    @KeyWord [nvarchar](max)
AS
BEGIN
    SELECT * FROM [dbo].[Photo] AS t0 WHERE t0.[Name] LIKE ('%'+@KeyWord+'%') OR t0.[CameraModel] LIKE ('%'+@KeyWord+'%') OR t0.[ShootingPlace] LIKE ('%'+@KeyWord+'%') OR t0.[ISO] LIKE ('%'+@KeyWord+'%') OR t0.[Diaphragm] LIKE ('%'+@KeyWord+'%')
END


