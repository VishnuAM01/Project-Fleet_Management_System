using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fleeman_with_dot_net.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "add_on_book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    add_on_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_add_on_book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "add_on_details",
                columns: table => new
                {
                    addOnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    addOnName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addOnPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_add_on_details", x => x.addOnId);
                });

            migrationBuilder.CreateTable(
                name: "airport_details",
                columns: table => new
                {
                    Airport_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Airport_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Airport_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_airport_details", x => x.Airport_Id);
                });

            migrationBuilder.CreateTable(
                name: "authentication_table",
                columns: table => new
                {
                    AuthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authentication_table", x => x.AuthId);
                });

            migrationBuilder.CreateTable(
                name: "booking_details",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DropLocation = table.Column<int>(type: "int", nullable: true),
                    PickupDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PickupLocation = table.Column<int>(type: "int", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    AddonBookId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_details", x => x.BookingId);
                });

            migrationBuilder.CreateTable(
                name: "car_details",
                columns: table => new
                {
                    Car_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuelStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    MaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_details", x => x.Car_Id);
                });

            migrationBuilder.CreateTable(
                name: "invoice_header_table",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    ActualReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DropLocation = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    CarRentalAmount = table.Column<double>(type: "float", nullable: false),
                    AddonRentalAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice_header_table", x => x.InvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "member_details",
                columns: table => new
                {
                    Member_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditCard = table.Column<int>(type: "int", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DrivingLicenseId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DrivingLicenseIssuedBy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DrivingLicenseValidThru = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MemberFirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Idp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdpIssuedBy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdpValidThru = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MemberLastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    PassportIssuedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportValidate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_details", x => x.Member_Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "state_details",
                columns: table => new
                {
                    State_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state_details", x => x.State_Id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_details",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyRate = table.Column<double>(type: "float", nullable: false),
                    MonthlyRate = table.Column<double>(type: "float", nullable: false),
                    WeeklyRate = table.Column<double>(type: "float", nullable: false),
                    ImgPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_details", x => x.VehicleId);
                });

            migrationBuilder.CreateTable(
                name: "booking_confirmation_details",
                columns: table => new
                {
                    BookingConfirmationDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Car_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_confirmation_details", x => x.BookingConfirmationDetailsId);
                    table.ForeignKey(
                        name: "FK_booking_confirmation_details_booking_details_BookingId",
                        column: x => x.BookingId,
                        principalTable: "booking_details",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_confirmation_details_car_details_Car_Id",
                        column: x => x.Car_Id,
                        principalTable: "car_details",
                        principalColumn: "Car_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_user",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MemberDetailsMember_Id = table.Column<int>(type: "int", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_application_user_member_details_MemberDetailsMember_Id",
                        column: x => x.MemberDetailsMember_Id,
                        principalTable: "member_details",
                        principalColumn: "Member_Id");
                });

            migrationBuilder.CreateTable(
                name: "city_details",
                columns: table => new
                {
                    City_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city_details", x => x.City_Id);
                    table.ForeignKey(
                        name: "FK_city_details_state_details_State_Id",
                        column: x => x.State_Id,
                        principalTable: "state_details",
                        principalColumn: "State_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staff_details",
                columns: table => new
                {
                    Staff_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffFirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    StaffLastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff_details", x => x.Staff_Id);
                    table.ForeignKey(
                        name: "FK_staff_details_application_user_UserId",
                        column: x => x.UserId,
                        principalTable: "application_user",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "location_details",
                columns: table => new
                {
                    Location_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Airport_Id = table.Column<int>(type: "int", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City_Id = table.Column<int>(type: "int", nullable: true),
                    State_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location_details", x => x.Location_Id);
                    table.ForeignKey(
                        name: "FK_location_details_city_details_City_Id",
                        column: x => x.City_Id,
                        principalTable: "city_details",
                        principalColumn: "City_Id");
                    table.ForeignKey(
                        name: "FK_location_details_state_details_State_Id",
                        column: x => x.State_Id,
                        principalTable: "state_details",
                        principalColumn: "State_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_application_user_MemberDetailsMember_Id",
                table: "application_user",
                column: "MemberDetailsMember_Id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_confirmation_details_BookingId",
                table: "booking_confirmation_details",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_booking_confirmation_details_Car_Id",
                table: "booking_confirmation_details",
                column: "Car_Id");

            migrationBuilder.CreateIndex(
                name: "IX_city_details_State_Id",
                table: "city_details",
                column: "State_Id");

            migrationBuilder.CreateIndex(
                name: "IX_location_details_City_Id",
                table: "location_details",
                column: "City_Id");

            migrationBuilder.CreateIndex(
                name: "IX_location_details_State_Id",
                table: "location_details",
                column: "State_Id");

            migrationBuilder.CreateIndex(
                name: "IX_staff_details_UserId",
                table: "staff_details",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "add_on_book");

            migrationBuilder.DropTable(
                name: "add_on_details");

            migrationBuilder.DropTable(
                name: "airport_details");

            migrationBuilder.DropTable(
                name: "authentication_table");

            migrationBuilder.DropTable(
                name: "booking_confirmation_details");

            migrationBuilder.DropTable(
                name: "invoice_header_table");

            migrationBuilder.DropTable(
                name: "location_details");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "staff_details");

            migrationBuilder.DropTable(
                name: "vehicle_details");

            migrationBuilder.DropTable(
                name: "booking_details");

            migrationBuilder.DropTable(
                name: "car_details");

            migrationBuilder.DropTable(
                name: "city_details");

            migrationBuilder.DropTable(
                name: "application_user");

            migrationBuilder.DropTable(
                name: "state_details");

            migrationBuilder.DropTable(
                name: "member_details");
        }
    }
}
