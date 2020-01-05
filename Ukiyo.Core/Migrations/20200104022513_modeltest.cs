using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ukiyo.Core.Migrations
{
    public partial class modeltest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Posts_Languages_CodeLanguageId",
                "Posts");

            migrationBuilder.DropForeignKey(
                "FK_Posts_User_UserId",
                "Posts");

            migrationBuilder.DropTable(
                "AuditEntryProperties");

            migrationBuilder.DropTable(
                "AuditEntries");

            migrationBuilder.DropIndex(
                "IX_Posts_CodeLanguageId",
                "Posts");

            migrationBuilder.DropIndex(
                "IX_Posts_UserId",
                "Posts");

            migrationBuilder.DropColumn(
                "CodeLanguageId",
                "Posts");

            migrationBuilder.DropColumn(
                "UserId",
                "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                "CodeLanguageId",
                "Posts",
                "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "UserId",
                "Posts",
                "text",
                nullable: true);

            migrationBuilder.CreateTable(
                "AuditEntries",
                table => new
                {
                    AuditEntryID = table.Column<int>("integer")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>("character varying(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>("timestamp without time zone"),
                    EntitySetName = table.Column<string>("character varying(255)", maxLength: 255, nullable: true),
                    EntityTypeName = table.Column<string>("character varying(255)", maxLength: 255, nullable: true),
                    State = table.Column<int>("integer"),
                    StateName = table.Column<string>("character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AuditEntries", x => x.AuditEntryID); });

            migrationBuilder.CreateTable(
                "AuditEntryProperties",
                table => new
                {
                    AuditEntryPropertyID = table.Column<int>("integer")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuditEntryID = table.Column<int>("integer"),
                    NewValue = table.Column<string>("text", nullable: true),
                    OldValue = table.Column<string>("text", nullable: true),
                    PropertyName = table.Column<string>("character varying(255)", maxLength: 255, nullable: true),
                    RelationName = table.Column<string>("character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntryProperties", x => x.AuditEntryPropertyID);
                    table.ForeignKey(
                        "FK_AuditEntryProperties_AuditEntries_AuditEntryID",
                        x => x.AuditEntryID,
                        "AuditEntries",
                        "AuditEntryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Posts_CodeLanguageId",
                "Posts",
                "CodeLanguageId");

            migrationBuilder.CreateIndex(
                "IX_Posts_UserId",
                "Posts",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AuditEntryProperties_AuditEntryID",
                "AuditEntryProperties",
                "AuditEntryID");

            migrationBuilder.AddForeignKey(
                "FK_Posts_Languages_CodeLanguageId",
                "Posts",
                "CodeLanguageId",
                "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Posts_User_UserId",
                "Posts",
                "UserId",
                "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}