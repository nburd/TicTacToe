namespace TicTacToe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoryMoves", "Move", c => c.Int(nullable: false));
            AddColumn("dbo.HistoryMoves", "XorO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HistoryMoves", "XorO");
            DropColumn("dbo.HistoryMoves", "Move");
        }
    }
}
