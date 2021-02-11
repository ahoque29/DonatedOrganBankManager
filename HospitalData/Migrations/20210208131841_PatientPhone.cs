using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalData.Migrations
{
	public partial class PatientPhone : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Phone",
				table: "Patients",
				type: "nvarchar(max)",
				nullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Phone",
				table: "Patients");
		}
	}
}