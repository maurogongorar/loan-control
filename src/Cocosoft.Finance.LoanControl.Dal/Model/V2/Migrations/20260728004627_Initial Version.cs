using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cocosoft.Finance.LoanControl.Dal.Model.V2.Migrations
{
    /// <inheritdoc />
    public partial class InitialVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    VERSION = table.Column<int>(type: "INTEGER", nullable: false),
                    IS_CURRENT = table.Column<bool>(type: "INTEGER", nullable: false),
                    IDENTIFICATION_NUMBER = table.Column<string>(type: "TEXT", nullable: false),
                    NAME = table.Column<string>(type: "TEXT", nullable: false),
                    SURNAME = table.Column<string>(type: "TEXT", nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "TEXT", nullable: false),
                    EMAIL = table.Column<string>(type: "TEXT", nullable: true),
                    CITY = table.Column<string>(type: "TEXT", nullable: false),
                    ADDRESS = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERS", x => new { x.ID, x.VERSION });
                });

            migrationBuilder.CreateTable(
                name: "ACCOUNTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    VERSION = table.Column<int>(type: "INTEGER", nullable: false),
                    IS_CURRENT = table.Column<bool>(type: "INTEGER", nullable: false),
                    ACCOUNT_NUMBER = table.Column<string>(type: "TEXT", nullable: false),
                    CUSTOMER_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    CUSTOMER_VERSION = table.Column<int>(type: "INTEGER", nullable: false),
                    IS_LOCKED = table.Column<bool>(type: "INTEGER", nullable: false),
                    IS_DELETED = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNTS", x => new { x.ID, x.VERSION });
                    table.ForeignKey(
                        name: "FK_ACCOUNTS_CUSTOMERS_CUSTOMER_ID_CUSTOMER_VERSION",
                        columns: x => new { x.CUSTOMER_ID, x.CUSTOMER_VERSION },
                        principalTable: "CUSTOMERS",
                        principalColumns: new[] { "ID", "VERSION" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LOANS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    VERSION = table.Column<int>(type: "INTEGER", nullable: false),
                    IS_CURRENT = table.Column<bool>(type: "INTEGER", nullable: false),
                    LOAN_NUMBER = table.Column<string>(type: "TEXT", nullable: false),
                    ACCOUNT_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    ACCOUNT_VERSION = table.Column<int>(type: "INTEGER", nullable: false),
                    INITIAL_AMOUNT = table.Column<decimal>(type: "TEXT", nullable: false),
                    DISBURSEMENT_DATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CURRENT_BALANCE = table.Column<decimal>(type: "TEXT", nullable: false),
                    FEE = table.Column<decimal>(type: "TEXT", nullable: false),
                    ANNUAL_INTEREST = table.Column<double>(type: "REAL", nullable: false),
                    LAST_PAYMENT_DATE = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NUMBER_INSTALMENTS = table.Column<int>(type: "INTEGER", nullable: false),
                    INTEREST_COLLECTED = table.Column<decimal>(type: "TEXT", nullable: false),
                    IS_CLOSED = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOANS", x => new { x.ID, x.VERSION });
                    table.ForeignKey(
                        name: "FK_LOANS_ACCOUNTS_ACCOUNT_ID_ACCOUNT_VERSION",
                        columns: x => new { x.ACCOUNT_ID, x.ACCOUNT_VERSION },
                        principalTable: "ACCOUNTS",
                        principalColumns: new[] { "ID", "VERSION" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    VERSION = table.Column<int>(type: "INTEGER", nullable: false),
                    IS_CURRENT = table.Column<bool>(type: "INTEGER", nullable: false),
                    LOAN_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    LOAN_VERSION = table.Column<int>(type: "INTEGER", nullable: false),
                    DATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AMOUNT = table.Column<decimal>(type: "TEXT", nullable: false),
                    CAPITAL = table.Column<decimal>(type: "TEXT", nullable: false),
                    INTEREST = table.Column<decimal>(type: "TEXT", nullable: false),
                    NEW_BALANCE = table.Column<decimal>(type: "TEXT", nullable: false),
                    IS_DELETED = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENTS", x => new { x.ID, x.VERSION });
                    table.ForeignKey(
                        name: "FK_PAYMENTS_LOANS_LOAN_ID_LOAN_VERSION",
                        columns: x => new { x.LOAN_ID, x.LOAN_VERSION },
                        principalTable: "LOANS",
                        principalColumns: new[] { "ID", "VERSION" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNTS_CUSTOMER_ID_CUSTOMER_VERSION",
                table: "ACCOUNTS",
                columns: new[] { "CUSTOMER_ID", "CUSTOMER_VERSION" });

            migrationBuilder.CreateIndex(
                name: "IX_U_ACCOUNTS_ACCOUNT_NUMBER",
                table: "ACCOUNTS",
                column: "ACCOUNT_NUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_U_CUSTOMERS_IDENTIFICATION_NUMBER",
                table: "CUSTOMERS",
                column: "IDENTIFICATION_NUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LOANS_ACCOUNT_ID_ACCOUNT_VERSION",
                table: "LOANS",
                columns: new[] { "ACCOUNT_ID", "ACCOUNT_VERSION" });

            migrationBuilder.CreateIndex(
                name: "IX_U_LOANS_LOAN_NUMBER",
                table: "LOANS",
                column: "LOAN_NUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PAYMENTS_LOAN_ID_LOAN_VERSION",
                table: "PAYMENTS",
                columns: new[] { "LOAN_ID", "LOAN_VERSION" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PAYMENTS");

            migrationBuilder.DropTable(
                name: "LOANS");

            migrationBuilder.DropTable(
                name: "ACCOUNTS");

            migrationBuilder.DropTable(
                name: "CUSTOMERS");
        }
    }
}
