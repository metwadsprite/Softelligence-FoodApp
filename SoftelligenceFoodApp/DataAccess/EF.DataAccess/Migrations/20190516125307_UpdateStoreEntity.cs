using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF.DataAccess.Migrations
{
    public partial class UpdateStoreEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Sessions_SessionId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_SessionId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Sessions");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SessionsStores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SessionId = table.Column<int>(nullable: true),
                    StoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionsStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionsStores_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionsStores_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SessionId",
                table: "Orders",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionsStores_SessionId",
                table: "SessionsStores",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionsStores_StoreId",
                table: "SessionsStores",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Sessions_SessionId",
                table: "Orders",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Sessions_SessionId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "SessionsStores");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SessionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Stores",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Sessions",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_Stores_SessionId",
                table: "Stores",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Sessions_SessionId",
                table: "Stores",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
