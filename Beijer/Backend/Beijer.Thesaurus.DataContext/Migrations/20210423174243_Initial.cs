using Microsoft.EntityFrameworkCore.Migrations;

namespace Beijer.Thesaurus.DataContext.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Words",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Synonyms",
                schema: "dbo",
                columns: table => new
                {
                    WordId = table.Column<long>(type: "INTEGER", nullable: false),
                    SynonymWordId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synonyms", x => new { x.WordId, x.SynonymWordId });
                    table.ForeignKey(
                        name: "FK_Synonyms_Words_SynonymWordId",
                        column: x => x.SynonymWordId,
                        principalSchema: "dbo",
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Synonyms_Words_WordId",
                        column: x => x.WordId,
                        principalSchema: "dbo",
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Synonyms_SynonymWordId",
                schema: "dbo",
                table: "Synonyms",
                column: "SynonymWordId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_Content",
                schema: "dbo",
                table: "Words",
                column: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Synonyms",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Words",
                schema: "dbo");
        }
    }
}
