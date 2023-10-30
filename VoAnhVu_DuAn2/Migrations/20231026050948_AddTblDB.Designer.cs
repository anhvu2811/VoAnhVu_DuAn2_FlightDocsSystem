﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20231026050948_AddTblDB")]
    partial class AddTblDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VoAnhVu_DuAn2.Entities.AccessEntity", b =>
                {
                    b.Property<string>("AccessId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccessName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccessId");

                    b.ToTable("Access");
                });

            modelBuilder.Entity("VoAnhVu_DuAn2.Entities.DocumentEntity", b =>
                {
                    b.Property<string>("DocumentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FileUpLoad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocumentId");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("FlightId");

                    b.HasIndex("UserId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("VoAnhVu_DuAn2.Entities.DocumentTypeEntity", b =>
                {
                    b.Property<string>("DocumentTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DocumentTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupPermissionId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DocumentTypeId");

                    b.HasIndex("GroupPermissionId");

                    b.ToTable("DocumentType");
                });

            modelBuilder.Entity("VoAnhVu_DuAn2.Entities.FlightEntity", b =>
                {
                    b.Property<string>("FlightId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("PointOfLoading")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PointOfUnloading")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FlightId");

                    b.ToTable("Flight");
                });

            modelBuilder.Entity("VoAnhVu_DuAn2.Entities.GroupPermissionEntity", b =>
                {
                    b.Property<string>("GroupPermissionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccessId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GroupPermissionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupPermissionId");

                    b.HasIndex("AccessId");

                    b.ToTable("GroupPermission");
                });

            modelBuilder.Entity("VoAnhVu_DuAn2.Entities.RoleEntity", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("VoAnhVu_DuAn2.Entities.UserEntity", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("VoAnhVu_DuAn2.Entities.DocumentEntity", b =>
                {
                    b.HasOne("VoAnhVu_DuAn2.Entities.DocumentTypeEntity", "DocumentType")
                        .WithMany()
                        .HasForeignKey("DocumentTypeId");

                    b.HasOne("VoAnhVu_DuAn2.Entities.FlightEntity", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId");

                    b.HasOne("VoAnhVu_DuAn2.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("DocumentType");

                    b.Navigation("Flight");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VoAnhVu_DuAn2.Entities.DocumentTypeEntity", b =>
                {
                    b.HasOne("VoAnhVu_DuAn2.Entities.GroupPermissionEntity", "GroupPermission")
                        .WithMany()
                        .HasForeignKey("GroupPermissionId");

                    b.Navigation("GroupPermission");
                });

            modelBuilder.Entity("VoAnhVu_DuAn2.Entities.GroupPermissionEntity", b =>
                {
                    b.HasOne("VoAnhVu_DuAn2.Entities.AccessEntity", "Access")
                        .WithMany()
                        .HasForeignKey("AccessId");

                    b.Navigation("Access");
                });

            modelBuilder.Entity("VoAnhVu_DuAn2.Entities.UserEntity", b =>
                {
                    b.HasOne("VoAnhVu_DuAn2.Entities.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}