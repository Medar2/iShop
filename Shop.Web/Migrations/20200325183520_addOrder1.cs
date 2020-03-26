using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Web.Migrations
{
    public partial class addOrder1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
