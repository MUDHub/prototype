using Microsoft.EntityFrameworkCore.Migrations;

namespace MUDhub.Prototype.Server.Migrations
{
    public partial class AddRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    Uid = table.Column<string>(nullable: false),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    EnterMessage = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    PositionUid = table.Column<string>(nullable: false),
                    WestId = table.Column<string>(nullable: true),
                    NorthId = table.Column<string>(nullable: true),
                    SouthId = table.Column<string>(nullable: true),
                    EastId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Point_PositionUid",
                        column: x => x.PositionUid,
                        principalTable: "Point",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PositionUid",
                table: "Rooms",
                column: "PositionUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Point");
        }
    }
}
