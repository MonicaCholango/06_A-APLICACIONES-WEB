﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _4_Clase.Data;

#nullable disable

namespace _4_Clase.Migrations
{
    [DbContext(typeof(Get_PostDbContext))]
    [Migration("20250305044939_Clase4")]
    partial class Clase4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("_4_Clase.Models.PreguntasModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("CreateAdd")
                        .HasColumnType("date");

                    b.Property<string>("Enunciado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoPreguntaModelId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("UpdateAdd")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("TipoPreguntaModelId");

                    b.ToTable("Preguntas");
                });

            modelBuilder.Entity("_4_Clase.Models.TipoPreguntaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Detalle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoPreguntas");
                });

            modelBuilder.Entity("_4_Clase.Models.PreguntasModel", b =>
                {
                    b.HasOne("_4_Clase.Models.TipoPreguntaModel", "TipoPreguntaModel")
                        .WithMany()
                        .HasForeignKey("TipoPreguntaModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoPreguntaModel");
                });
#pragma warning restore 612, 618
        }
    }
}
