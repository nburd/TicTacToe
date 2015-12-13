namespace TicTacToe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixed : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.HistoryMoves", newName: "Moves");
            AddColumn("dbo.Moves", "MoveNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Moves", "WinnerPosition", c => c.Boolean(nullable: false));
            DropColumn("dbo.Moves", "Move");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Moves", "Move", c => c.Int(nullable: false));
            DropColumn("dbo.Moves", "WinnerPosition");
            DropColumn("dbo.Moves", "MoveNumber");
            RenameTable(name: "dbo.Moves", newName: "HistoryMoves");
        }
    }
}
