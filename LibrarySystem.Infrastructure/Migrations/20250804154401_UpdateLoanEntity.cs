using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLoanEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "Loans",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "Loans");
        }
    }
}
