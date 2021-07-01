namespace Owl.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOwnerIdToParticipation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Participation", "OwnerId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Participation", "OwnerId");
        }
    }
}
