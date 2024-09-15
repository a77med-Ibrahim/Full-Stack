using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tryingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddTodoItemModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todoItemModels_UserAccounts_UserId",
                table: "todoItemModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_todoItemModels",
                table: "todoItemModels");

            migrationBuilder.RenameTable(
                name: "todoItemModels",
                newName: "TodoItems");

            migrationBuilder.RenameColumn(
                name: "Descriptoin",
                table: "TodoItems",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_todoItemModels_UserId",
                table: "TodoItems",
                newName: "IX_TodoItems_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_UserAccounts_UserId",
                table: "TodoItems",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_UserAccounts_UserId",
                table: "TodoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems");

            migrationBuilder.RenameTable(
                name: "TodoItems",
                newName: "todoItemModels");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "todoItemModels",
                newName: "Descriptoin");

            migrationBuilder.RenameIndex(
                name: "IX_TodoItems_UserId",
                table: "todoItemModels",
                newName: "IX_todoItemModels_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_todoItemModels",
                table: "todoItemModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_todoItemModels_UserAccounts_UserId",
                table: "todoItemModels",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
