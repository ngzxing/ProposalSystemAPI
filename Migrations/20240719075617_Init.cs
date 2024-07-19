using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProposalSystem.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicPrograms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Logined = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admin_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcademicProgramId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Domain = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                    table.UniqueConstraint("AK_Lecturers_StaffId", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_Lecturers_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lecturers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LecturerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcademicProgramId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Committees_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Committees_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "StaffId");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatricId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Session = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    AcademicProgramId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SupervisorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FirstEvaluatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SecondEvaluatorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.UniqueConstraint("AK_Students_MatricId", x => x.MatricId);
                    table.ForeignKey(
                        name: "FK_Students_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Lecturers_FirstEvaluatorId",
                        column: x => x.FirstEvaluatorId,
                        principalTable: "Lecturers",
                        principalColumn: "StaffId");
                    table.ForeignKey(
                        name: "FK_Students_Lecturers_SecondEvaluatorId",
                        column: x => x.SecondEvaluatorId,
                        principalTable: "Lecturers",
                        principalColumn: "StaffId");
                    table.ForeignKey(
                        name: "FK_Students_Lecturers_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Lecturers",
                        principalColumn: "StaffId");
                });

            migrationBuilder.CreateTable(
                name: "ApplySupervisors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SupervisorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatricId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplyState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplySupervisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplySupervisors_Lecturers_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Lecturers",
                        principalColumn: "StaffId");
                    table.ForeignKey(
                        name: "FK_ApplySupervisors_Students_MatricId",
                        column: x => x.MatricId,
                        principalTable: "Students",
                        principalColumn: "MatricId");
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Session = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    LinkProposal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkForm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProposalStatus = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "MatricId");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProposalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EvaluatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BackupEvaluatorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Lecturers_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Lecturers",
                        principalColumn: "StaffId");
                    table.ForeignKey(
                        name: "FK_Comments_Proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AcademicPrograms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "program1", "Software Engineering Description", "Software Engineering" },
                    { "program2", "Data Engineering Description", "Data Engineering" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "Lecturer", "LECTURER" },
                    { "3", null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logined", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "admin1", 0, "251155c7-840f-45a0-b972-8f8992e91646", "ngzixing@example.com", false, false, null, false, "NGZIXING@EXAMPLE.COM", "NGZIXING", "AQAAAAIAAYagAAAAEMcO9PT9qNxzfd20HmaEJ3YlEha0W1tFWsGAPZOatPgC6ipijVIsm3i7ALvsLkT14w==", "0123456789", false, "f66befb2-0020-4ca0-bd29-c34e183f92eb", false, "NgZiXing" },
                    { "lecturer1", 0, "9380b9f7-26b3-4857-89b1-6c748fc06ee8", "angyiqin@example.com", false, false, null, false, "ANGYIQIN@EXAMPLE.COM", "ANGYIQIN", "AQAAAAIAAYagAAAAEEImL4w7x/iOrQdHPig/fG4DHJZdzzWjY7ElaEqeLDS8f6eYEN5x5K4tfLb2jTTPqg==", "0123456789", false, "a475eaf2-2b89-4801-ba35-3163803e0327", false, "AngYiQin" },
                    { "lecturer2", 0, "a2696f74-82ea-4d09-8731-d688e6978dfe", "liewyvonne@example.com", false, false, null, false, "liewyvonne@EXAMPLE.COM", "LIEWYVONNE", "AQAAAAIAAYagAAAAEMMgF/9lcg2SoiRglmR23jX4T8+7E34iusZMrU5ABI4T0cSlQhdNGSg7KsT8+j/FpQ==", "0123456789", false, "aab6e28b-47ec-4fbd-b768-028990113d83", false, "LiewYvonne" },
                    { "lecturer3", 0, "689c27a1-5883-4519-ae1f-1708a3cb8be7", "soowanying@example.com", false, false, null, false, "SOOWANYING@EXAMPLE.COM", "SOOWANYING", "AQAAAAIAAYagAAAAEFVgZZdQUbBGNVe5bGelLf4XyTsxVn/XwP4z86lCq0nsnRZWTqmF3ZES12qNaXRvrQ==", "0123456789", false, "64e30a89-d801-41f9-a5fc-c4fca64ec41d", false, "SooWanYing" },
                    { "student1", 0, "becbfb63-6df3-414d-bbcd-59f32f5dff26", "yewruixiang@example.com", false, false, null, false, "YEWRUIXIANG@EXAMPLE.COM", "YEWRUIXIANG", "AQAAAAIAAYagAAAAEJZRm6frfIISUl+5tF20d24f6mkqZeWGVABk1EzlxOp+7ApUe5cVJi1HtTtlQI1Qhg==", "0123456789", false, "9c436b38-52ff-4b3c-bdd2-1df833a9d60a", false, "YewRuiXiang" },
                    { "student2", 0, "720fbd7a-e4dd-4565-9e47-18b47cb22348", "loozhiyuan@example.com", false, false, null, false, "LOOZHIYUAN@EXAMPLE.COM", "LOOZHIYUAN", "AQAAAAIAAYagAAAAEJKvxbVTQvouTHnL6LKUjs3HZ9Cb9GQzNz4KHeXJP5wYo7gIvPRMH+PnH7oY0gzfmA==", "0123456789", false, "62f7cc0a-5029-42b3-aaa5-0283401551eb", false, "LooZhiYuan" },
                    { "student3", 0, "e2ae707a-0b44-41a9-a588-b8598c8d47b2", "samchiayun@example.com", false, false, null, false, "SAMCHIAYUN@EXAMPLE.COM", "SAMCHIAYUN", "AQAAAAIAAYagAAAAELKQ1Gy183hKxTZuaspMeFeq+xcp0Gc9gnLtvWDg59nLbOZN0BDGzm5MJNhpow28HA==", "0123456789", false, "1cdb9311-a86e-4246-b22c-ad73228d9f54", false, "SamChiaYun" }
                });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "AdminId" },
                values: new object[] { "admin1", "A21EC0213" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "admin1" },
                    { "2", "lecturer1" },
                    { "2", "lecturer2" },
                    { "2", "lecturer3" },
                    { "3", "student1" },
                    { "3", "student2" },
                    { "3", "student3" }
                });

            migrationBuilder.InsertData(
                table: "Lecturers",
                columns: new[] { "Id", "AcademicProgramId", "Domain", "StaffId" },
                values: new object[,]
                {
                    { "lecturer1", "program1", 0, "A21EC1234" },
                    { "lecturer2", "program1", 0, "A21EC2234" },
                    { "lecturer3", "program2", 1, "A21EC3234" }
                });

            migrationBuilder.InsertData(
                table: "Committees",
                columns: new[] { "Id", "AcademicProgramId", "LecturerId" },
                values: new object[,]
                {
                    { "committee1", "program1", "A21EC1234" },
                    { "committee2", "program1", "A21EC2234" },
                    { "committee3", "program2", "A21EC3234" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AcademicProgramId", "FirstEvaluatorId", "MatricId", "SecondEvaluatorId", "Semester", "Session", "SupervisorId", "Year" },
                values: new object[,]
                {
                    { "student1", "program1", "A21EC2234", "A21EC0149", "A21EC1234", 1, 1, "A21EC3234", 1 },
                    { "student2", "program1", "A21EC2234", "A21EC0197", "A21EC1234", 1, 2, "A21EC3234", 1 },
                    { "student3", "program2", null, "A21EC0127", null, 2, 3, "A21EC1234", 1 }
                });

            migrationBuilder.InsertData(
                table: "ApplySupervisors",
                columns: new[] { "Id", "ApplyState", "MatricId", "SupervisorId" },
                values: new object[,]
                {
                    { "committee1", 2, "A21EC0149", "A21EC1234" },
                    { "committee2", 1, "A21EC0149", "A21EC2234" },
                    { "committee3", 1, "A21EC0197", "A21EC3234" },
                    { "committee4", 0, "A21EC0197", "A21EC2234" }
                });

            migrationBuilder.InsertData(
                table: "Proposals",
                columns: new[] { "Id", "CreatedAt", "Domain", "LinkForm", "LinkProposal", "Mark", "ProposalStatus", "Semester", "Session", "StudentId", "Title", "Year" },
                values: new object[,]
                {
                    { "proposal1", new DateTime(2024, 7, 15, 8, 23, 50, 0, DateTimeKind.Unspecified), 0, "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy", "1-emCUoUVFDOTgwWn0YAdWGf7xz_2cjOj", 89m, 1, 1, 1, "A21EC0149", "Attention Is All You Need", 1 },
                    { "proposal2", new DateTime(2024, 7, 14, 7, 13, 50, 0, DateTimeKind.Unspecified), 0, "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy", "1EZ8hzuu1dx5TtjLvQyAihEW6YRC3HJpt", null, 2, 1, 1, "A21EC0149", "GPT", 1 },
                    { "proposal3", new DateTime(2024, 7, 13, 10, 56, 51, 0, DateTimeKind.Unspecified), 0, "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy", "1iSMSMqsudSCIsOUwxhS1aqIwAItqPlKc", null, 3, 1, 1, "A21EC0149", "Bert", 1 },
                    { "proposal4", new DateTime(2024, 7, 1, 8, 0, 32, 0, DateTimeKind.Unspecified), 0, "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy", "1HFVTio-i4xXXScDc8PBEmRYfbQZKGeiP", null, 0, 1, 1, "A21EC0149", "Chain Of Thought", 1 },
                    { "proposal5", new DateTime(2024, 6, 15, 12, 23, 21, 0, DateTimeKind.Unspecified), 1, "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy", "1-emCUoUVFDOTgwWn0YAdWGf7xz_2cjOj", null, 0, 1, 2, "A21EC0197", "Attention Is All You Need", 1 },
                    { "proposal6", new DateTime(2024, 6, 24, 11, 11, 29, 0, DateTimeKind.Unspecified), 1, "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy", "1EZ8hzuu1dx5TtjLvQyAihEW6YRC3HJpt", null, 3, 2, 3, "A21EC0127", "GPT", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplySupervisors_MatricId",
                table: "ApplySupervisors",
                column: "MatricId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplySupervisors_SupervisorId",
                table: "ApplySupervisors",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EvaluatorId",
                table: "Comments",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProposalId",
                table: "Comments",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_AcademicProgramId_LecturerId",
                table: "Committees",
                columns: new[] { "AcademicProgramId", "LecturerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Committees_LecturerId",
                table: "Committees",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_AcademicProgramId",
                table: "Lecturers",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_StaffId",
                table: "Lecturers",
                column: "StaffId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_StudentId",
                table: "Proposals",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AcademicProgramId",
                table: "Students",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FirstEvaluatorId",
                table: "Students",
                column: "FirstEvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MatricId",
                table: "Students",
                column: "MatricId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SecondEvaluatorId",
                table: "Students",
                column: "SecondEvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SupervisorId",
                table: "Students",
                column: "SupervisorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "ApplySupervisors");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "AcademicPrograms");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
