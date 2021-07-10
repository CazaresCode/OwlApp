namespace Owl.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedPersonAndMeetingConnection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Participation", "Student_Id", "dbo.Person");
            DropIndex("dbo.Participation", new[] { "Student_Id" });
            RenameColumn(table: "dbo.Meeting", name: "Persons_Id", newName: "Person_Id");
            RenameIndex(table: "dbo.Meeting", name: "IX_Persons_Id", newName: "IX_Person_Id");
            DropColumn("dbo.Participation", "Student_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Participation", "Student_Id", c => c.Int());
            RenameIndex(table: "dbo.Meeting", name: "IX_Person_Id", newName: "IX_Persons_Id");
            RenameColumn(table: "dbo.Meeting", name: "Person_Id", newName: "Persons_Id");
            CreateIndex("dbo.Participation", "Student_Id");
            AddForeignKey("dbo.Participation", "Student_Id", "dbo.Person", "Id");
        }
    }
}
