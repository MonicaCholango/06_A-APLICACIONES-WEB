﻿// <auto-generated />
using Ejercicio2.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ejercicio2.Migrations
{
    [DbContext(typeof(Ejercicio2.Config.NetCoreDbContext))]
    [Migration("20250227193906_Inicial2")]
    partial class Inicial2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ejercicio2.Models.ClienteModel", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Apellido")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Cedula_RUC")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Direccion")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Nombre")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Telefono")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Cliente");
            });
#pragma warning restore 612, 618
        }
    }
}

