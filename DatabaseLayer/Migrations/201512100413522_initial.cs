namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountInfoes",
                c => new
                    {
                        userid = c.String(nullable: false, maxLength: 128),
                        password = c.String(),
                        firstname = c.String(),
                        lastname = c.String(),
                    })
                .PrimaryKey(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountInfoes");
        }
    }
}
