namespace TicTacToe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class winnerPropertyadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Winner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Winner");
        }
    }
}
