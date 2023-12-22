using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddDeadInnogotchies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Innogotchis_LifeStates_LifeStateId",
                table: "Innogotchis");

            migrationBuilder.DropTable(
                name: "LifeStates");

            migrationBuilder.DropIndex(
                name: "IX_Innogotchis_LifeStateId",
                table: "Innogotchis");

            migrationBuilder.DropColumn(
                name: "LifeStateId",
                table: "Innogotchis");

            migrationBuilder.CreateTable(
                name: "DeadInnogotchis",
                columns: table => new
                {
                    DeadInnogotchiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeadInnogotchiName = table.Column<int>(type: "int", nullable: false),
                    DeadInnogotchiAge = table.Column<int>(type: "int", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeadInnogotchis", x => x.DeadInnogotchiId);
                    table.ForeignKey(
                        name: "FK_DeadInnogotchis_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "FarmId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeadInnogotchis_FarmId",
                table: "DeadInnogotchis",
                column: "FarmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeadInnogotchis");

            migrationBuilder.AddColumn<int>(
                name: "LifeStateId",
                table: "Innogotchis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LifeStates",
                columns: table => new
                {
                    LifeStateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LifeStateName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeStates", x => x.LifeStateId);
                });

            migrationBuilder.InsertData(
                table: "LifeStates",
                columns: new[] { "LifeStateId", "LifeStateName" },
                values: new object[,]
                {
                    { 1, "Alive" },
                    { 2, "Dead" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Innogotchis_LifeStateId",
                table: "Innogotchis",
                column: "LifeStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Innogotchis_LifeStates_LifeStateId",
                table: "Innogotchis",
                column: "LifeStateId",
                principalTable: "LifeStates",
                principalColumn: "LifeStateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
