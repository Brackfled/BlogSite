using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class init10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_BlogFiles_SubjectImageFileId1",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_SubjectImageFileId1",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "SubjectImageFileId1",
                table: "Subject");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 37, 221, 28, 127, 80, 219, 98, 247, 170, 3, 147, 223, 71, 98, 157, 87, 76, 90, 151, 71, 120, 82, 19, 29, 224, 228, 28, 156, 25, 64, 153, 194, 31, 60, 68, 178, 60, 76, 96, 231, 108, 242, 157, 230, 114, 23, 4, 161, 237, 232, 192, 249, 210, 167, 34, 196, 116, 189, 187, 132, 178, 139, 185, 95 }, new byte[] { 128, 39, 36, 47, 14, 205, 121, 255, 229, 83, 188, 67, 222, 187, 79, 82, 137, 16, 24, 165, 204, 109, 246, 95, 143, 110, 10, 204, 13, 246, 152, 168, 172, 164, 154, 201, 115, 189, 225, 29, 3, 186, 127, 66, 108, 43, 125, 30, 8, 57, 78, 66, 142, 243, 217, 228, 115, 2, 174, 214, 111, 48, 161, 92, 220, 247, 12, 6, 251, 127, 221, 77, 53, 95, 54, 17, 127, 150, 67, 90, 142, 255, 160, 156, 81, 195, 96, 206, 243, 188, 65, 234, 239, 203, 248, 233, 30, 214, 179, 173, 207, 60, 243, 148, 240, 103, 155, 120, 18, 169, 40, 64, 67, 23, 242, 9, 237, 224, 51, 241, 32, 247, 41, 235, 236, 41, 53, 159 } });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SubjectImageFileId",
                table: "Subject",
                column: "SubjectImageFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_BlogFiles_SubjectImageFileId",
                table: "Subject",
                column: "SubjectImageFileId",
                principalTable: "BlogFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_BlogFiles_SubjectImageFileId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_SubjectImageFileId",
                table: "Subject");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectImageFileId1",
                table: "Subject",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 222, 254, 20, 231, 136, 5, 90, 152, 25, 41, 235, 101, 68, 233, 67, 133, 184, 161, 26, 244, 143, 28, 2, 168, 208, 144, 123, 168, 51, 31, 179, 220, 15, 222, 134, 30, 111, 107, 251, 142, 87, 241, 24, 102, 185, 252, 148, 16, 135, 52, 98, 95, 59, 17, 219, 243, 154, 147, 50, 30, 186, 81, 155, 101 }, new byte[] { 151, 119, 8, 98, 246, 185, 186, 92, 230, 122, 189, 129, 247, 69, 184, 125, 116, 118, 100, 114, 80, 199, 148, 215, 80, 239, 111, 114, 221, 247, 166, 78, 84, 44, 160, 10, 168, 60, 34, 186, 219, 235, 218, 166, 30, 243, 192, 0, 20, 182, 139, 209, 56, 134, 245, 54, 29, 154, 161, 118, 111, 53, 104, 46, 0, 168, 98, 27, 49, 205, 150, 199, 204, 170, 23, 34, 120, 40, 195, 136, 103, 89, 117, 75, 99, 86, 242, 240, 125, 48, 84, 46, 56, 158, 120, 155, 83, 156, 203, 189, 98, 92, 198, 20, 151, 136, 227, 106, 206, 123, 176, 164, 56, 108, 70, 64, 226, 46, 25, 248, 29, 112, 159, 34, 15, 11, 6, 18 } });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SubjectImageFileId1",
                table: "Subject",
                column: "SubjectImageFileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_BlogFiles_SubjectImageFileId1",
                table: "Subject",
                column: "SubjectImageFileId1",
                principalTable: "BlogFiles",
                principalColumn: "Id");
        }
    }
}
