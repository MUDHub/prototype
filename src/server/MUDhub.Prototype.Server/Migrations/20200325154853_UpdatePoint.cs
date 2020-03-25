using Microsoft.EntityFrameworkCore.Migrations;

namespace MUDhub.Prototype.Server.Migrations
{
    public partial class UpdatePoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Point_PositionUid",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_PositionUid",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Point",
                table: "Point");

            migrationBuilder.DropColumn(
                name: "PositionUid",
                table: "Rooms");

            migrationBuilder.RenameTable(
                name: "Point",
                newName: "Rooms1");

            migrationBuilder.AddColumn<string>(
                name: "RoomId",
                table: "Rooms1",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms1",
                table: "Rooms1",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms1_RoomId",
                table: "Rooms1",
                column: "RoomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms1_Rooms_RoomId",
                table: "Rooms1",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms1_Rooms_RoomId",
                table: "Rooms1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms1",
                table: "Rooms1");

            migrationBuilder.DropIndex(
                name: "IX_Rooms1_RoomId",
                table: "Rooms1");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Rooms1");

            migrationBuilder.RenameTable(
                name: "Rooms1",
                newName: "Point");

            migrationBuilder.AddColumn<string>(
                name: "PositionUid",
                table: "Rooms",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Point",
                table: "Point",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PositionUid",
                table: "Rooms",
                column: "PositionUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Point_PositionUid",
                table: "Rooms",
                column: "PositionUid",
                principalTable: "Point",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
