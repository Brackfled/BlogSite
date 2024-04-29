﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 202, 9, 34, 231, 110, 17, 75, 216, 118, 164, 191, 78, 12, 11, 163, 232, 162, 247, 158, 153, 214, 170, 9, 139, 223, 174, 55, 74, 98, 199, 62, 124, 53, 195, 205, 231, 95, 4, 194, 180, 46, 126, 209, 143, 86, 107, 150, 156, 153, 40, 199, 118, 11, 223, 237, 105, 17, 129, 8, 28, 189, 14, 88, 81 }, new byte[] { 24, 65, 242, 167, 97, 125, 14, 154, 183, 12, 177, 25, 20, 252, 242, 251, 159, 162, 174, 18, 172, 1, 79, 203, 85, 47, 197, 63, 83, 136, 169, 56, 39, 210, 142, 180, 174, 181, 14, 34, 10, 5, 151, 126, 202, 65, 66, 133, 207, 195, 190, 128, 246, 146, 208, 93, 7, 171, 35, 95, 225, 156, 189, 116, 66, 111, 198, 247, 160, 39, 133, 97, 45, 97, 131, 35, 180, 11, 217, 75, 92, 55, 122, 44, 41, 70, 233, 190, 255, 34, 99, 104, 86, 53, 35, 218, 9, 245, 154, 39, 242, 185, 207, 139, 89, 77, 79, 210, 131, 215, 56, 159, 85, 50, 135, 67, 44, 190, 187, 193, 5, 196, 50, 27, 58, 87, 152, 213 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 111, 133, 114, 12, 110, 194, 206, 213, 233, 240, 152, 119, 89, 176, 129, 131, 195, 255, 170, 79, 135, 224, 72, 45, 36, 150, 127, 78, 241, 197, 193, 191, 28, 89, 161, 210, 175, 82, 254, 151, 178, 9, 71, 233, 160, 86, 244, 151, 78, 152, 85, 209, 108, 85, 178, 140, 115, 157, 78, 45, 66, 135, 146, 69 }, new byte[] { 55, 59, 108, 94, 182, 0, 143, 84, 234, 223, 232, 176, 91, 75, 137, 158, 86, 156, 65, 191, 248, 106, 47, 10, 41, 1, 17, 108, 183, 101, 210, 32, 71, 165, 47, 135, 55, 120, 59, 119, 39, 229, 209, 23, 85, 127, 57, 8, 68, 162, 25, 222, 17, 16, 239, 249, 135, 237, 85, 112, 52, 89, 82, 83, 112, 24, 227, 6, 202, 2, 9, 190, 195, 208, 233, 69, 95, 57, 47, 36, 89, 136, 9, 110, 183, 30, 61, 204, 66, 42, 48, 78, 187, 221, 212, 74, 210, 137, 190, 20, 218, 151, 71, 222, 130, 140, 87, 133, 19, 192, 193, 49, 169, 209, 228, 159, 239, 186, 182, 27, 58, 192, 132, 18, 248, 193, 201, 31 } });
        }
    }
}
