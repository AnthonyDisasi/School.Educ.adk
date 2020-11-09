﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using School.Educ.adk.Areas.Ecole.DataContext;

namespace School.Educ.adk.Migrations
{
    [DbContext(typeof(DbEcole))]
    partial class DbEcoleModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Categorie", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("categories");
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

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Cours", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Categorie");

                    b.Property<string>("ClasseID");

                    b.Property<string>("Intituler");

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

                    b.Property<string>("Postnom")
                        .IsRequired();

                    b.Property<string>("Prenom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Directeurs");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Ecole", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DirecteurID");

                    b.Property<string>("EcoleLatitude");

                    b.Property<string>("EcoleLongitude");

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<string>("SousDivision")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("DirecteurID")
                        .IsUnique()
                        .HasFilter("[DirecteurID] IS NOT NULL");

                    b.ToTable("Ecoles");
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

                    b.Property<string>("Postnom")
                        .IsRequired();

                    b.Property<string>("Prenom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Eleves");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Inscription", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnneeScolaire");

                    b.Property<string>("ClasseID");

                    b.Property<DateTime>("DateInscription");

                    b.Property<string>("EleveId");

                    b.HasKey("ID");

                    b.HasIndex("ClasseID");

                    b.HasIndex("EleveId");

                    b.ToTable("Inscriptions");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Option", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Options");
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

                    b.Property<string>("Postnom")
                        .IsRequired();

                    b.Property<string>("Prenom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("EcoleID");

                    b.ToTable("Professeurs");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Section", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.SousDivision", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("SousDivisions");
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
