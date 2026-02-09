using Microsoft.EntityFrameworkCore;
using Prueba.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Orden> Ordenes => Set<Orden>();
        public DbSet<DetalleOrden> DetalleOrdenes => Set<DetalleOrden>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ClienteId);

                entity.Property(e => e.ClienteId)
                      .ValueGeneratedOnAdd(); 

                entity.Property(e => e.Nombre)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(e => e.Identidad)
                      .HasMaxLength(50)
                      .IsRequired();
            });

         
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.ProductoId);

                entity.Property(e => e.ProductoId)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                      .HasMaxLength(150)
                      .IsRequired();


                entity.Property(e => e.Precio)
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Existencia)
                      .IsRequired();
            });

           
            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasKey(e => e.OrdenId);

                entity.Property(e => e.OrdenId)
                      .ValueGeneratedOnAdd(); 

                entity.HasOne(o => o.Cliente)
                      .WithMany(c => c.Ordenes)
                      .HasForeignKey(o => o.ClienteId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(o => o.ClienteId)
                      .HasDatabaseName("IX_Orden_ClienteId");
            });

          
            modelBuilder.Entity<DetalleOrden>(entity =>
            {
                entity.HasKey(e => e.DetalleId);

                entity.Property(e => e.DetalleId)
                      .ValueGeneratedOnAdd(); 

                entity.Property(e => e.Impuesto)
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Subtotal)
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Total)
                      .HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.Orden)
                      .WithMany(o => o.Detalles)
                      .HasForeignKey(d => d.OrdenId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Producto)
                      .WithMany(p => p.Detalles)
                      .HasForeignKey(d => d.ProductoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(d => d.OrdenId)
                      .HasDatabaseName("IX_DetalleOrden_OrdenId");

                entity.HasIndex(d => d.ProductoId)
                      .HasDatabaseName("IX_DetalleOrden_ProductoId");
            });

            modelBuilder.Entity<Cliente>().HasData(
         new Cliente { ClienteId = 1, Nombre = "Juan Pérez", Identidad = "0801-1990-12345" },
         new Cliente { ClienteId = 2, Nombre = "María González", Identidad = "0801-1985-67890" },
         new Cliente { ClienteId = 3, Nombre = "Carlos Rodríguez", Identidad = "0801-1992-11111" }
     );

            modelBuilder.Entity<Producto>().HasData(
                new Producto { ProductoId = 1, Nombre = "Laptop Dell XPS 15", Descripcion = "Laptop de alto rendimiento", Precio = 1299.99m, Existencia = 50 },
                new Producto { ProductoId = 2, Nombre = "Mouse Logitech MX Master 3", Descripcion = "Mouse ergonómico inalámbrico", Precio = 99.99m, Existencia = 150 },
                new Producto { ProductoId = 3, Nombre = "Teclado Mecánico Keychron K2", Descripcion = "Teclado mecánico retroiluminado", Precio = 89.99m, Existencia = 75 },
                new Producto { ProductoId = 4, Nombre = "Monitor LG 27\" 4K", Descripcion = "Monitor 4K UHD", Precio = 449.99m, Existencia = 30 },
                new Producto { ProductoId = 5, Nombre = "Webcam Logitech C920", Descripcion = "Webcam Full HD 1080p", Precio = 79.99m, Existencia = 100 }
            );
        }
    }
}
