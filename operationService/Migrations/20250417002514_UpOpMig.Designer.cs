﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace operationService.Migrations
{
    [DbContext(typeof(OperationDbContext))]
    [Migration("20250417002514_UpOpMig")]
    partial class UpOpMig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Operation", b =>
                {
                    b.Property<Guid>("OperationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OperationAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("OperationQuantity")
                        .HasColumnType("int");

                    b.Property<int>("OperationType")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OperationId");

                    b.ToTable("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}
