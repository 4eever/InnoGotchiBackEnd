using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddLifeState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InnogotchiName",
                table: "Innogotchis",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                name: "IX_Innogotchis_InnogotchiName",
                table: "Innogotchis",
                column: "InnogotchiName",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Innogotchis_LifeStates_LifeStateId",
                table: "Innogotchis");

            migrationBuilder.DropTable(
                name: "LifeStates");

            migrationBuilder.DropIndex(
                name: "IX_Innogotchis_InnogotchiName",
                table: "Innogotchis");

            migrationBuilder.DropIndex(
                name: "IX_Innogotchis_LifeStateId",
                table: "Innogotchis");

            migrationBuilder.DropColumn(
                name: "LifeStateId",
                table: "Innogotchis");

            migrationBuilder.AlterColumn<string>(
                name: "InnogotchiName",
                table: "Innogotchis",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
