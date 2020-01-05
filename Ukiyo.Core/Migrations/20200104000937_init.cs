using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ukiyo.Core.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AuditEntries",
                table => new
                {
                    AuditEntryID = table.Column<int>()
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(),
                    EntitySetName = table.Column<string>(maxLength: 255, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 255, nullable: true),
                    State = table.Column<int>(),
                    StateName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AuditEntries", x => x.AuditEntryID); });

            migrationBuilder.CreateTable(
                "Languages",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Abbreviation = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Languages", x => x.Id); });

            migrationBuilder.CreateTable(
                "User",
                table => new
                {
                    Id = table.Column<string>(),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(),
                    TwoFactorEnabled = table.Column<bool>(),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(),
                    AccessFailedCount = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_User", x => x.Id); });

            migrationBuilder.CreateTable(
                "AuditEntryProperties",
                table => new
                {
                    AuditEntryPropertyID = table.Column<int>()
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuditEntryID = table.Column<int>(),
                    PropertyName = table.Column<string>(maxLength: 255, nullable: true),
                    RelationName = table.Column<string>(maxLength: 255, nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    OldValue = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                "Comments",
                table => new
                {
                    Id = table.Column<Guid>(),
                    ParentCommentId = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        "FK_Comments_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Posts",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Description = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    CodeLanguageId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        "FK_Posts_Languages_CodeLanguageId",
                        x => x.CodeLanguageId,
                        "Languages",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Posts_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_AuditEntryProperties_AuditEntryID",
                "AuditEntryProperties",
                "AuditEntryID");

            migrationBuilder.CreateIndex(
                "IX_Comments_UserId",
                "Comments",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_Posts_CodeLanguageId",
                "Posts",
                "CodeLanguageId");

            migrationBuilder.CreateIndex(
                "IX_Posts_UserId",
                "Posts",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AuditEntryProperties");

            migrationBuilder.DropTable(
                "Comments");

            migrationBuilder.DropTable(
                "Posts");

            migrationBuilder.DropTable(
                "AuditEntries");

            migrationBuilder.DropTable(
                "Languages");

            migrationBuilder.DropTable(
                "User");
        }
    }
}