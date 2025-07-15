using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace companyApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddIndices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_company_inn",
                table: "company",
                column: "inn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_company_kpp",
                table: "company",
                column: "kpp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_company_ogrn",
                table: "company",
                column: "ogrn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_company_rep_email",
                table: "company",
                column: "rep_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_company_rep_phone",
                table: "company",
                column: "rep_phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_company_inn",
                table: "company");

            migrationBuilder.DropIndex(
                name: "IX_company_kpp",
                table: "company");

            migrationBuilder.DropIndex(
                name: "IX_company_ogrn",
                table: "company");

            migrationBuilder.DropIndex(
                name: "IX_company_rep_email",
                table: "company");

            migrationBuilder.DropIndex(
                name: "IX_company_rep_phone",
                table: "company");
        }
    }
}
