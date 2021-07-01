namespace Owl.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFullName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "FullName");
        }
    }
}
