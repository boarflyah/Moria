﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseGenerator.Migrations
{
    [DbContext(typeof(MoriaDataContext))]
    [Migration("20250216082018_updateComponent")]
    partial class updateComponent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("MoriaModels.Models.DriveComponents.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int?>("ComponentProductId")
                        .HasColumnType("integer");

                    b.Property<string>("ElectricalDescription")
                        .HasColumnType("text");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComponentProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("MoriaModels.Models.DriveComponents.Drive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<bool>("Inverter")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<int>("MotorId")
                        .HasColumnType("integer");

                    b.Property<byte>("Quantity")
                        .HasColumnType("smallint");

                    b.Property<bool>("Variator")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("MotorId");

                    b.ToTable("Drives");
                });

            modelBuilder.Entity("MoriaModels.Models.DriveComponents.Motor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Power")
                        .HasColumnType("numeric");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Motors");
                });

            modelBuilder.Entity("MoriaModels.Models.DriveComponents.MotorGear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int?>("DriveId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Ratio")
                        .HasColumnType("text");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DriveId");

                    b.ToTable("MotorGears");
                });

            modelBuilder.Entity("MoriaModels.Models.EntityPersonel.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<bool>("Admin")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int?>("PositionId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("MoriaModels.Models.EntityPersonel.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("MoriaModels.Models.Orders.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<string>("LongName")
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .HasColumnType("text");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("MoriaModels.Models.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("CatalogLink")
                        .HasColumnType("text");

                    b.Property<string>("ClientSymbol")
                        .HasColumnType("text");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<string>("OrderNumberSymbol")
                        .HasColumnType("text");

                    b.Property<int>("OrderingContactId")
                        .HasColumnType("integer");

                    b.Property<int>("ReceivingContactId")
                        .HasColumnType("integer");

                    b.Property<string>("Remarks")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderingContactId");

                    b.HasIndex("ReceivingContactId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MoriaModels.Models.Orders.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int?>("ComponentId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DesignerId")
                        .HasColumnType("integer");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<decimal>("MachineWeight")
                        .HasColumnType("numeric");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<string>("TechnicalDrawingLink")
                        .HasColumnType("text");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("DesignerId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("MoriaModels.Models.Products.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MoriaModels.Models.Products.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("MoriaModels.Models.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMainMachine")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.Property<int?>("SteelKindId")
                        .HasColumnType("integer");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.Property<int?>("WarehouseId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SteelKindId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MoriaModels.Models.Products.SteelKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SteelKinds");
                });

            modelBuilder.Entity("MoriaModels.Models.Warehouses.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModified")
                        .HasColumnType("text");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("MoriaModels.Models.DriveComponents.Component", b =>
                {
                    b.HasOne("MoriaModels.Models.Products.Product", "ComponentProduct")
                        .WithMany()
                        .HasForeignKey("ComponentProductId");

                    b.HasOne("MoriaModels.Models.Products.Product", "Product")
                        .WithMany("Components")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ComponentProduct");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MoriaModels.Models.DriveComponents.Drive", b =>
                {
                    b.HasOne("MoriaModels.Models.DriveComponents.Motor", "Motor")
                        .WithMany()
                        .HasForeignKey("MotorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motor");
                });

            modelBuilder.Entity("MoriaModels.Models.DriveComponents.MotorGear", b =>
                {
                    b.HasOne("MoriaModels.Models.DriveComponents.Drive", null)
                        .WithMany("Gearboxes")
                        .HasForeignKey("DriveId");
                });

            modelBuilder.Entity("MoriaModels.Models.EntityPersonel.Employee", b =>
                {
                    b.HasOne("MoriaModels.Models.EntityPersonel.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("MoriaModels.Models.Orders.Order", b =>
                {
                    b.HasOne("MoriaModels.Models.Orders.Contact", "OrderingContact")
                        .WithMany()
                        .HasForeignKey("OrderingContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoriaModels.Models.Orders.Contact", "ReceivingContact")
                        .WithMany()
                        .HasForeignKey("ReceivingContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderingContact");

                    b.Navigation("ReceivingContact");
                });

            modelBuilder.Entity("MoriaModels.Models.Orders.OrderItem", b =>
                {
                    b.HasOne("MoriaModels.Models.DriveComponents.Component", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("ComponentId");

                    b.HasOne("MoriaModels.Models.EntityPersonel.Employee", "Designer")
                        .WithMany()
                        .HasForeignKey("DesignerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoriaModels.Models.Orders.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("MoriaModels.Models.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoriaModels.Models.Warehouses.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Designer");

                    b.Navigation("Product");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("MoriaModels.Models.Products.Product", b =>
                {
                    b.HasOne("MoriaModels.Models.Products.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("MoriaModels.Models.Products.SteelKind", "SteelKind")
                        .WithMany()
                        .HasForeignKey("SteelKindId");

                    b.HasOne("MoriaModels.Models.Warehouses.Warehouse", null)
                        .WithMany("Products")
                        .HasForeignKey("WarehouseId");

                    b.Navigation("Category");

                    b.Navigation("SteelKind");
                });

            modelBuilder.Entity("MoriaModels.Models.DriveComponents.Component", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("MoriaModels.Models.DriveComponents.Drive", b =>
                {
                    b.Navigation("Gearboxes");
                });

            modelBuilder.Entity("MoriaModels.Models.Orders.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("MoriaModels.Models.Products.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("MoriaModels.Models.Products.Product", b =>
                {
                    b.Navigation("Components");
                });

            modelBuilder.Entity("MoriaModels.Models.Warehouses.Warehouse", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
