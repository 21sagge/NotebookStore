using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;

namespace NotebookStoreContext;

public class NotebookStoreContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public NotebookStoreContext() { }

    public NotebookStoreContext(DbContextOptions<NotebookStoreContext> options) : base(options) { }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Cpu> Cpus { get; set; }
    public DbSet<Display> Displays { get; set; }
    public DbSet<Memory> Memories { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Notebook> Notebooks { get; set; }
    public DbSet<NotebooksQuantities> NotebooksQuantities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(b =>
        {
            b.HasKey(b => b.Id);
            b.Property(b => b.Name).IsRequired();
            b.Property(b => b.CreatedAt).IsRequired();
            b.HasIndex(b => b.Name).IsUnique();
        });

        modelBuilder.Entity<Model>(m =>
        {
            m.HasKey(m => m.Id);
            m.Property(m => m.Name).IsRequired();
            m.Property(m => m.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<Cpu>(c =>
        {
            c.HasKey(c => c.Id);
            c.Property(c => c.Brand).IsRequired();
            c.Property(c => c.Model).IsRequired();
            c.Property(c => c.CreatedAt).IsRequired();
            c.HasIndex(c => new { c.Brand, c.Model }).IsUnique();
        });

        modelBuilder.Entity<Display>(d =>
        {
            d.HasKey(d => d.Id);
            d.Property(d => d.Size).IsRequired();
            d.Property(d => d.ResolutionWidth).IsRequired();
            d.Property(d => d.ResolutionHeight).IsRequired();
            d.Property(d => d.PanelType).IsRequired();
            d.Property(d => d.CreatedAt).IsRequired();
            d.HasIndex(d => new { d.Size, d.ResolutionWidth, d.ResolutionHeight, d.PanelType }).IsUnique();
        });

        modelBuilder.Entity<Memory>(m =>
        {
            m.HasKey(m => m.Id);
            m.Property(m => m.Capacity).IsRequired();
            m.Property(m => m.Speed).IsRequired();
            m.Property(m => m.CreatedAt).IsRequired();
            m.HasIndex(m => new { m.Capacity, m.Speed }).IsUnique();
        });

        modelBuilder.Entity<Storage>(s =>
        {
            s.HasKey(s => s.Id);
            s.Property(s => s.Capacity).IsRequired();
            s.Property(s => s.Type).IsRequired();
            s.Property(s => s.CreatedAt).IsRequired();
            s.HasIndex(s => new { s.Capacity, s.Type }).IsUnique();
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
            n.HasOne(n => n.Brand).WithMany().HasForeignKey(n => n.BrandId);
            n.HasOne(n => n.Model).WithMany().HasForeignKey(n => n.ModelId);
            n.HasOne(n => n.Cpu).WithMany().HasForeignKey(n => n.CpuId);
            n.HasOne(n => n.Display).WithMany().HasForeignKey(n => n.DisplayId);
            n.HasOne(n => n.Memory).WithMany().HasForeignKey(n => n.MemoryId);
            n.HasOne(n => n.Storage).WithMany().HasForeignKey(n => n.StorageId);
            n.HasIndex(n => new { n.BrandId, n.ModelId, n.CpuId, n.DisplayId, n.MemoryId, n.StorageId, n.Color, n.Price }).IsUnique();
        });

        modelBuilder.Entity<NotebooksQuantities>(nq =>
        {
            nq.HasKey(nq => nq.NotebookId);
            nq.Property(nq => nq.Quantity).IsRequired();
            nq.HasOne(nq => nq.Notebook).WithOne().HasForeignKey<NotebooksQuantities>(nq => nq.NotebookId);
        });

        base.OnModelCreating(modelBuilder);
    }
}
