﻿// <auto-generated />
using System;
using HospitalData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HospitalData.Migrations
{
    [DbContext(typeof(HospitalContext))]
    partial class HospitalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("HospitalData.DonatedOrgan", b =>
                {
                    b.Property<int>("DonatedOrganId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BloodType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DonationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DonorAge")
                        .HasColumnType("int");

                    b.Property<bool>("IsDonated")
                        .HasColumnType("bit");

                    b.Property<int>("OrganId")
                        .HasColumnType("int");

                    b.HasKey("DonatedOrganId");

                    b.HasIndex("OrganId");

                    b.ToTable("DonatedOrgans");
                });

            modelBuilder.Entity("HospitalData.MatchedDonation", b =>
                {
                    b.Property<int>("MatchedDonationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateOfMatch")
                        .HasColumnType("datetime2");

                    b.Property<int>("DonatedOrganId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("MatchedDonationId");

                    b.HasIndex("DonatedOrganId");

                    b.HasIndex("PatientId");

                    b.ToTable("MatchedDonations");
                });

            modelBuilder.Entity("HospitalData.Organ", b =>
                {
                    b.Property<int>("OrganId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsAgeChecked")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrganId");

                    b.ToTable("Organs");
                });

            modelBuilder.Entity("HospitalData.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BloodType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("HospitalData.Waiting", b =>
                {
                    b.Property<int>("WaitingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateOfEntry")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrganId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("WaitingId");

                    b.HasIndex("OrganId");

                    b.HasIndex("PatientId");

                    b.ToTable("Waitings");
                });

            modelBuilder.Entity("HospitalData.DonatedOrgan", b =>
                {
                    b.HasOne("HospitalData.Organ", "Organ")
                        .WithMany("DonatedOrgans")
                        .HasForeignKey("OrganId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organ");
                });

            modelBuilder.Entity("HospitalData.MatchedDonation", b =>
                {
                    b.HasOne("HospitalData.DonatedOrgan", "DonatedOrgan")
                        .WithMany("MatchedDonations")
                        .HasForeignKey("DonatedOrganId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalData.Patient", "Patient")
                        .WithMany("MatchedDonations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonatedOrgan");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HospitalData.Waiting", b =>
                {
                    b.HasOne("HospitalData.Organ", "Organ")
                        .WithMany("Waitings")
                        .HasForeignKey("OrganId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalData.Patient", "Patient")
                        .WithMany("Waitings")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organ");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HospitalData.DonatedOrgan", b =>
                {
                    b.Navigation("MatchedDonations");
                });

            modelBuilder.Entity("HospitalData.Organ", b =>
                {
                    b.Navigation("DonatedOrgans");

                    b.Navigation("Waitings");
                });

            modelBuilder.Entity("HospitalData.Patient", b =>
                {
                    b.Navigation("MatchedDonations");

                    b.Navigation("Waitings");
                });
#pragma warning restore 612, 618
        }
    }
}
