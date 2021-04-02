using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HospitalData.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Organs",
				columns: table => new
				{
					OrganId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
					IsAgeChecked = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Organs", x => x.OrganId);
				});

			migrationBuilder.CreateTable(
				name: "Patients",
				columns: table => new
				{
					PatientId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
					LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
					Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
					City = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
					BloodType = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Patients", x => x.PatientId);
				});

			migrationBuilder.CreateTable(
				name: "DonatedOrgans",
				columns: table => new
				{
					DonatedOrganId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					OrganId = table.Column<int>(type: "int", nullable: false),
					BloodType = table.Column<int>(type: "int", nullable: false),
					DonorAge = table.Column<int>(type: "int", nullable: true),
					DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					IsDonated = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DonatedOrgans", x => x.DonatedOrganId);
					table.ForeignKey(
						name: "FK_DonatedOrgans_Organs_OrganId",
						column: x => x.OrganId,
						principalTable: "Organs",
						principalColumn: "OrganId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Waitings",
				columns: table => new
				{
					WaitingId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					PatientId = table.Column<int>(type: "int", nullable: false),
					OrganId = table.Column<int>(type: "int", nullable: false),
					DateOfEntry = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Waitings", x => x.WaitingId);
					table.ForeignKey(
						name: "FK_Waitings_Organs_OrganId",
						column: x => x.OrganId,
						principalTable: "Organs",
						principalColumn: "OrganId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Waitings_Patients_PatientId",
						column: x => x.PatientId,
						principalTable: "Patients",
						principalColumn: "PatientId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "MatchedDonations",
				columns: table => new
				{
					MatchedDonationId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					PatientId = table.Column<int>(type: "int", nullable: false),
					DonationId = table.Column<int>(type: "int", nullable: false),
					DateOfMatch = table.Column<DateTime>(type: "datetime2", nullable: false),
					DonatedOrganId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MatchedDonations", x => x.MatchedDonationId);
					table.ForeignKey(
						name: "FK_MatchedDonations_DonatedOrgans_DonatedOrganId",
						column: x => x.DonatedOrganId,
						principalTable: "DonatedOrgans",
						principalColumn: "DonatedOrganId",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_MatchedDonations_Patients_PatientId",
						column: x => x.PatientId,
						principalTable: "Patients",
						principalColumn: "PatientId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_DonatedOrgans_OrganId",
				table: "DonatedOrgans",
				column: "OrganId");

			migrationBuilder.CreateIndex(
				name: "IX_MatchedDonations_DonatedOrganId",
				table: "MatchedDonations",
				column: "DonatedOrganId");

			migrationBuilder.CreateIndex(
				name: "IX_MatchedDonations_PatientId",
				table: "MatchedDonations",
				column: "PatientId");

			migrationBuilder.CreateIndex(
				name: "IX_Waitings_OrganId",
				table: "Waitings",
				column: "OrganId");

			migrationBuilder.CreateIndex(
				name: "IX_Waitings_PatientId",
				table: "Waitings",
				column: "PatientId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "MatchedDonations");

			migrationBuilder.DropTable(
				name: "Waitings");

			migrationBuilder.DropTable(
				name: "DonatedOrgans");

			migrationBuilder.DropTable(
				name: "Patients");

			migrationBuilder.DropTable(
				name: "Organs");
		}
	}
}