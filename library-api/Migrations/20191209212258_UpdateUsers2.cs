using Microsoft.EntityFrameworkCore.Migrations;

namespace library_api.Migrations
{
    public partial class UpdateUsers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeID",
                table: "Users",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Users"
            );

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Users",
                nullable: true,
                defaultValue: "Teste",
                type: "varchar(255)"
            );

            migrationBuilder.AddForeignKey(
                name: "TypeIDX",
                table: "Users",
                column: "TypeID",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.InsertData(
                column: "Name",
                table: "UserTypes",
                value: "Admin"
            );

            migrationBuilder.InsertData(
                column: "Name",
                table: "UserTypes",
                value: "Seller"
            );

            migrationBuilder.InsertData(
                column: "Name",
                table: "UserTypes",
                value: "Customer"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TypeID",
                table: "Users",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
