using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PetopiaWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatePostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__admin__43AA41416FA8F5DF", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "pet_category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pet_cate__D54EE9B4CF21D854", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone_no = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    user_role = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__B9BE370F1C04A088", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "breed",
                columns: table => new
                {
                    breed_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__breed__9C02143553FA916A", x => x.breed_id);
                    table.ForeignKey(
                        name: "FK__breed__category___3F466844",
                        column: x => x.category_id,
                        principalTable: "pet_category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pet_profile",
                columns: table => new
                {
                    pet_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    breed_id = table.Column<int>(type: "integer", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: true),
                    size = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    weight = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    vaccinated = table.Column<bool>(type: "boolean", nullable: true),
                    spayed = table.Column<bool>(type: "boolean", nullable: true),
                    health = table.Column<string>(type: "text", nullable: true),
                    needs = table.Column<string>(type: "text", nullable: true),
                    house_trained = table.Column<bool>(type: "boolean", nullable: true),
                    good_with_kids = table.Column<bool>(type: "boolean", nullable: true),
                    good_with_pets = table.Column<bool>(type: "boolean", nullable: true),
                    personality = table.Column<string>(type: "text", nullable: true),
                    monthly_expenses = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    isRegisteredWithGovt = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true, defaultValue: "no"),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pet_prof__390CC5FE6B06AC88", x => x.pet_id);
                    table.ForeignKey(
                        name: "FK__pet_profi__breed__4E88ABD4",
                        column: x => x.breed_id,
                        principalTable: "breed",
                        principalColumn: "breed_id");
                    table.ForeignKey(
                        name: "FK__pet_profi__categ__4D94879B",
                        column: x => x.category_id,
                        principalTable: "pet_category",
                        principalColumn: "category_id");
                    table.ForeignKey(
                        name: "FK__pet_profi__user___4F7CD00D",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "adoption_status",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pet_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__adoption__3683B53114E6A628", x => x.status_id);
                    table.ForeignKey(
                        name: "FK__adoption___pet_i__5629CD9C",
                        column: x => x.pet_id,
                        principalTable: "pet_profile",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__adoption___user___571DF1D5",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pet_images",
                columns: table => new
                {
                    image_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pet_id = table.Column<int>(type: "integer", nullable: false),
                    image_path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pet_imag__DC9AC9554D3966CD", x => x.image_id);
                    table.ForeignKey(
                        name: "FK__pet_image__pet_i__52593CB8",
                        column: x => x.pet_id,
                        principalTable: "pet_profile",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__admin__AB6E6164AA1BBB67",
                table: "admin",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_adoption_status_pet_id",
                table: "adoption_status",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "IX_adoption_status_user_id",
                table: "adoption_status",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_breed_category_id",
                table: "breed",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "UQ__pet_cate__72E12F1BF2955269",
                table: "pet_category",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pet_images_pet_id",
                table: "pet_images",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "IX_pet_profile_breed_id",
                table: "pet_profile",
                column: "breed_id");

            migrationBuilder.CreateIndex(
                name: "IX_pet_profile_category_id",
                table: "pet_profile",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_pet_profile_user_id",
                table: "pet_profile",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__users__AB6E616403F5D9A0",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "adoption_status");

            migrationBuilder.DropTable(
                name: "pet_images");

            migrationBuilder.DropTable(
                name: "pet_profile");

            migrationBuilder.DropTable(
                name: "breed");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "pet_category");
        }
    }
}
