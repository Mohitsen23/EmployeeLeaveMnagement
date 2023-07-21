using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Existing code for creating other tables (LeaveTable, Manager, Employee, etc.)

            migrationBuilder.CreateTable(
                name: "EmployeeProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmplId = table.Column<int>(type: "int", nullable: false),
                    Profile = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfile", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Existing code for dropping other tables (EmployeeData, LeaveQuota, etc.)

            migrationBuilder.DropTable(
                name: "EmployeeProfile");
        }
    }
}
