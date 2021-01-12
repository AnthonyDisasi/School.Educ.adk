﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using School.Educ.adk.Areas.Admin.Data;

namespace School.Educ.adk.Migrations.InspecteurDbMigrations
{
    [DbContext(typeof(InspecteurDb))]
    [Migration("20210112155219__12_01_2021_5")]
    partial class _12_01_2021_5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Affectation", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAffectation");

                    b.Property<string>("Description");

                    b.Property<string>("EcoleID");

                    b.Property<string>("InspecteurID");

                    b.Property<string>("PeriodeAffectectation")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("EcoleID")
                        .IsUnique()
                        .HasFilter("[EcoleID] IS NOT NULL");

                    b.HasIndex("InspecteurID")
                        .IsUnique()
                        .HasFilter("[InspecteurID] IS NOT NULL");

                    b.ToTable("Affectations");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Assertion", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Intituler")
                        .IsRequired();

                    b.Property<string>("QuestionID");

                    b.HasKey("ID");

                    b.HasIndex("QuestionID");

                    b.ToTable("Assertions");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Examen", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CodeAcces")
                        .IsRequired();

                    b.Property<DateTime>("DateExamen");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Duree");

                    b.Property<int>("Periode");

                    b.Property<string>("Serie")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Examens");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Inspecteur", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateNaissance");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int?>("Genre");

                    b.Property<string>("Matricule")
                        .IsRequired();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Postnom")
                        .IsRequired();

                    b.Property<string>("Prenom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Inspecteurs");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Participant", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateExamen");

                    b.Property<string>("IdentifiantEleve")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Question", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BonneReponse");

                    b.Property<double>("Cote");

                    b.Property<string>("Enoncer");

                    b.Property<string>("ExamenID")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("ExamenID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Reponse", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ParticipantID")
                        .IsRequired();

                    b.Property<double>("Point");

                    b.Property<string>("QuestionID")
                        .IsRequired();

                    b.Property<string>("ReponseDonnee")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("ParticipantID");

                    b.HasIndex("QuestionID");

                    b.ToTable("Reponses");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Classe", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EcoleID");

                    b.Property<string>("Niveau")
                        .IsRequired();

                    b.Property<string>("Option");

                    b.Property<string>("Section")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("EcoleID");

                    b.ToTable("Classe");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Cours", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Categorie")
                        .IsRequired();

                    b.Property<string>("ClasseID");

                    b.Property<string>("Intituler")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("ClasseID");

                    b.ToTable("Cours");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Directeur", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateNaissance");

                    b.Property<string>("Email");

                    b.Property<int?>("Genre");

                    b.Property<string>("Matricule")
                        .IsRequired();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Postnom")
                        .IsRequired();

                    b.Property<string>("Prenom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Directeur");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Ecole", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreate");

                    b.Property<string>("DirecteurID");

                    b.Property<string>("EcoleLatitude")
                        .IsRequired();

                    b.Property<string>("EcoleLongitude")
                        .IsRequired();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<string>("SousDivision")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("DirecteurID")
                        .IsUnique()
                        .HasFilter("[DirecteurID] IS NOT NULL");

                    b.ToTable("Ecole");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Eleve", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateNaissance");

                    b.Property<int?>("Genre");

                    b.Property<string>("Matricule")
                        .IsRequired();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Postnom")
                        .IsRequired();

                    b.Property<string>("Prenom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Eleve");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Inscription", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnneeScolaire")
                        .IsRequired();

                    b.Property<string>("ClasseID");

                    b.Property<DateTime>("DateInscription");

                    b.Property<string>("EleveId");

                    b.HasKey("ID");

                    b.HasIndex("ClasseID");

                    b.HasIndex("EleveId");

                    b.ToTable("Inscription");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Professeur", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateNaissance");

                    b.Property<string>("EcoleID");

                    b.Property<string>("Email");

                    b.Property<int?>("Genre");

                    b.Property<string>("Matricule")
                        .IsRequired();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Postnom")
                        .IsRequired();

                    b.Property<string>("Prenom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("EcoleID");

                    b.ToTable("Professeur");
                });

            modelBuilder.Entity("School.Educ.adk.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("School.Educ.adk.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("School.Educ.adk.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("School.Educ.adk.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("School.Educ.adk.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Affectation", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Ecole", "Ecole")
                        .WithOne("Affectation")
                        .HasForeignKey("School.Educ.adk.Areas.Admin.Models.Affectation", "EcoleID");

                    b.HasOne("School.Educ.adk.Areas.Admin.Models.Inspecteur", "Inspecteur")
                        .WithOne("Affectation")
                        .HasForeignKey("School.Educ.adk.Areas.Admin.Models.Affectation", "InspecteurID");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Assertion", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Admin.Models.Question", "Question")
                        .WithMany("Assertions")
                        .HasForeignKey("QuestionID");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Question", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Admin.Models.Examen", "Examen")
                        .WithMany("Questions")
                        .HasForeignKey("ExamenID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Reponse", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Admin.Models.Participant", "Participant")
                        .WithMany("Reponses")
                        .HasForeignKey("ParticipantID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("School.Educ.adk.Areas.Admin.Models.Question", "Question")
                        .WithMany("Reponse")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Classe", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Ecole", "Ecole")
                        .WithMany("Classes")
                        .HasForeignKey("EcoleID");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Cours", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Classe", "Classe")
                        .WithMany("Cours")
                        .HasForeignKey("ClasseID");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Ecole", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Directeur", "Directeur")
                        .WithOne("Ecole")
                        .HasForeignKey("School.Educ.adk.Areas.Ecole.Models.Ecole", "DirecteurID");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Inscription", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Classe", "Classe")
                        .WithMany("Inscriptions")
                        .HasForeignKey("ClasseID");

                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Eleve", "Eleve")
                        .WithMany("Inscriptions")
                        .HasForeignKey("EleveId");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Professeur", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Ecole", "Ecole")
                        .WithMany("Professeurs")
                        .HasForeignKey("EcoleID");
                });
#pragma warning restore 612, 618
        }
    }
}