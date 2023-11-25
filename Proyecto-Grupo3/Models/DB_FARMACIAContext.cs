using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proyecto_Grupo3.Models
{
    public partial class DB_FARMACIAContext : DbContext
    {
        public DB_FARMACIAContext()
        {
        }

        public DB_FARMACIAContext(DbContextOptions<DB_FARMACIAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TFactura> TFacturas { get; set; } = null!;
        public virtual DbSet<TFacturasUsuario> TFacturasUsuarios { get; set; } = null!;
        public virtual DbSet<TProducto> TProductos { get; set; } = null!;
        public virtual DbSet<TProductosVendido> TProductosVendidos { get; set; } = null!;
        public virtual DbSet<TRegistroCliente> TRegistroClientes { get; set; } = null!;
        public virtual DbSet<TRegistroUsuario> TRegistroUsuarios { get; set; } = null!;
        public virtual DbSet<TTiposProducto> TTiposProductos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TFactura>(entity =>
            {
                entity.HasKey(e => e.IdFactura);

                entity.ToTable("T_FACTURA", "SCH_FARMACIA");

                entity.Property(e => e.IdFactura).HasColumnName("ID_FACTURA");

                entity.Property(e => e.CantidadProductos).HasColumnName("CANTIDAD_PRODUCTOS");

                entity.Property(e => e.CodigoFactura).HasColumnName("CODIGO_FACTURA");

                entity.Property(e => e.CodigoProducto).HasColumnName("CODIGO_PRODUCTO");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_COMPRA");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.Iva)
                    .HasColumnType("money")
                    .HasColumnName("IVA");

                entity.Property(e => e.MetodoPago)
                    .HasMaxLength(30)
                    .HasColumnName("METODO_PAGO");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("money")
                    .HasColumnName("SUBTOTAL");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("TOTAL");

                entity.HasOne(d => d.CodigoProductoNavigation)
                    .WithMany(p => p.TFacturas)
                    .HasForeignKey(d => d.CodigoProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.TFacturas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TFacturasUsuario>(entity =>
            {
                entity.HasKey(e => e.IdFacturasUsuario);

                entity.ToTable("T_FACTURAS_USUARIO", "SCH_FARMACIA");

                entity.Property(e => e.IdFacturasUsuario).HasColumnName("ID_FACTURAS_USUARIO");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.IdFactura).HasColumnName("ID_FACTURA");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.TFacturasUsuarios)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.TFacturasUsuarios)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TProducto>(entity =>
            {
                entity.HasKey(e => e.NombreCategoria);

                entity.ToTable("T_PRODUCTO", "SCH_FARMACIA");

                entity.Property(e => e.NombreCategoria)
                    .HasMaxLength(50)
                    .HasColumnName("NOMBRE_CATEGORIA");

                entity.Property(e => e.CodigoTipoProducto).HasColumnName("CODIGO_TIPO_PRODUCTO");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(70)
                    .HasColumnName("NOMBRE_PRODUCTO");

                entity.HasOne(d => d.CodigoTipoProductoNavigation)
                    .WithMany(p => p.TProductos)
                    .HasForeignKey(d => d.CodigoTipoProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TProductosVendido>(entity =>
            {
                entity.HasKey(e => e.CodigoProducto);

                entity.ToTable("T_PRODUCTOS_VENDIDO", "SCH_FARMACIA");

                entity.Property(e => e.CodigoProducto).HasColumnName("CODIGO_PRODUCTO");

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.CodigoTipoProducto).HasColumnName("CODIGO_TIPO_PRODUCTO");

                entity.Property(e => e.DescripcionProducto)
                    .HasMaxLength(300)
                    .HasColumnName("DESCRIPCION_PRODUCTO");

                entity.Property(e => e.Estado)
                    .HasMaxLength(30)
                    .HasColumnName("ESTADO")
                    .IsFixedLength();

                entity.Property(e => e.Precio)
                    .HasColumnType("money")
                    .HasColumnName("PRECIO");

                entity.HasOne(d => d.CodigoTipoProductoNavigation)
                    .WithMany(p => p.TProductosVendidos)
                    .HasForeignKey(d => d.CodigoTipoProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TRegistroCliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("T_REGISTRO_CLIENTE", "SCH_ADMINISTRATIVO");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.Correo)
                    .HasMaxLength(40)
                    .HasColumnName("CORREO");

                entity.Property(e => e.IdentificacionCliente)
                    .HasMaxLength(20)
                    .HasColumnName("IDENTIFICACION_CLIENTE");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(30)
                    .HasColumnName("NOMBRE_COMPLETO");
            });

            modelBuilder.Entity<TRegistroUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("T_REGISTRO_USUARIO", "SCH_ADMINISTRATIVO");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(50)
                    .HasColumnName("CONTRASEÑA");

                entity.Property(e => e.Correo)
                    .HasMaxLength(30)
                    .HasColumnName("CORREO");

                entity.Property(e => e.Estado)
                    .HasMaxLength(40)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.IdentificacionUsuario)
                    .HasMaxLength(20)
                    .HasColumnName("IDENTIFICACION_USUARIO");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(30)
                    .HasColumnName("NOMBRE_COMPLETO");

                entity.Property(e => e.TipoUsuario)
                    .HasMaxLength(30)
                    .HasColumnName("TIPO_USUARIO");
            });

            modelBuilder.Entity<TTiposProducto>(entity =>
            {
                entity.HasKey(e => e.CodigoTipoProducto);

                entity.ToTable("T_TIPOS_PRODUCTO", "SCH_FARMACIA");

                entity.Property(e => e.CodigoTipoProducto).HasColumnName("CODIGO_TIPO_PRODUCTO");

                entity.Property(e => e.DescripcionTipoProducto)
                    .HasMaxLength(300)
                    .HasColumnName("DESCRIPCION_TIPO_PRODUCTO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
