using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tryingSystem.Migrations
{
    /// <inheritdoc />
    public partial class CreateTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    AssignedToNurseId = table.Column<int>(type: "int", nullable: false),
                    AssignedByDoctorId = table.Column<int>(type: "int", nullable: false),
                    AssingedByDcotorId = table.Column<int>(type: "int", nullable: false),
                    AssingedToNurseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_UserAccounts_AssignedByDoctorId",
                        column: x => x.AssignedByDoctorId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_UserAccounts_AssignedToNurseId",
                        column: x => x.AssignedToNurseId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "Role", "UserName" },
                values: new object[] { 1, "admin11@gmail.com", "Admin", "Manager", "A123456789", "Manager", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedByDoctorId",
                table: "Tasks",
                column: "AssignedByDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedToNurseId",
                table: "Tasks",
                column: "AssignedToNurseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_Email",
                table: "UserAccounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_UserName",
                table: "UserAccounts",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
