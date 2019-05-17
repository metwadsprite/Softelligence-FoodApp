﻿// <auto-generated />
using System;
using EF.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EF.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EF.DataAccess.DataModel.MenuDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Hyperlink");

                    b.Property<string>("Image");

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("EF.DataAccess.DataModel.OrderDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details");

                    b.Property<bool>("IsActive");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SessionId");

                    b.Property<int?>("StoreId");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EF.DataAccess.DataModel.SessionDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("EF.DataAccess.DataModel.SessionStoreDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("SessionId");

                    b.Property<int?>("StoreId");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.HasIndex("StoreId");

                    b.ToTable("SessionsStores");
                });

            modelBuilder.Entity("EF.DataAccess.DataModel.StoreDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ClosingTime");

                    b.Property<int?>("MenuId");

                    b.Property<string>("Name");

                    b.Property<DateTime>("OpeningTime");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("EF.DataAccess.DataModel.UserDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EF.DataAccess.DataModel.OrderDO", b =>
                {
                    b.HasOne("EF.DataAccess.DataModel.SessionDO", "Session")
                        .WithMany("Orders")
                        .HasForeignKey("SessionId");

                    b.HasOne("EF.DataAccess.DataModel.StoreDO", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId");

                    b.HasOne("EF.DataAccess.DataModel.UserDO", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("EF.DataAccess.DataModel.SessionStoreDO", b =>
                {
                    b.HasOne("EF.DataAccess.DataModel.SessionDO", "Session")
                        .WithMany("SesionStore")
                        .HasForeignKey("SessionId");

                    b.HasOne("EF.DataAccess.DataModel.StoreDO", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId");
                });

            modelBuilder.Entity("EF.DataAccess.DataModel.StoreDO", b =>
                {
                    b.HasOne("EF.DataAccess.DataModel.MenuDO", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId");
                });
#pragma warning restore 612, 618
        }
    }
}
