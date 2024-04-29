﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 56, 54, 184, 126, 128, 227, 178, 53, 151, 199, 208, 11, 105, 223, 177, 69, 134, 54, 214, 220, 151, 109, 51, 118, 76, 150, 237, 14, 125, 28, 63, 77, 220, 225, 245, 112, 245, 212, 134, 230, 174, 253, 248, 233, 160, 188, 122, 128, 86, 167, 226, 128, 3, 206, 200, 63, 6, 70, 0, 80, 137, 84, 214, 160 }, new byte[] { 237, 119, 148, 90, 165, 139, 98, 63, 228, 252, 5, 44, 172, 163, 60, 57, 71, 65, 211, 80, 103, 106, 133, 7, 34, 83, 1, 175, 60, 211, 175, 23, 212, 57, 41, 200, 69, 56, 68, 211, 142, 226, 4, 77, 62, 124, 74, 101, 188, 135, 12, 62, 254, 34, 60, 40, 213, 239, 131, 210, 211, 29, 198, 120, 147, 170, 5, 77, 192, 47, 146, 171, 102, 159, 255, 178, 16, 98, 206, 89, 46, 131, 165, 248, 241, 97, 27, 220, 68, 12, 145, 248, 20, 198, 58, 143, 248, 103, 67, 33, 222, 227, 33, 253, 176, 150, 135, 3, 234, 249, 140, 16, 12, 3, 206, 229, 63, 185, 246, 39, 209, 83, 142, 163, 138, 179, 83, 160 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 223, 47, 32, 94, 191, 245, 17, 37, 82, 168, 110, 126, 135, 245, 212, 40, 121, 145, 132, 103, 229, 57, 78, 111, 147, 125, 143, 60, 125, 39, 85, 150, 35, 21, 136, 86, 204, 227, 92, 207, 71, 164, 104, 233, 73, 67, 78, 10, 5, 193, 246, 249, 107, 198, 237, 249, 238, 9, 189, 120, 215, 5, 178, 136 }, new byte[] { 23, 235, 92, 12, 210, 70, 82, 136, 93, 113, 83, 208, 194, 42, 155, 207, 253, 61, 120, 145, 176, 218, 239, 187, 173, 100, 86, 74, 222, 21, 184, 144, 62, 149, 93, 221, 197, 192, 46, 143, 147, 116, 133, 10, 115, 215, 192, 100, 79, 224, 164, 63, 185, 84, 190, 175, 152, 193, 32, 98, 47, 28, 163, 104, 35, 104, 99, 57, 114, 194, 189, 166, 209, 86, 154, 153, 244, 131, 35, 80, 206, 117, 141, 82, 126, 223, 222, 129, 54, 255, 229, 13, 156, 241, 29, 254, 206, 242, 218, 222, 14, 100, 33, 167, 140, 26, 82, 32, 161, 35, 34, 195, 7, 197, 225, 216, 58, 103, 65, 219, 250, 76, 226, 190, 105, 231, 243, 20 } });
        }
    }
}