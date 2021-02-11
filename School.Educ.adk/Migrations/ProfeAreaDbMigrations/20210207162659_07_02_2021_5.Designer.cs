﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using School.Educ.adk.Areas.ProfeArea.Data;

namespace School.Educ.adk.Migrations.ProfeAreaDbMigrations
{
    [DbContext(typeof(ProfeAreaDb))]
    [Migration("20210207162659_07_02_2021_5")]
    partial class _07_02_2021_5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Affectation", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAffectation");

                    b.Property<string>("Description");

                    b.Property<string>("IdEcole");

                    b.Property<string>("InspecteurID");

                    b.Property<string>("PeriodeAffectectation")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("InspecteurID")
                        .IsUnique()
                        .HasFilter("[InspecteurID] IS NOT NULL");

                    b.ToTable("Affectation");
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

                    b.ToTable("Inspecteur");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Classe", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnneeScolaire")
                        .IsRequired();

                    b.Property<string>("EcoleID");

                    b.Property<string>("Niveau")
                        .IsRequired();

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

                    b.Property<string>("ProfesseurID")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("ClasseID");

                    b.HasIndex("ProfesseurID");

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

                    b.Property<string>("EcoleID");

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

                    b.ToTable("Eleve");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Inscription", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("School.Educ.adk.Areas.Inspection.Models.Evaluer", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cotation");

                    b.Property<string>("InpecteurID");

                    b.Property<string>("LeconID");

                    b.Property<string>("Remarque");

                    b.HasKey("ID");

                    b.HasIndex("InpecteurID");

                    b.HasIndex("LeconID")
                        .IsUnique()
                        .HasFilter("[LeconID] IS NOT NULL");

                    b.ToTable("Evaluer");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.ProfeArea.Models.CahierCote", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CoursID")
                        .IsRequired();

                    b.Property<string>("Periode")
                        .IsRequired();

                    b.Property<double>("Total");

                    b.HasKey("ID");

                    b.HasIndex("CoursID");

                    b.ToTable("CahierCotes");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.ProfeArea.Models.Cotation", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EleveID")
                        .IsRequired();

                    b.Property<string>("EpreuveID");

                    b.Property<double>("Point");

                    b.HasKey("ID");

                    b.HasIndex("EleveID");

                    b.HasIndex("EpreuveID");

                    b.ToTable("Cotations");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.ProfeArea.Models.Epreuve", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CahierCoteID");

                    b.Property<DateTime>("DateEpreuve");

                    b.Property<string>("Description")
                        .HasMaxLength(50);

                    b.Property<double>("Total");

                    b.HasKey("ID");

                    b.HasIndex("CahierCoteID");

                    b.ToTable("Epreuves");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.ProfeArea.Models.Lecon", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CoursID");

                    b.Property<DateTime>("DateLecon");

                    b.Property<string>("LeconDonnee");

                    b.Property<string>("ProfesseurID");

                    b.HasKey("ID");

                    b.HasIndex("CoursID");

                    b.HasIndex("ProfesseurID");

                    b.ToTable("Lecons");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Admin.Models.Affectation", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Admin.Models.Inspecteur", "Inspecteur")
                        .WithOne("Affectation")
                        .HasForeignKey("School.Educ.adk.Areas.Admin.Models.Affectation", "InspecteurID");
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

                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Professeur", "Professeur")
                        .WithMany("Cours")
                        .HasForeignKey("ProfesseurID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Ecole", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Directeur", "Directeur")
                        .WithOne("Ecole")
                        .HasForeignKey("School.Educ.adk.Areas.Ecole.Models.Ecole", "DirecteurID");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.Ecole.Models.Eleve", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Ecole", "Ecole")
                        .WithMany("Eleves")
                        .HasForeignKey("EcoleID");
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

            modelBuilder.Entity("School.Educ.adk.Areas.Inspection.Models.Evaluer", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Admin.Models.Inspecteur", "Inpecteur")
                        .WithMany()
                        .HasForeignKey("InpecteurID");

                    b.HasOne("School.Educ.adk.Areas.ProfeArea.Models.Lecon", "Lecon")
                        .WithOne("Evaluer")
                        .HasForeignKey("School.Educ.adk.Areas.Inspection.Models.Evaluer", "LeconID");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.ProfeArea.Models.CahierCote", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Cours", "Cours")
                        .WithMany()
                        .HasForeignKey("CoursID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("School.Educ.adk.Areas.ProfeArea.Models.Cotation", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Eleve", "Eleve")
                        .WithMany()
                        .HasForeignKey("EleveID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("School.Educ.adk.Areas.ProfeArea.Models.Epreuve", "Epreuve")
                        .WithMany("Cotations")
                        .HasForeignKey("EpreuveID");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.ProfeArea.Models.Epreuve", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.ProfeArea.Models.CahierCote", "CahierCote")
                        .WithMany("Epreuves")
                        .HasForeignKey("CahierCoteID");
                });

            modelBuilder.Entity("School.Educ.adk.Areas.ProfeArea.Models.Lecon", b =>
                {
                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Cours", "Cours")
                        .WithMany()
                        .HasForeignKey("CoursID");

                    b.HasOne("School.Educ.adk.Areas.Ecole.Models.Professeur", "Professeur")
                        .WithMany()
                        .HasForeignKey("ProfesseurID");
                });
#pragma warning restore 612, 618
        }
    }
}