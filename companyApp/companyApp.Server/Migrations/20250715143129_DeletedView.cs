using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace companyApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class DeletedView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ogrn",
                table: "company",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "kpp",
                table: "company",
                type: "character varying(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "inn",
                table: "company",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ogrn",
                table: "company",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<int>(
                name: "kpp",
                table: "company",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(9)",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<long>(
                name: "inn",
                table: "company",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12);
        }
    }
}
