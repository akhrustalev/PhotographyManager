namespace PhotographyManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance : DbMigration
    {
        public override void Up()
        {
            AddColumn("[dbo.User]","Discriminator", c => c.String(nullable:false));
        }
        
        public override void Down()
        {
            DropColumn("[dbo.User]", "Discriminator");
        }
    }
}
