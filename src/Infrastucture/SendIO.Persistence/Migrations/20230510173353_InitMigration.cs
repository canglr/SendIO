using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SendIO.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "filehead",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    enddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filehead", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "filecontent",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    originalname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    generatedname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filecontent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_filecontent_filehead_FileHeadId",
                        column: x => x.FileHeadId,
                        principalSchema: "dbo",
                        principalTable: "filehead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_filecontent_FileHeadId",
                schema: "dbo",
                table: "filecontent",
                column: "FileHeadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "filecontent",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "filehead",
                schema: "dbo");
        }
    }
}
