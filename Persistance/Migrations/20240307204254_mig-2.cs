using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Status", "UpdatedDate" },
                values: new object[] { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "oncellhsyn@outlook.com", "Hüseyin", "ÖNCEL", new byte[] { 99, 27, 216, 28, 172, 86, 146, 36, 129, 226, 179, 120, 171, 193, 15, 37, 143, 191, 41, 186, 227, 208, 185, 78, 251, 144, 244, 194, 93, 30, 210, 102, 125, 81, 187, 8, 114, 221, 12, 202, 112, 230, 172, 54, 162, 152, 53, 26, 201, 27, 141, 15, 30, 134, 144, 97, 188, 146, 67, 190, 18, 131, 182, 128 }, new byte[] { 187, 242, 63, 253, 248, 243, 115, 79, 146, 121, 255, 25, 74, 61, 93, 80, 83, 56, 27, 18, 65, 105, 240, 135, 184, 232, 181, 25, 210, 152, 227, 122, 236, 220, 85, 27, 104, 238, 119, 153, 80, 200, 131, 169, 74, 105, 81, 251, 16, 46, 234, 211, 94, 185, 7, 229, 205, 10, 50, 187, 5, 85, 122, 99, 104, 8, 111, 227, 113, 47, 96, 96, 69, 176, 235, 7, 156, 60, 65, 241, 35, 60, 54, 47, 169, 139, 59, 232, 163, 225, 56, 226, 232, 80, 7, 227, 167, 254, 248, 106, 215, 37, 152, 32, 156, 167, 244, 198, 225, 235, 162, 193, 213, 43, 153, 225, 86, 202, 66, 229, 219, 253, 253, 207, 77, 149, 29, 12 }, true, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
