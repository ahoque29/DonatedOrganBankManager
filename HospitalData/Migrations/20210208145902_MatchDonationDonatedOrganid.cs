using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalData.Migrations
{
    public partial class MatchDonationDonatedOrganid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchedDonations_DonatedOrgans_DonatedOrganId",
                table: "MatchedDonations");

            migrationBuilder.DropColumn(
                name: "DonationId",
                table: "MatchedDonations");

            migrationBuilder.AlterColumn<int>(
                name: "DonatedOrganId",
                table: "MatchedDonations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchedDonations_DonatedOrgans_DonatedOrganId",
                table: "MatchedDonations",
                column: "DonatedOrganId",
                principalTable: "DonatedOrgans",
                principalColumn: "DonatedOrganId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchedDonations_DonatedOrgans_DonatedOrganId",
                table: "MatchedDonations");

            migrationBuilder.AlterColumn<int>(
                name: "DonatedOrganId",
                table: "MatchedDonations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DonationId",
                table: "MatchedDonations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchedDonations_DonatedOrgans_DonatedOrganId",
                table: "MatchedDonations",
                column: "DonatedOrganId",
                principalTable: "DonatedOrgans",
                principalColumn: "DonatedOrganId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
