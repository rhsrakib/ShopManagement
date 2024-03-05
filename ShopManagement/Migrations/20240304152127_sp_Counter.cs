using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopManagement.Migrations
{
    /// <inheritdoc />
    public partial class sp_Counter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var Sp1 = @"CREATE PROCEDURE InsertCounter
                        @PosName varchar (50)
                        AS
                        BEGIN
                        INSERT INTO Counters (PosName) VALUES (@PosName)
                        END";
            migrationBuilder.Sql(Sp1);
            var Sp2 = @"CREATE PROCEDURE GetCounter
                        AS
                        BEGIN
                        SELECT * FROM Counters
                        END";
            migrationBuilder.Sql(Sp2);
            var Sp3 = @"CREATE PROCEDURE UpdateCounter
                        @CounterId INT,
                        @PosName varchar (50)
                        AS
                        BEGIN
                        UPDATE Counters SET PosName = @PosName WHERE CounterId = @CounterId
                        END";
            migrationBuilder.Sql(Sp3);
            var Sp4 = @"CREATE PROCEDURE DeleteCounter
                        @CounterId INT
                        AS
                        BEGIN
                        DELETE FROM Counters WHERE CounterId = @CounterId
                        END";
            migrationBuilder.Sql(Sp4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var Sp1 = @"DROP PROCEDURE InsertCounter";
            migrationBuilder.Sql(Sp1);
            var Sp2 = @"DROP PROCEDURE InsertCounter";
            migrationBuilder.Sql(Sp2);
            var Sp3 = @"DROP PROCEDURE InsertCounter";
            migrationBuilder.Sql(Sp3);
            var Sp4 = @"DROP PROCEDURE InsertCounter";
            migrationBuilder.Sql(Sp4);
        }
    }
}
