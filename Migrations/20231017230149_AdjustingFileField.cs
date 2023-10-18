using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueueWorker.Migrations
{
    /// <inheritdoc />
    public partial class AdjustingFileField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemoteAcess",
                table: "Devices",
                newName: "RemoteAccess");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "CsvFiles",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemoteAccess",
                table: "Devices",
                newName: "RemoteAcess");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CsvFiles",
                newName: "FileName");
        }
    }
}
