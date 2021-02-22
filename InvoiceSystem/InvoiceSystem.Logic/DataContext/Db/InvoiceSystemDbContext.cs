using InvoiceSystem.Logic.Entities.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Logic.DataContext.Db
{
    internal class InvoiceSystemDbContext : GenericDbContext
    {
        static string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=InvoiceSystemDB;Integrated Security=True";

        public DbSet<InvoiceHead> InvoiceHeadSet { get; set; }
        public DbSet<InvoicePosition> InvoicePositionSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public override DbSet<E> Set<I, E>()
        {
            DbSet<E> result = null;

            if (typeof(I) == typeof(Contracts.Persistence.IInvoiceHead))
            {
                result = InvoiceHeadSet as DbSet<E>;
            }
            else if (typeof(I) == typeof(Contracts.Persistence.IInvoicePosition))
            {
                result = InvoicePositionSet as DbSet<E>;
            }
            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<InvoiceHead>()
                .ToTable(nameof(InvoiceHead))
                .HasKey(k => k.Id);
            modelBuilder.Entity<InvoiceHead>()
                .Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(256);
            modelBuilder.Entity<InvoiceHead>()
                .Property(p => p.Street)
                .IsRequired()
                .HasMaxLength(128);
            modelBuilder.Entity<InvoiceHead>()
                .Property(p => p.ZipCode)
                .IsRequired()
                .HasMaxLength(10);
            modelBuilder.Entity<InvoiceHead>()
                .Property(p => p.City)
                .IsRequired()
                .HasMaxLength(64);

            modelBuilder.Entity<InvoicePosition>()
                .ToTable(nameof(InvoicePosition))
                .HasKey(k => k.Id);
            modelBuilder.Entity<InvoicePosition>()
                .Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
