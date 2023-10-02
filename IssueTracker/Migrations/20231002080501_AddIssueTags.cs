using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IssueTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddIssueTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssueTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    Label = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueIssueTag",
                columns: table => new
                {
                    IssuesId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueIssueTag", x => new { x.IssuesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_IssueIssueTag_IssueTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "IssueTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueIssueTag_Issues_IssuesId",
                        column: x => x.IssuesId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IssueTags",
                columns: new[] { "Id", "Color", "CreatedDate", "Description", "IsDeleted", "Label", "Slug", "UpdatedDate" },
                values: new object[,]
                {
                    { -108, "light", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indicates that work won't continue on an issue, pull request, or discussion", false, "wontfix", "wontfix", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -107, "light", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indicates that an issue, pull request, or discussion needs more information", false, "question", "question", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -106, "light", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indicates that an issue, pull request, or discussion is no longer relevant", false, "invalid", "invalid", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -105, "light", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indicates that a maintainer wants help on an issue or pull request", false, "help wanted", "help-wanted", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -104, "light", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indicates a good issue for first-time contributors", false, "good first issue", "good-first-issue", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -103, "light", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indicates new feature requests", false, "enhancement", "enhancement", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -102, "light", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indicates similar issues, pull requests, or discussions", false, "duplicate", "duplicate", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -101, "light", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indicates a need for improvements or additions to documentation", false, "documentation", "documentation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -100, "light", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indicates an unexpected problem or unintended behavior", false, "bug", "bug", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueIssueTag_TagsId",
                table: "IssueIssueTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueIssueTag");

            migrationBuilder.DropTable(
                name: "IssueTags");
        }
    }
}
