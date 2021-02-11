using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalData.Migrations
{
	public partial class DonatedOrganBloodTypeFromIntToString : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "BloodType",
				table: "DonatedOrgans",
				type: "nvarchar(max)",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<int>(
				name: "BloodType",
				table: "DonatedOrgans",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true);
		}
	}
}