using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TimescaleDataProcessor.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddValuesAndResultsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: false, comment: "Имя файла, из которого получен интегральный результат"),
                    TimeDeltaInSeconds = table.Column<double>(type: "double precision", nullable: false, comment: "Дельта времени StartTime в секундах"),
                    MinStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Минимальное время начала операции в формате UTC"),
                    AvgExecutionTime = table.Column<double>(type: "double precision", nullable: false, comment: "Среднее время выполнения операции в секундах"),
                    AvgIndicator = table.Column<double>(type: "double precision", nullable: false, comment: "Среднее значение показателя"),
                    MedianIndicator = table.Column<double>(type: "double precision", nullable: false, comment: "Медианное значение показателя"),
                    MaxIndicator = table.Column<double>(type: "double precision", nullable: false, comment: "Максимальное значение показателя"),
                    MinIndicator = table.Column<double>(type: "double precision", nullable: false, comment: "Минимальное значение показателя")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: false, comment: "Имя файла, из которого было получено значение операции"),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Время начала операции в формате UTC"),
                    ExecutionTime = table.Column<double>(type: "double precision", nullable: false, comment: "Время выполнения операции в секундах"),
                    Indicator = table.Column<double>(type: "double precision", nullable: false, comment: "Значение показателя")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_FileName",
                table: "Results",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Values_FileName",
                table: "Values",
                column: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Values");
        }
    }
}
