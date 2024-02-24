using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BDTheque.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:public.french_ci_ai", "fr_fr-u-ks-level1,fr_fr-u-ks-level1,icu,False")
                .Annotation("Npgsql:Enum:metier", "Scenariste,Dessinateur,Coloriste")
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.CreateTable(
                name: "editeurs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nom = table.Column<string>(type: "text", nullable: false, collation: "french_ci_ai"),
                    nom_raw = table.Column<string>(type: "text", nullable: false, computedColumnSql: "(nom COLLATE \"fr-x-icu\")", stored: true),
                    site_web = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    associations = table.Column<string[]>(type: "text[]", nullable: true),
                    initiale = table.Column<char>(type: "character(1)", maxLength: 1, nullable: false, computedColumnSql: "(upper(nom))::character(1)", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_editeurs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nom = table.Column<string>(type: "text", nullable: false, collation: "french_ci_ai"),
                    nom_raw = table.Column<string>(type: "text", nullable: false, computedColumnSql: "(nom COLLATE \"fr-x-icu\")", stored: true),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    associations = table.Column<string[]>(type: "text[]", nullable: true),
                    initiale = table.Column<char>(type: "character(1)", maxLength: 1, nullable: false, computedColumnSql: "(upper(nom))::character(1)", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "options",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category = table.Column<int>(type: "integer", nullable: false),
                    libelle = table.Column<string>(type: "text", nullable: false, collation: "french_ci_ai"),
                    ordre = table.Column<int>(type: "integer", nullable: false),
                    defaut = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_options", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "personnes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nom = table.Column<string>(type: "text", nullable: false, collation: "french_ci_ai"),
                    nom_raw = table.Column<string>(type: "text", nullable: false, computedColumnSql: "(nom COLLATE \"fr-x-icu\")", stored: true),
                    biographie = table.Column<string>(type: "text", nullable: true, collation: "french_ci_ai"),
                    biographie_raw = table.Column<string>(type: "text", nullable: true, computedColumnSql: "(biographie COLLATE \"fr-x-icu\")", stored: true),
                    site_web = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    associations = table.Column<string[]>(type: "text[]", nullable: true),
                    initiale = table.Column<char>(type: "character(1)", maxLength: 1, nullable: false, computedColumnSql: "(upper(nom))::character(1)", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personnes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "univers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nom = table.Column<string>(type: "text", nullable: false, collation: "french_ci_ai"),
                    nom_raw = table.Column<string>(type: "text", nullable: false, computedColumnSql: "(nom COLLATE \"fr-x-icu\")", stored: true),
                    description = table.Column<string>(type: "text", nullable: true, collation: "french_ci_ai"),
                    description_raw = table.Column<string>(type: "text", nullable: true, computedColumnSql: "(description COLLATE \"fr-x-icu\")", stored: true),
                    site_web = table.Column<string>(type: "text", nullable: true),
                    univers_racine_id = table.Column<Guid>(type: "uuid", nullable: false),
                    univers_parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    univers_branches = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    associations = table.Column<string[]>(type: "text[]", nullable: true),
                    initiale = table.Column<char>(type: "character(1)", maxLength: 1, nullable: false, computedColumnSql: "(upper(nom))::character(1)", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_univers", x => x.id);
                    table.ForeignKey(
                        name: "FK_univers_univers_univers_parent_id",
                        column: x => x.univers_parent_id,
                        principalTable: "univers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_univers_univers_univers_racine_id",
                        column: x => x.univers_racine_id,
                        principalTable: "univers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "collections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nom = table.Column<string>(type: "text", nullable: false, collation: "french_ci_ai"),
                    nom_raw = table.Column<string>(type: "text", nullable: false, computedColumnSql: "(nom COLLATE \"fr-x-icu\")", stored: true),
                    editeur_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    associations = table.Column<string[]>(type: "text[]", nullable: true),
                    initiale = table.Column<char>(type: "character(1)", maxLength: 1, nullable: false, computedColumnSql: "(upper(nom))::character(1)", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collections", x => x.id);
                    table.UniqueConstraint("AK_collections_editeur_id_id", x => new { x.editeur_id, x.id });
                    table.ForeignKey(
                        name: "FK_collections_editeurs_editeur_id",
                        column: x => x.editeur_id,
                        principalTable: "editeurs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "editions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    annee_edition = table.Column<int>(type: "integer", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true, collation: "french_ci_ai"),
                    notes_raw = table.Column<string>(type: "text", nullable: true, computedColumnSql: "(notes COLLATE \"fr-x-icu\")", stored: true),
                    isbn = table.Column<string>(type: "text", nullable: true),
                    nombre_de_pages = table.Column<int>(type: "integer", nullable: true),
                    couleur = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    vo = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    reliure_id = table.Column<int>(type: "integer", nullable: true),
                    format_edition_id = table.Column<int>(type: "integer", nullable: true),
                    type_edition_id = table.Column<int>(type: "integer", nullable: true),
                    orientation_id = table.Column<int>(type: "integer", nullable: true),
                    sens_lecture_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_editions", x => x.id);
                    table.ForeignKey(
                        name: "FK_editions_options_format_edition_id",
                        column: x => x.format_edition_id,
                        principalTable: "options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_editions_options_orientation_id",
                        column: x => x.orientation_id,
                        principalTable: "options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_editions_options_reliure_id",
                        column: x => x.reliure_id,
                        principalTable: "options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_editions_options_sens_lecture_id",
                        column: x => x.sens_lecture_id,
                        principalTable: "options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_editions_options_type_edition_id",
                        column: x => x.type_edition_id,
                        principalTable: "options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "auteurs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    personne_id = table.Column<Guid>(type: "uuid", nullable: false),
                    metier = table.Column<int>(type: "metier", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auteurs", x => x.id);
                    table.ForeignKey(
                        name: "FK_auteurs_personnes_personne_id",
                        column: x => x.personne_id,
                        principalTable: "personnes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "series",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    titre = table.Column<string>(type: "text", nullable: true, collation: "french_ci_ai"),
                    titre_raw = table.Column<string>(type: "text", nullable: true, computedColumnSql: "(titre COLLATE \"fr-x-icu\")", stored: true),
                    sujet = table.Column<string>(type: "text", nullable: true, collation: "french_ci_ai"),
                    sujet_raw = table.Column<string>(type: "text", nullable: true, computedColumnSql: "(sujet COLLATE \"fr-x-icu\")", stored: true),
                    notes = table.Column<string>(type: "text", nullable: true, collation: "french_ci_ai"),
                    notes_raw = table.Column<string>(type: "text", nullable: true, computedColumnSql: "(notes COLLATE \"fr-x-icu\")", stored: true),
                    site_web = table.Column<string>(type: "text", nullable: true),
                    editeur_id = table.Column<Guid>(type: "uuid", nullable: true),
                    collection_id = table.Column<Guid>(type: "uuid", nullable: true),
                    modele_edition_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nb_albums = table.Column<int>(type: "integer", nullable: true),
                    terminee = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    complete = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    suivre_sorties = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    suivre_manquants = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    notation_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    associations = table.Column<string[]>(type: "text[]", nullable: true),
                    initiale = table.Column<char>(type: "character(1)", maxLength: 1, nullable: true, computedColumnSql: "(upper(titre))::character(1)", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_series", x => x.id);
                    table.ForeignKey(
                        name: "FK_series_collections_editeur_id_collection_id",
                        columns: x => new { x.editeur_id, x.collection_id },
                        principalTable: "collections",
                        principalColumns: new[] { "editeur_id", "id" },
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_series_editeurs_editeur_id",
                        column: x => x.editeur_id,
                        principalTable: "editeurs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_series_editions_modele_edition_id",
                        column: x => x.modele_edition_id,
                        principalTable: "editions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_series_options_notation_id",
                        column: x => x.notation_id,
                        principalTable: "options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "albums",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    titre = table.Column<string>(type: "text", nullable: true, collation: "french_ci_ai"),
                    titre_raw = table.Column<string>(type: "text", nullable: true, computedColumnSql: "(titre COLLATE \"fr-x-icu\")", stored: true),
                    sujet = table.Column<string>(type: "text", nullable: true, collation: "french_ci_ai"),
                    sujet_raw = table.Column<string>(type: "text", nullable: true, computedColumnSql: "(sujet COLLATE \"fr-x-icu\")", stored: true),
                    notes = table.Column<string>(type: "text", nullable: true, collation: "french_ci_ai"),
                    notes_raw = table.Column<string>(type: "text", nullable: true, computedColumnSql: "(notes COLLATE \"fr-x-icu\")", stored: true),
                    hors_serie = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    tome = table.Column<int>(type: "integer", nullable: true),
                    integrale = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    tome_debut = table.Column<int>(type: "integer", nullable: true),
                    tome_fin = table.Column<int>(type: "integer", nullable: true),
                    mois_parution = table.Column<int>(type: "integer", nullable: true),
                    annee_parution = table.Column<int>(type: "integer", nullable: true),
                    serie_id = table.Column<Guid>(type: "uuid", nullable: true),
                    notation_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    associations = table.Column<string[]>(type: "text[]", nullable: true),
                    initiale = table.Column<char>(type: "character(1)", maxLength: 1, nullable: true, computedColumnSql: "(upper(titre))::character(1)", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albums", x => x.id);
                    table.ForeignKey(
                        name: "FK_albums_options_notation_id",
                        column: x => x.notation_id,
                        principalTable: "options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_albums_series_serie_id",
                        column: x => x.serie_id,
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "auteurs_series",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    auteur_id = table.Column<Guid>(type: "uuid", nullable: false),
                    serie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auteurs_series", x => x.id);
                    table.ForeignKey(
                        name: "FK_auteurs_series_auteurs_auteur_id",
                        column: x => x.auteur_id,
                        principalTable: "auteurs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_auteurs_series_series_serie_id",
                        column: x => x.serie_id,
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "genres_series",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false),
                    serie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres_series", x => x.id);
                    table.ForeignKey(
                        name: "FK_genres_series_genres_genre_id",
                        column: x => x.genre_id,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_genres_series_series_serie_id",
                        column: x => x.serie_id,
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "univers_series",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    univers_id = table.Column<Guid>(type: "uuid", nullable: false),
                    serie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_univers_series", x => x.id);
                    table.ForeignKey(
                        name: "FK_univers_series_series_serie_id",
                        column: x => x.serie_id,
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_univers_series_univers_univers_id",
                        column: x => x.univers_id,
                        principalTable: "univers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "auteurs_albums",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    album_id = table.Column<Guid>(type: "uuid", nullable: false),
                    auteur_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auteurs_albums", x => x.id);
                    table.ForeignKey(
                        name: "FK_auteurs_albums_albums_album_id",
                        column: x => x.album_id,
                        principalTable: "albums",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_auteurs_albums_auteurs_auteur_id",
                        column: x => x.auteur_id,
                        principalTable: "auteurs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "editions_albums",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    edition_id = table.Column<Guid>(type: "uuid", nullable: false),
                    album_id = table.Column<Guid>(type: "uuid", nullable: false),
                    editeur_id = table.Column<Guid>(type: "uuid", nullable: false),
                    collection_id = table.Column<Guid>(type: "uuid", nullable: true),
                    etat_id = table.Column<int>(type: "integer", nullable: true),
                    stock = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    offert = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    occasion = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    gratuit = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    date_achat = table.Column<DateOnly>(type: "date", nullable: true),
                    prix = table.Column<decimal>(type: "numeric(8,3)", precision: 8, scale: 3, nullable: true),
                    dedicace = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    numero_perso = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_editions_albums", x => x.id);
                    table.ForeignKey(
                        name: "FK_editions_albums_albums_album_id",
                        column: x => x.album_id,
                        principalTable: "albums",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_editions_albums_collections_editeur_id_collection_id",
                        columns: x => new { x.editeur_id, x.collection_id },
                        principalTable: "collections",
                        principalColumns: new[] { "editeur_id", "id" });
                    table.ForeignKey(
                        name: "FK_editions_albums_editeurs_editeur_id",
                        column: x => x.editeur_id,
                        principalTable: "editeurs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_editions_albums_editions_edition_id",
                        column: x => x.edition_id,
                        principalTable: "editions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_editions_albums_options_etat_id",
                        column: x => x.etat_id,
                        principalTable: "options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "genres_albums",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false),
                    album_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_serie = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres_albums", x => x.id);
                    table.ForeignKey(
                        name: "FK_genres_albums_albums_album_id",
                        column: x => x.album_id,
                        principalTable: "albums",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_genres_albums_genres_genre_id",
                        column: x => x.genre_id,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "univers_albums",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    univers_id = table.Column<Guid>(type: "uuid", nullable: false),
                    album_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_serie = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_univers_albums", x => x.id);
                    table.ForeignKey(
                        name: "FK_univers_albums_albums_album_id",
                        column: x => x.album_id,
                        principalTable: "albums",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_univers_albums_univers_univers_id",
                        column: x => x.univers_id,
                        principalTable: "univers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cotes_editions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    edition_album_id = table.Column<Guid>(type: "uuid", nullable: false),
                    annee = table.Column<int>(type: "integer", nullable: false),
                    prix = table.Column<decimal>(type: "numeric(8,3)", precision: 8, scale: 3, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cotes_editions", x => x.id);
                    table.ForeignKey(
                        name: "FK_cotes_editions_editions_albums_edition_album_id",
                        column: x => x.edition_album_id,
                        principalTable: "editions_albums",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    titre = table.Column<string>(type: "text", nullable: true, collation: "french_ci_ai"),
                    bytes = table.Column<byte[]>(type: "bytea", nullable: false),
                    ordre = table.Column<int>(type: "integer", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    edition_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(3) with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_images_editions_albums_edition_id",
                        column: x => x.edition_id,
                        principalTable: "editions_albums",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_images_options_type_id",
                        column: x => x.type_id,
                        principalTable: "options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "genres",
                columns: new[] { "id", "associations", "created_at", "nom", "updated_at" },
                values: new object[,]
                {
                    { new Guid("0d86e9c7-cfd2-43f5-ab3b-e004fb7de812"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6319), "Sport", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6320) },
                    { new Guid("10c607d5-6f20-44d5-b8a6-060a2bceddbb"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6352), "Romance", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6353) },
                    { new Guid("1f03cc8f-b16f-4d4c-b886-3ebc553fb917"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6293), "Ésotérisme", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6294) },
                    { new Guid("294d78f5-8cc9-42ae-bbac-495079d812b8"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6099), "Mythologie", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6099) },
                    { new Guid("2e1f9807-07d8-4036-8503-d5661660d8e6"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6129), "Hommage", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6129) },
                    { new Guid("37f12eb3-0a8a-4ba4-b890-533c2d82cdeb"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6336), "Jeunesse", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6336) },
                    { new Guid("3953f72d-208d-49c5-8a12-265f3b735b9e"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6261), "Road movie", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6261) },
                    { new Guid("411b7fb5-3474-45eb-8bd5-4e1f67291438"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6188), "Animation", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6188) },
                    { new Guid("412f4f5e-309d-4f69-85b7-a018ffa49f71"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6088), "Héroïque fantaisie", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6088) },
                    { new Guid("5858be6d-a72b-4b77-a464-80c2410fa38a"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6311), "Érotique", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6311) },
                    { new Guid("59d1f562-5c18-41cd-8eb8-53f259c0a2c9"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6147), "Horreur", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6147) },
                    { new Guid("678307d8-862b-4f94-88d8-b8a6962e8292"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6079), "Humour", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6079) },
                    { new Guid("6d5d61fa-4a8d-42de-ab51-406ff7c913ab"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6285), "Espionnage", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6285) },
                    { new Guid("8825a97c-7b24-4d30-96ce-68a1e30f801a"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6163), "Science-Fiction", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6164) },
                    { new Guid("9509f256-8c4c-4080-ade5-c80b4cb62eb1"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6252), "Comédie musicale", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6252) },
                    { new Guid("95c8f82f-b9bd-47df-b6c3-d724837a8703"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6269), "Historique", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6269) },
                    { new Guid("9d8f75cb-feac-48af-86ae-f31c737d1b3f"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6067), "Aventures", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6067) },
                    { new Guid("a59d7520-fa3b-41f9-9aac-957f1efd6766"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6328), "Roman graphique", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6328) },
                    { new Guid("a9390924-20d5-4bf0-940a-95272359eefb"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6242), "Comédie dramatique", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6242) },
                    { new Guid("bc7157cc-3883-46e9-adc8-bacfc7710824"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6361), "Pirates", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6361) },
                    { new Guid("c5ed5c16-bcfa-4b14-a0c6-f873db9e8aab"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6172), "Western", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6172) },
                    { new Guid("c88e9abd-a9f8-410e-9897-6b2240be664e"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6137), "Guerre", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6137) },
                    { new Guid("c8a3b392-5bfc-439a-a235-082abe5c0205"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6112), "Enfant", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6112) },
                    { new Guid("c9182466-4a97-4a16-a9f5-204a527766c6"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(5951), "Action", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(5952) },
                    { new Guid("cf4a1427-90cf-4b57-ab3b-237d066e5173"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6231), "Comédie", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6231) },
                    { new Guid("d6ec9b1e-1340-413d-a98e-fdb4eb80fc91"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6302), "Blog", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6303) },
                    { new Guid("dad45ccb-19e4-4f15-bc2b-b1706d0994a5"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6344), "Comics", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6344) },
                    { new Guid("dcc27058-f9e4-4b08-a9b5-1b099da8d13c"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6155), "Policier", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6155) },
                    { new Guid("e812da51-345a-4718-99fa-e11e9bf33f1c"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6196), "Manga", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6196) },
                    { new Guid("eeff3beb-70c4-4ec0-811d-0c51a0bd008b"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6120), "Fantastique", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6121) },
                    { new Guid("f710a4af-49fb-4e9e-b062-5c5a9dd21931"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6277), "Thriller", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6277) },
                    { new Guid("fdac06e3-14ba-4801-9064-b6d87527f7c6"), new string[0], new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6179), "Anticipation", new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6180) }
                });

            migrationBuilder.InsertData(
                table: "options",
                columns: new[] { "id", "category", "created_at", "defaut", "libelle", "ordre", "updated_at" },
                values: new object[,]
                {
                    { 100, 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6632), false, "Très mauvais", 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6633) },
                    { 103, 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6722), false, "Mauvais", 2, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6723) },
                    { 105, 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6740), false, "Bon", 3, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6740) },
                    { 108, 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6754), false, "Très bon", 4, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6754) },
                    { 110, 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6768), true, "Excellent (neuf)", 5, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6768) },
                    { 200, 2, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6833), true, "Cartonné", 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6834) },
                    { 201, 2, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6849), false, "Broché", 2, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6849) },
                    { 301, 3, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6863), false, "Première édition", 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6863) },
                    { 302, 3, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6878), false, "Edition spéciale", 2, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6878) },
                    { 303, 3, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6894), false, "Tirage de tête", 3, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6894) },
                    { 401, 4, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6908), true, "Portrait", 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6908) },
                    { 402, 4, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6925), false, "Italienne", 2, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6925) },
                    { 501, 5, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6939), false, "Poche", 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6939) },
                    { 503, 5, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6952), false, "Moyen (A5)", 2, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6952) },
                    { 504, 5, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6966), true, "Normal (A4)", 3, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6966) },
                    { 505, 5, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6979), false, "Grand (A3)", 4, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6979) },
                    { 506, 5, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6992), false, "Très grand (A2)", 5, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(6992) },
                    { 510, 5, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7007), false, "Spécial", 6, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7007) },
                    { 600, 6, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7021), true, "Couverture", 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7021) },
                    { 601, 6, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7047), false, "Planche", 4, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7047) },
                    { 602, 6, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7086), false, "4ème de couverture", 5, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7086) },
                    { 603, 6, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7034), false, "Page de garde", 3, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7034) },
                    { 604, 6, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7102), false, "Dédicace", 10, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7102) },
                    { 801, 8, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7116), true, "Gauche à droite", 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7117) },
                    { 802, 8, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7130), false, "Droite à gauche", 2, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7130) },
                    { 900, 9, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7143), true, "Pas noté", 1, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7144) },
                    { 901, 9, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7156), false, "Très mauvais", 2, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7157) },
                    { 902, 9, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7170), false, "Mauvais", 3, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7170) },
                    { 903, 9, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7183), false, "Moyen", 4, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7183) },
                    { 904, 9, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7196), false, "Bien", 5, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7196) },
                    { 905, 9, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7209), false, "Très bien", 6, new DateTime(2024, 2, 24, 16, 32, 16, 552, DateTimeKind.Utc).AddTicks(7209) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_albums_notation_id",
                table: "albums",
                column: "notation_id");

            migrationBuilder.CreateIndex(
                name: "IX_albums_serie_id",
                table: "albums",
                column: "serie_id");

            migrationBuilder.CreateIndex(
                name: "IX_albums_titre",
                table: "albums",
                column: "titre")
                .Annotation("Relational:Collation", new[] { "french_ci_ai" });

            migrationBuilder.CreateIndex(
                name: "IX_albums_titre_raw",
                table: "albums",
                column: "titre_raw");

            migrationBuilder.CreateIndex(
                name: "IX_auteurs_personne_id",
                table: "auteurs",
                column: "personne_id");

            migrationBuilder.CreateIndex(
                name: "IX_auteurs_albums_album_id",
                table: "auteurs_albums",
                column: "album_id");

            migrationBuilder.CreateIndex(
                name: "IX_auteurs_albums_auteur_id_album_id",
                table: "auteurs_albums",
                columns: new[] { "auteur_id", "album_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auteurs_series_auteur_id_serie_id",
                table: "auteurs_series",
                columns: new[] { "auteur_id", "serie_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auteurs_series_serie_id",
                table: "auteurs_series",
                column: "serie_id");

            migrationBuilder.CreateIndex(
                name: "IX_collections_editeur_id_id",
                table: "collections",
                columns: new[] { "editeur_id", "id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_collections_editeur_id_nom",
                table: "collections",
                columns: new[] { "editeur_id", "nom" },
                unique: true)
                .Annotation("Relational:Collation", new[] { null, "french_ci_ai" });

            migrationBuilder.CreateIndex(
                name: "IX_collections_nom",
                table: "collections",
                column: "nom")
                .Annotation("Relational:Collation", new[] { "french_ci_ai" });

            migrationBuilder.CreateIndex(
                name: "IX_collections_nom_raw",
                table: "collections",
                column: "nom_raw");

            migrationBuilder.CreateIndex(
                name: "IX_cotes_editions_edition_album_id_annee",
                table: "cotes_editions",
                columns: new[] { "edition_album_id", "annee" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_editeurs_nom",
                table: "editeurs",
                column: "nom",
                unique: true)
                .Annotation("Relational:Collation", new[] { "french_ci_ai" });

            migrationBuilder.CreateIndex(
                name: "IX_editeurs_nom_raw",
                table: "editeurs",
                column: "nom_raw");

            migrationBuilder.CreateIndex(
                name: "IX_editions_format_edition_id",
                table: "editions",
                column: "format_edition_id");

            migrationBuilder.CreateIndex(
                name: "IX_editions_orientation_id",
                table: "editions",
                column: "orientation_id");

            migrationBuilder.CreateIndex(
                name: "IX_editions_reliure_id",
                table: "editions",
                column: "reliure_id");

            migrationBuilder.CreateIndex(
                name: "IX_editions_sens_lecture_id",
                table: "editions",
                column: "sens_lecture_id");

            migrationBuilder.CreateIndex(
                name: "IX_editions_type_edition_id",
                table: "editions",
                column: "type_edition_id");

            migrationBuilder.CreateIndex(
                name: "IX_editions_albums_album_id_id",
                table: "editions_albums",
                columns: new[] { "album_id", "id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_editions_albums_editeur_id_collection_id",
                table: "editions_albums",
                columns: new[] { "editeur_id", "collection_id" });

            migrationBuilder.CreateIndex(
                name: "IX_editions_albums_edition_id",
                table: "editions_albums",
                column: "edition_id");

            migrationBuilder.CreateIndex(
                name: "IX_editions_albums_etat_id",
                table: "editions_albums",
                column: "etat_id");

            migrationBuilder.CreateIndex(
                name: "IX_genres_nom",
                table: "genres",
                column: "nom",
                unique: true)
                .Annotation("Relational:Collation", new[] { "french_ci_ai" });

            migrationBuilder.CreateIndex(
                name: "IX_genres_nom_raw",
                table: "genres",
                column: "nom_raw");

            migrationBuilder.CreateIndex(
                name: "IX_genres_albums_album_id",
                table: "genres_albums",
                column: "album_id");

            migrationBuilder.CreateIndex(
                name: "IX_genres_albums_genre_id_album_id",
                table: "genres_albums",
                columns: new[] { "genre_id", "album_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_genres_series_genre_id_serie_id",
                table: "genres_series",
                columns: new[] { "genre_id", "serie_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_genres_series_serie_id",
                table: "genres_series",
                column: "serie_id");

            migrationBuilder.CreateIndex(
                name: "IX_images_edition_id_type_id_ordre",
                table: "images",
                columns: new[] { "edition_id", "type_id", "ordre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_images_type_id",
                table: "images",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_options_category_libelle",
                table: "options",
                columns: new[] { "category", "libelle" },
                unique: true)
                .Annotation("Relational:Collation", new[] { null, "french_ci_ai" });

            migrationBuilder.CreateIndex(
                name: "IX_options_category_ordre",
                table: "options",
                columns: new[] { "category", "ordre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personnes_nom",
                table: "personnes",
                column: "nom",
                unique: true)
                .Annotation("Relational:Collation", new[] { "french_ci_ai" });

            migrationBuilder.CreateIndex(
                name: "IX_personnes_nom_raw",
                table: "personnes",
                column: "nom_raw");

            migrationBuilder.CreateIndex(
                name: "IX_series_editeur_id_collection_id",
                table: "series",
                columns: new[] { "editeur_id", "collection_id" });

            migrationBuilder.CreateIndex(
                name: "IX_series_modele_edition_id",
                table: "series",
                column: "modele_edition_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_series_notation_id",
                table: "series",
                column: "notation_id");

            migrationBuilder.CreateIndex(
                name: "IX_series_titre",
                table: "series",
                column: "titre")
                .Annotation("Relational:Collation", new[] { "french_ci_ai" });

            migrationBuilder.CreateIndex(
                name: "IX_series_titre_raw",
                table: "series",
                column: "titre_raw");

            migrationBuilder.CreateIndex(
                name: "IX_univers_nom",
                table: "univers",
                column: "nom")
                .Annotation("Relational:Collation", new[] { "french_ci_ai" });

            migrationBuilder.CreateIndex(
                name: "IX_univers_nom_raw",
                table: "univers",
                column: "nom_raw");

            migrationBuilder.CreateIndex(
                name: "IX_univers_univers_parent_id",
                table: "univers",
                column: "univers_parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_univers_univers_racine_id",
                table: "univers",
                column: "univers_racine_id");

            migrationBuilder.CreateIndex(
                name: "IX_univers_albums_album_id",
                table: "univers_albums",
                column: "album_id");

            migrationBuilder.CreateIndex(
                name: "IX_univers_albums_univers_id_album_id",
                table: "univers_albums",
                columns: new[] { "univers_id", "album_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_univers_series_serie_id",
                table: "univers_series",
                column: "serie_id");

            migrationBuilder.CreateIndex(
                name: "IX_univers_series_univers_id_serie_id",
                table: "univers_series",
                columns: new[] { "univers_id", "serie_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auteurs_albums");

            migrationBuilder.DropTable(
                name: "auteurs_series");

            migrationBuilder.DropTable(
                name: "cotes_editions");

            migrationBuilder.DropTable(
                name: "genres_albums");

            migrationBuilder.DropTable(
                name: "genres_series");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "univers_albums");

            migrationBuilder.DropTable(
                name: "univers_series");

            migrationBuilder.DropTable(
                name: "auteurs");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "editions_albums");

            migrationBuilder.DropTable(
                name: "univers");

            migrationBuilder.DropTable(
                name: "personnes");

            migrationBuilder.DropTable(
                name: "albums");

            migrationBuilder.DropTable(
                name: "series");

            migrationBuilder.DropTable(
                name: "collections");

            migrationBuilder.DropTable(
                name: "editions");

            migrationBuilder.DropTable(
                name: "editeurs");

            migrationBuilder.DropTable(
                name: "options");
        }
    }
}
