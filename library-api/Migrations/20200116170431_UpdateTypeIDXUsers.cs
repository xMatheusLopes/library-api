using Microsoft.EntityFrameworkCore.Migrations;

namespace library_api.Migrations
{
    public partial class UpdateTypeIDXUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "TypeIDX",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "TypeIDX",
                table: "Users",
                column: "TypeID",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.Cascade
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
