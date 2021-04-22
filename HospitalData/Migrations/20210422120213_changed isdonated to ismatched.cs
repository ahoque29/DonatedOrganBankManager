using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalData.Migrations
{
	public partial class changedisdonatedtoismatched : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "IsDonated",
				table: "DonatedOrgans",
				newName: "IsMatched");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "IsMatched",
				table: "DonatedOrgans",
				newName: "IsDonated");
		}
	}
}