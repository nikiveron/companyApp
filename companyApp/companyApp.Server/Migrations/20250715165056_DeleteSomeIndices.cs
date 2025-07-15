using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace companyApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSomeIndices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_company_rep_email",
                table: "company");

            migrationBuilder.DropIndex(
                name: "IX_company_rep_phone",
                table: "company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
