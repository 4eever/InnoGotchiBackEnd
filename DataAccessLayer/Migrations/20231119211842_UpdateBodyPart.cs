using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBodyPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_innogotchiBodyParts_bodyParts_BodyPartId",
                table: "innogotchiBodyParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_innogotchiBodyParts",
                table: "innogotchiBodyParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bodyParts",
                table: "bodyParts");

            migrationBuilder.RenameTable(
                name: "innogotchiBodyParts",
                newName: "InnogotchiBodyParts");

            migrationBuilder.RenameTable(
                name: "bodyParts",
                newName: "BodyParts");

            migrationBuilder.RenameIndex(
                name: "IX_innogotchiBodyParts_BodyPartId",
                table: "InnogotchiBodyParts",
                newName: "IX_InnogotchiBodyParts_BodyPartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InnogotchiBodyParts",
                table: "InnogotchiBodyParts",
                column: "InnogotchiBodyPartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyParts",
                table: "BodyParts",
                column: "BodyPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_InnogotchiBodyParts_BodyParts_BodyPartId",
                table: "InnogotchiBodyParts",
                column: "BodyPartId",
                principalTable: "BodyParts",
                principalColumn: "BodyPartId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InnogotchiBodyParts_BodyParts_BodyPartId",
                table: "InnogotchiBodyParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InnogotchiBodyParts",
                table: "InnogotchiBodyParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyParts",
                table: "BodyParts");

            migrationBuilder.RenameTable(
                name: "InnogotchiBodyParts",
                newName: "innogotchiBodyParts");

            migrationBuilder.RenameTable(
                name: "BodyParts",
                newName: "bodyParts");

            migrationBuilder.RenameIndex(
                name: "IX_InnogotchiBodyParts_BodyPartId",
                table: "innogotchiBodyParts",
                newName: "IX_innogotchiBodyParts_BodyPartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_innogotchiBodyParts",
                table: "innogotchiBodyParts",
                column: "InnogotchiBodyPartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bodyParts",
                table: "bodyParts",
                column: "BodyPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_innogotchiBodyParts_bodyParts_BodyPartId",
                table: "innogotchiBodyParts",
                column: "BodyPartId",
                principalTable: "bodyParts",
                principalColumn: "BodyPartId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
