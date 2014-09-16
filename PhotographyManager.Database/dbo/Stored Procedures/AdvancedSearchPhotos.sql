CREATE PROCEDURE [dbo].[AdvancedSearchPhotos]
    @name [varchar](50),
	@shootingPlace [varchar](100),
	@cameraModel [varchar](100),
	@ISO [varchar](100),
	@diaphragm [varchar](100),
	@shotAfter [datetime],
	@shotBefore [datetime],
	@shutterSpeed [float],
	@focalDistance [float],
	@flash [bit]
AS
BEGIN
   SELECT * FROM [dbo].[Photo] AS t0 WHERE (t0.[Name] LIKE ('%'+@name+'%') OR @name LIKE '') AND (t0.[CameraModel] LIKE ('%'+@cameraModel+'%') OR @cameraModel LIKE '') AND (t0.[ShootingPlace] LIKE ('%'+@shootingPlace+'%') OR @shootingPlace LIKE '') AND (t0.[ISO] LIKE ('%'+@ISO+'%') OR @ISO LIKE '') AND (t0.[Diaphragm] LIKE ('%'+@diaphragm+'%') OR @diaphragm LIKE '') AND (t0.[ShootingTime] > @shotAfter OR @shotAfter IS NULL) AND (t0.[ShootingTime]<@shotBefore OR @shotBefore IS NULL) AND (t0.[ShutterSpeed] = @shutterSpeed OR @shutterSpeed IS NULL) AND (t0.[FocalDistance] =@focalDistance OR @focalDistance IS NULL) AND (t0.[Flash] = @flash OR @flash IS NULL)
END