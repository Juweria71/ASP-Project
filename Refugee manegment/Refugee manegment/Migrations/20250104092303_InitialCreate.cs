using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Refugee_manegment.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "refugee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refugee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefugeeId = table.Column<int>(type: "int", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employements_refugee_RefugeeId",
                        column: x => x.RefugeeId,
                        principalTable: "refugee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthyRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefugeeId = table.Column<int>(type: "int", nullable: false),
                    MedicalCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfRecord = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TreatmentDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthyRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthyRecords_refugee_RefugeeId",
                        column: x => x.RefugeeId,
                        principalTable: "refugee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sponsorships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefugeeId = table.Column<int>(type: "int", nullable: false),
                    SponsorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SponsorContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SponsorshipStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SponsorshipEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsorships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sponsorships_refugee_RefugeeId",
                        column: x => x.RefugeeId,
                        principalTable: "refugee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employements_RefugeeId",
                table: "Employements",
                column: "RefugeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthyRecords_RefugeeId",
                table: "HealthyRecords",
                column: "RefugeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsorships_RefugeeId",
                table: "Sponsorships",
                column: "RefugeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employements");

            migrationBuilder.DropTable(
                name: "HealthyRecords");

            migrationBuilder.DropTable(
                name: "Sponsorships");

            migrationBuilder.DropTable(
                name: "refugee");
        }
    }
}
