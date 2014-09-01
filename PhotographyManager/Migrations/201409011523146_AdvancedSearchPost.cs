namespace PhotographyManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvancedSearchPost : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
"dbo.AdvancedSearchPhoto",
p => new
{
    name = p.String(),
    shootingPlace = p.String(),
    shootingTime = p.DateTime(),
    cameraModel = p.String(),
    diaphragm = p.String(),
    ISO = p.String(),
    shutterSpeed = p.Double(),
    focalDistance = p.Double(),
    flash = p.Boolean()

},
body:
 "SELECT * FROM [dbo].[Photo] AS t0 WHERE t0.[Name] LIKE ('%'+@name+'%') AND t0.[CameraModel] LIKE ('%'+@cameraModel+'%') AND t0.[ShootingPlace] LIKE ('%'+@shootingPlace+'%') AND t0.[ISO] LIKE ('%'+@ISO+'%') AND t0.[Diaphragm] LIKE ('%'+@diaphragm+'%') AND t0.[ShootingTime] = @shootingTime AND t0.[ShutterSpeed] = @shutterSpeed AND t0.[FocalDistance] =@focalDistance AND t0.[Flash] = @flash"

);
        }
        
        public override void Down()
        {
        }
    }
}
