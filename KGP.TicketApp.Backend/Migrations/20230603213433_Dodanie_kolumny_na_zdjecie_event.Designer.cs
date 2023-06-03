﻿// <auto-generated />
using System;
using KGP.TicketApp.Model.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KGP.TicketApp.Backend.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230603213433_Dodanie_kolumny_na_zdjecie_event")]
    partial class Dodanie_kolumny_na_zdjecie_event
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.ClientEvent_Liking", b =>
                {
                    b.Property<Guid>("LikingClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LikedEventId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LikingClientId", "LikedEventId");

                    b.HasIndex("LikedEventId");

                    b.ToTable("ClientEvent_Likings");
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.ClientEvent_Participating", b =>
                {
                    b.Property<Guid>("ParticipatingClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ParticipatedEventId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ParticipatingClientId", "ParticipatedEventId");

                    b.HasIndex("ParticipatedEventId");

                    b.ToTable("ClientEvent_Participatings");
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ParticipantsLimit")
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("TicketSaleEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TicketSaleStartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BlobTicketUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsValidated")
                        .HasColumnType("bit");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Surname")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.Client", b =>
                {
                    b.HasBaseType("KGP.TicketApp.Model.Database.Tables.User");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.Organizer", b =>
                {
                    b.HasBaseType("KGP.TicketApp.Model.Database.Tables.User");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasDiscriminator().HasValue("Organizer");
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.ClientEvent_Liking", b =>
                {
                    b.HasOne("KGP.TicketApp.Model.Database.Tables.Event", "LikedEvent")
                        .WithMany("ClientEvent_Likings")
                        .HasForeignKey("LikedEventId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("KGP.TicketApp.Model.Database.Tables.Client", "LikingClient")
                        .WithMany("ClientEvent_Likings")
                        .HasForeignKey("LikingClientId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("LikedEvent");

                    b.Navigation("LikingClient");
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.ClientEvent_Participating", b =>
                {
                    b.HasOne("KGP.TicketApp.Model.Database.Tables.Event", "ParticipatedEvent")
                        .WithMany("ClientEvent_Participatings")
                        .HasForeignKey("ParticipatedEventId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("KGP.TicketApp.Model.Database.Tables.Client", "ParticipatingClient")
                        .WithMany("ClientEvent_Participatings")
                        .HasForeignKey("ParticipatingClientId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("ParticipatedEvent");

                    b.Navigation("ParticipatingClient");
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.Event", b =>
                {
                    b.HasOne("KGP.TicketApp.Model.Database.Tables.Organizer", "Organizer")
                        .WithMany("Events")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("KGP.TicketApp.Model.Database.Tables.Location", "Place", b1 =>
                        {
                            b1.Property<Guid>("EventId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("BuildingName")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("StreetName")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("StreetNumber")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.HasKey("EventId");

                            b1.ToTable("Locations");

                            b1.WithOwner()
                                .HasForeignKey("EventId");
                        });

                    b.Navigation("Organizer");

                    b.Navigation("Place")
                        .IsRequired();
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.Ticket", b =>
                {
                    b.HasOne("KGP.TicketApp.Model.Database.Tables.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KGP.TicketApp.Model.Database.Tables.Client", "Owner")
                        .WithMany("Tickets")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.Event", b =>
                {
                    b.Navigation("ClientEvent_Likings");

                    b.Navigation("ClientEvent_Participatings");
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.Client", b =>
                {
                    b.Navigation("ClientEvent_Likings");

                    b.Navigation("ClientEvent_Participatings");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("KGP.TicketApp.Model.Database.Tables.Organizer", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
