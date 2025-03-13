﻿// <auto-generated />
using System;
using ExamenParcial.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExamenParcial.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250313202628_yyu")]
    partial class yyu
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExamenParcial.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cliente_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("apellido");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("direccion");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nombre");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("telefono");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ExamenParcial.Models.Producto", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("producto_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("descripcion");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("nombre");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("precio");

                    b.Property<int>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("ProductoId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("ExamenParcial.Models.Venta", b =>
                {
                    b.Property<int>("VentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("venta_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VentaId"));

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int")
                        .HasColumnName("cliente_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("estado");

                    b.Property<DateTime>("FechaVenta")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha_venta");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("total");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("VentaId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("ExamenParcial.Models.VentaDetalle", b =>
                {
                    b.Property<int>("DetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("detalle_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetalleId"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("precio_unitario");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int")
                        .HasColumnName("producto_id");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("subtotal");

                    b.Property<int>("VentaId")
                        .HasColumnType("int")
                        .HasColumnName("venta_id");

                    b.HasKey("DetalleId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("VentaId");

                    b.ToTable("DetalleVenta");
                });

            modelBuilder.Entity("ExamenParcial.Models.Venta", b =>
                {
                    b.HasOne("ExamenParcial.Models.Cliente", "Cliente")
                        .WithMany("Ventas")
                        .HasForeignKey("ClienteId");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ExamenParcial.Models.VentaDetalle", b =>
                {
                    b.HasOne("ExamenParcial.Models.Producto", "Productos")
                        .WithMany("DetalleVenta")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamenParcial.Models.Venta", "Venta")
                        .WithMany("VentaDetalle")
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Productos");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("ExamenParcial.Models.Cliente", b =>
                {
                    b.Navigation("Ventas");
                });

            modelBuilder.Entity("ExamenParcial.Models.Producto", b =>
                {
                    b.Navigation("DetalleVenta");
                });

            modelBuilder.Entity("ExamenParcial.Models.Venta", b =>
                {
                    b.Navigation("VentaDetalle");
                });
#pragma warning restore 612, 618
        }
    }
}
