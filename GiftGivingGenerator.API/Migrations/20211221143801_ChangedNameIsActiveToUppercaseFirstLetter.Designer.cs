﻿// <auto-generated />
using System;
using GiftGivingGenerator.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GiftGivingGenerator.API.Migrations
{
    [DbContext(typeof(AppContext))]
    [Migration("20211221143801_ChangedNameIsActiveToUppercaseFirstLetter")]
    partial class ChangedNameIsActiveToUppercaseFirstLetter
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EventPerson", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EventId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("EventPerson");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.DrawingResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GiverPersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecipientPersonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("GiverPersonId");

                    b.HasIndex("RecipientPersonId");

                    b.ToTable("DrawingResults");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Organizer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizer");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("EventPerson", b =>
                {
                    b.HasOne("GiftGivingGenerator.API.Entities.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GiftGivingGenerator.API.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.DrawingResult", b =>
                {
                    b.HasOne("GiftGivingGenerator.API.Entities.Event", "Event")
                        .WithMany("DrawingResults")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GiftGivingGenerator.API.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("GiverPersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GiftGivingGenerator.API.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("RecipientPersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Event", b =>
                {
                    b.HasOne("GiftGivingGenerator.API.Entities.Organizer", "Organizer")
                        .WithMany("Events")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Person", b =>
                {
                    b.HasOne("GiftGivingGenerator.API.Entities.Organizer", "Organizer")
                        .WithMany("Persons")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Event", b =>
                {
                    b.Navigation("DrawingResults");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Organizer", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Persons");
                });
#pragma warning restore 612, 618
        }
    }
}
