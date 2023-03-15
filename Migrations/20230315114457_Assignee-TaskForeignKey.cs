using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class AssigneeTaskForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TasksTaskId",
                table: "Assignees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignees_TasksTaskId",
                table: "Assignees",
                column: "TasksTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignees_Tasks_TasksTaskId",
                table: "Assignees",
                column: "TasksTaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignees_Tasks_TasksTaskId",
                table: "Assignees");

            migrationBuilder.DropIndex(
                name: "IX_Assignees_TasksTaskId",
                table: "Assignees");

            migrationBuilder.DropColumn(
                name: "TasksTaskId",
                table: "Assignees");
        }
    }
}
