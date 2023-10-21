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
    [Migration("20231021174634_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<int?>("Budget")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Events");

                    b.HasAnnotation("Cosmos:ContainerName", "Event");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Exclusion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExcludedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("ExcludedId");

                    b.HasIndex("PersonId");

                    b.ToTable("Exclusions");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.GiftWish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Wish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("PersonId");

                    b.ToTable("GiftWish");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasAnnotation("Cosmos:ContainerName", "Person");
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

                    b.HasOne("GiftGivingGenerator.API.Entities.Person", "GiverPerson")
                        .WithMany()
                        .HasForeignKey("GiverPersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GiftGivingGenerator.API.Entities.Person", "RecipientPerson")
                        .WithMany()
                        .HasForeignKey("RecipientPersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("GiverPerson");

                    b.Navigation("RecipientPerson");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Event", b =>
                {
                    b.HasOne("GiftGivingGenerator.API.Entities.Person", "Organizer")
                        .WithMany("CreatedEvents")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Exclusion", b =>
                {
                    b.HasOne("GiftGivingGenerator.API.Entities.Event", "Event")
                        .WithMany("Exclusions")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GiftGivingGenerator.API.Entities.Person", "Excluded")
                        .WithMany()
                        .HasForeignKey("ExcludedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GiftGivingGenerator.API.Entities.Person", "Person")
                        .WithMany("Exclusions")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Excluded");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.GiftWish", b =>
                {
                    b.HasOne("GiftGivingGenerator.API.Entities.Event", "Event")
                        .WithMany("GiftWishes")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GiftGivingGenerator.API.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Event", b =>
                {
                    b.Navigation("DrawingResults");

                    b.Navigation("Exclusions");

                    b.Navigation("GiftWishes");
                });

            modelBuilder.Entity("GiftGivingGenerator.API.Entities.Person", b =>
                {
                    b.Navigation("CreatedEvents");

                    b.Navigation("Exclusions");
                });
#pragma warning restore 612, 618
        }
    }
}
