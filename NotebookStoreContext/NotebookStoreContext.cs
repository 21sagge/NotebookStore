﻿using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;

namespace NotebookStoreContext;

public class NotebookStoreContext : DbContext
{
  public DbSet<Brand> Brands { get; set; }
  public DbSet<Model> Models { get; set; }
  public DbSet<Cpu> Cpus { get; set; }
  public DbSet<Display> Displays { get; set; }
  public DbSet<Memory> Memories { get; set; }
  public DbSet<Storage> Storages { get; set; }
  public DbSet<Notebook> Notebooks { get; set; }

  public string DbPath { get; }

  public NotebookStoreContext()
  {
    DbPath = "notebookstore.db";
    Database.EnsureCreated();
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite($"Data Source={DbPath}");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Brand>(b =>
    {
      b.HasKey(b => b.Id);
      b.Property(b => b.Name).IsRequired();
    });

    modelBuilder.Entity<Model>(m =>
    {
      m.HasKey(m => m.Id);
      m.Property(m => m.Name).IsRequired();
    });

    modelBuilder.Entity<Cpu>(c =>
    {
      c.HasKey(c => c.Id);
      c.Property(c => c.Brand).IsRequired();
      c.Property(c => c.Model).IsRequired();
    });

    modelBuilder.Entity<Display>(d =>
    {
      d.HasKey(d => d.Id);
      d.Property(d => d.Size).IsRequired();
      d.Property(d => d.Resolution).IsRequired();
      d.Property(d => d.PanelType).IsRequired();
    });

    modelBuilder.Entity<Memory>(m =>
    {
      m.HasKey(m => m.Id);
      m.Property(m => m.Capacity).IsRequired();
      m.Property(m => m.Speed).IsRequired();
    });

    modelBuilder.Entity<Storage>(s =>
    {
      s.HasKey(s => s.Id);
      s.Property(s => s.Capacity).IsRequired();
      s.Property(s => s.Type).IsRequired();
    });

    modelBuilder.Entity<Notebook>(n =>
    {
      n.HasKey(n => n.Id);
      n.Property(n => n.Color).IsRequired();
      n.Property(n => n.Price).IsRequired();
      n.Property(n => n.BrandId).IsRequired();
      n.Property(n => n.ModelId).IsRequired();
      n.Property(n => n.CpuId).IsRequired();
      n.Property(n => n.DisplayId).IsRequired();
      n.Property(n => n.MemoryId).IsRequired();
      n.Property(n => n.StorageId).IsRequired();
    });
  }
}