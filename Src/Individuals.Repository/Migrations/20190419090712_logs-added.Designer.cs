﻿// <auto-generated />
using System;
using Individuals.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Individuals.Persistance.Migrations
{
    [DbContext(typeof(IndividualsDBContext))]
    [Migration("20190419090712_logs-added")]
    partial class logsadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Individuals.Domain.Domains.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Tbilisi"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Batumi"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Kutaisi"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Telavi"
                        });
                });

            modelBuilder.Entity("Individuals.Domain.Domains.Individual", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CityId");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<int>("ImageBlobId");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PersonalNumber")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Individuals");
                });

            modelBuilder.Entity("Individuals.Domain.Domains.Log", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Callsite");

                    b.Property<string>("Exception");

                    b.Property<string>("Level");

                    b.Property<string>("Logged");

                    b.Property<string>("Logger");

                    b.Property<string>("MachineName");

                    b.Property<string>("Message");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Individuals.Domain.Entities.ConnectedIndividual", b =>
                {
                    b.Property<long>("ConnectedFromIndividualId");

                    b.Property<long>("ConnectedToIndividualId");

                    b.Property<int>("ConnectionType");

                    b.HasKey("ConnectedFromIndividualId", "ConnectedToIndividualId");

                    b.HasIndex("ConnectedToIndividualId");

                    b.ToTable("ConnectedIndividuals");
                });

            modelBuilder.Entity("Individuals.Domain.ValueObjects.PhoneNumber", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("IndividualId");

                    b.Property<string>("Number")
                        .IsRequired();

                    b.Property<int>("NumberType");

                    b.HasKey("Id");

                    b.HasIndex("IndividualId");

                    b.ToTable("PhoneNumbers");
                });

            modelBuilder.Entity("Individuals.Domain.Domains.Individual", b =>
                {
                    b.HasOne("Individuals.Domain.Domains.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Individuals.Domain.Entities.ConnectedIndividual", b =>
                {
                    b.HasOne("Individuals.Domain.Domains.Individual", "ConnectedFromIndividual")
                        .WithMany("ConnectedIndividuals")
                        .HasForeignKey("ConnectedFromIndividualId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Individuals.Domain.Domains.Individual", "ConnectedToIndividual")
                        .WithMany()
                        .HasForeignKey("ConnectedToIndividualId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Individuals.Domain.ValueObjects.PhoneNumber", b =>
                {
                    b.HasOne("Individuals.Domain.Domains.Individual")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("IndividualId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
