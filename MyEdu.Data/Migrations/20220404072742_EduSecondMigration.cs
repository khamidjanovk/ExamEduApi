using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MyEdu.Data.Migrations
{
    public partial class EduSecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseType_CourseTypeId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseType",
                table: "CourseType");

            migrationBuilder.RenameTable(
                name: "CourseType",
                newName: "CourseTypes");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Courses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTypes",
                table: "CourseTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseTypes_CourseTypeId",
                table: "Courses",
                column: "CourseTypeId",
                principalTable: "CourseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseTypes_CourseTypeId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTypes",
                table: "CourseTypes");

            migrationBuilder.RenameTable(
                name: "CourseTypes",
                newName: "CourseType");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Courses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseType",
                table: "CourseType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseType_CourseTypeId",
                table: "Courses",
                column: "CourseTypeId",
                principalTable: "CourseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
