﻿// <auto-generated />
using System;
using CRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRS.WebApi.Migrations
{
    [DbContext(typeof(CrsdbContext))]
    [Migration("20240626072457_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CRS.WebApi.Models.TaxPayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AmountOwing")
                        .HasColumnType("money")
                        .HasColumnName("amountOwing");

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("Group")
                        .HasColumnType("int")
                        .HasColumnName("group");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<int?>("PersonaId")
                        .HasColumnType("int")
                        .HasColumnName("personaId");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<Guid>("TaxPayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("taxPayerID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime?>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("updated")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("Id")
                        .HasName("PK_TaxPayerId");

                    b.HasIndex("Group");

                    b.HasIndex("Status");

                    b.ToTable("TaxPayer", null, t =>
                        {
                            t.HasTrigger("trgAfterUpdate");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("CRS.WebApi.Models.TaxPayerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("description");

                    b.HasKey("Id")
                        .HasName("PK_TaxPayerTypeId");

                    b.ToTable("TaxPayerType");
                });

            modelBuilder.Entity("CRS.WebApi.Models.TaxPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("money")
                        .HasColumnName("amount");

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("TaxPayerId")
                        .HasColumnType("int")
                        .HasColumnName("taxPayerID");

                    b.Property<int>("TaxType")
                        .HasColumnType("int")
                        .HasColumnName("taxType");

                    b.HasKey("Id")
                        .HasName("PK_TaxPaymentId");

                    b.HasIndex("TaxPayerId");

                    b.HasIndex("TaxType");

                    b.ToTable("TaxPayment");
                });

            modelBuilder.Entity("CRS.WebApi.Models.TaxStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("description");

                    b.HasKey("Id")
                        .HasName("PK_TaxStatusId");

                    b.ToTable("TaxStatus");
                });

            modelBuilder.Entity("CRS.WebApi.Models.TaxType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("description");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(5, 5)")
                        .HasColumnName("rate");

                    b.HasKey("Id")
                        .HasName("PK_TaxTypeId");

                    b.ToTable("TaxType");
                });

            modelBuilder.Entity("CRS.WebApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.HasKey("Id")
                        .HasName("PK_UserId");

                    b.HasIndex(new[] { "Email" }, "UQ__User__AB6E6164031D2F51")
                        .IsUnique()
                        .HasFilter("[email] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CRS.WebApi.Models.TaxPayer", b =>
                {
                    b.HasOne("CRS.WebApi.Models.TaxPayerType", "GroupNavigation")
                        .WithMany("TaxPayers")
                        .HasForeignKey("Group")
                        .IsRequired()
                        .HasConstraintName("FK_TaxPayer_TaxPyerType");

                    b.HasOne("CRS.WebApi.Models.TaxStatus", "StatusNavigation")
                        .WithMany("TaxPayers")
                        .HasForeignKey("Status")
                        .IsRequired()
                        .HasConstraintName("FK_TaxPayer_TaxStatus");

                    b.Navigation("GroupNavigation");

                    b.Navigation("StatusNavigation");
                });

            modelBuilder.Entity("CRS.WebApi.Models.TaxPayment", b =>
                {
                    b.HasOne("CRS.WebApi.Models.TaxPayer", "TaxPayer")
                        .WithMany("TaxPayments")
                        .HasForeignKey("TaxPayerId")
                        .IsRequired()
                        .HasConstraintName("FK_TaxPayer_TaxPayment");

                    b.HasOne("CRS.WebApi.Models.TaxType", "TaxTypeNavigation")
                        .WithMany("TaxPayments")
                        .HasForeignKey("TaxType")
                        .IsRequired()
                        .HasConstraintName("FK_TaxPayment_TaxType");

                    b.Navigation("TaxPayer");

                    b.Navigation("TaxTypeNavigation");
                });

            modelBuilder.Entity("CRS.WebApi.Models.TaxPayer", b =>
                {
                    b.Navigation("TaxPayments");
                });

            modelBuilder.Entity("CRS.WebApi.Models.TaxPayerType", b =>
                {
                    b.Navigation("TaxPayers");
                });

            modelBuilder.Entity("CRS.WebApi.Models.TaxStatus", b =>
                {
                    b.Navigation("TaxPayers");
                });

            modelBuilder.Entity("CRS.WebApi.Models.TaxType", b =>
                {
                    b.Navigation("TaxPayments");
                });
#pragma warning restore 612, 618
        }
    }
}
