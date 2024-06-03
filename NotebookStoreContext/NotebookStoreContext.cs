using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;

namespace NotebookStoreContext;

public class NotebookStoreContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public NotebookStoreContext() { }

    public NotebookStoreContext(DbContextOptions<NotebookStoreContext> options) : base(options)
    {
        if (!Database.CanConnect())
        {
            Database.EnsureCreated();
        }
    }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Cpu> Cpus { get; set; }
    public DbSet<Display> Displays { get; set; }
    public DbSet<Memory> Memories { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Notebook> Notebooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(b =>
        {
            b.HasKey(b => b.Id);
            b.Property(b => b.Name).IsRequired();
            b.HasIndex(b => b.Name).IsUnique();
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
            c.HasIndex(c => new { c.Brand, c.Model }).IsUnique();
        });

        modelBuilder.Entity<Display>(d =>
        {
            d.HasKey(d => d.Id);
            d.Property(d => d.Size).IsRequired();
            d.Property(d => d.ResolutionWidth).IsRequired();
            d.Property(d => d.ResolutionHeight).IsRequired();
            d.Property(d => d.PanelType).IsRequired();
            d.HasIndex(d => new { d.Size, d.ResolutionWidth, d.ResolutionHeight, d.PanelType }).IsUnique();
        });

        modelBuilder.Entity<Memory>(m =>
        {
            m.HasKey(m => m.Id);
            m.Property(m => m.Capacity).IsRequired();
            m.Property(m => m.Speed).IsRequired();
            m.HasIndex(m => new { m.Capacity, m.Speed }).IsUnique();
        });

        modelBuilder.Entity<Storage>(s =>
        {
            s.HasKey(s => s.Id);
            s.Property(s => s.Capacity).IsRequired();
            s.Property(s => s.Type).IsRequired();
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

        base.OnModelCreating(modelBuilder);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "Editor", NormalizedName = "EDITOR" },
            new IdentityRole { Id = "3", Name = "Viewer", NormalizedName = "VIEWER" }
        );

        modelBuilder.Entity<IdentityUser>().HasData(
            new IdentityUser { Id = "1", UserName = "admin", NormalizedUserName = "ADMIN", Email = "admin@admin.com", NormalizedEmail = "ADMIN.COM", EmailConfirmed = true, PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "admin") },
            new IdentityUser { Id = "2", UserName = "editor", NormalizedUserName = "EDITOR", Email = "editor@editor.com", NormalizedEmail = "EDITOR.COM", EmailConfirmed = true, PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "editor") },
            new IdentityUser { Id = "3", UserName = "viewer", NormalizedUserName = "VIEWER", Email = "viewer@viewer.com", NormalizedEmail = "VIEWER.COM", EmailConfirmed = true, PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "viewer") }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = "1", RoleId = "1" },
            new IdentityUserRole<string> { UserId = "2", RoleId = "2" },
            new IdentityUserRole<string> { UserId = "3", RoleId = "3" }
        );

        modelBuilder.Entity<Brand>().HasData(
            new Brand { Id = 1, Name = "Apple" },
            new Brand { Id = 2, Name = "Dell" },
            new Brand { Id = 3, Name = "HP" },
            new Brand { Id = 4, Name = "Lenovo" },
            new Brand { Id = 5, Name = "Microsoft" },
            new Brand { Id = 6, Name = "Samsung" }
        );

        modelBuilder.Entity<Model>().HasData(
            new Model { Id = 1, Name = "MacBook Pro" },
            new Model { Id = 2, Name = "XPS" },
            new Model { Id = 3, Name = "Spectre" },
            new Model { Id = 4, Name = "ThinkPad" },
            new Model { Id = 5, Name = "Surface" },
            new Model { Id = 6, Name = "Galaxy Book" }
        );

        modelBuilder.Entity<Cpu>().HasData(
            new Cpu { Id = 1, Brand = "Intel", Model = "Core i5" },
            new Cpu { Id = 2, Brand = "Intel", Model = "Core i7" },
            new Cpu { Id = 3, Brand = "Intel", Model = "Core i9" },
            new Cpu { Id = 4, Brand = "AMD", Model = "Ryzen 5" },
            new Cpu { Id = 5, Brand = "AMD", Model = "Ryzen 7" },
            new Cpu { Id = 6, Brand = "AMD", Model = "Ryzen 9" }
        );

        modelBuilder.Entity<Display>().HasData(
            new Display { Id = 1, Size = 13.3, ResolutionWidth = 2560, ResolutionHeight = 1600, PanelType = "IPS" },
            new Display { Id = 2, Size = 15.6, ResolutionWidth = 1920, ResolutionHeight = 1080, PanelType = "IPS" },
            new Display { Id = 3, Size = 13.3, ResolutionWidth = 1920, ResolutionHeight = 1080, PanelType = "OLED" },
            new Display { Id = 4, Size = 14.0, ResolutionWidth = 1920, ResolutionHeight = 1080, PanelType = "IPS" },
            new Display { Id = 5, Size = 12.3, ResolutionWidth = 2736, ResolutionHeight = 1824, PanelType = "IPS" },
            new Display { Id = 6, Size = 12.0, ResolutionWidth = 2160, ResolutionHeight = 1440, PanelType = "AMOLED" }
        );

        modelBuilder.Entity<Memory>().HasData(
            new Memory { Id = 1, Capacity = 8, Speed = 2666 },
            new Memory { Id = 2, Capacity = 16, Speed = 2666 },
            new Memory { Id = 3, Capacity = 32, Speed = 2666 },
            new Memory { Id = 4, Capacity = 8, Speed = 3200 },
            new Memory { Id = 5, Capacity = 16, Speed = 3200 },
            new Memory { Id = 6, Capacity = 32, Speed = 3200 }
        );

        modelBuilder.Entity<Storage>().HasData(
            new Storage { Id = 1, Capacity = 256, Type = "SSD" },
            new Storage { Id = 2, Capacity = 512, Type = "SSD" },
            new Storage { Id = 3, Capacity = 1024, Type = "SSD" },
            new Storage { Id = 4, Capacity = 256, Type = "HDD" },
            new Storage { Id = 5, Capacity = 512, Type = "HDD" },
            new Storage { Id = 6, Capacity = 1024, Type = "HDD" }
        );

        modelBuilder.Entity<Notebook>().HasData(
            new Notebook
            {
                Id = 1,
                BrandId = 1,
                ModelId = 1,
                CpuId = 1,
                DisplayId = 1,
                MemoryId = 1,
                StorageId = 1,
                Color = "Space Gray",
                Price = 1299
            },
            new Notebook
            {
                Id = 2,
                BrandId = 2,
                ModelId = 2,
                CpuId = 2,
                DisplayId = 2,
                MemoryId = 2,
                StorageId = 2,
                Color = "Platinum Silver",
                Price = 1199
            },
            new Notebook
            {
                Id = 3,
                BrandId = 3,
                ModelId = 3,
                CpuId = 3,
                DisplayId = 3,
                MemoryId = 3,
                StorageId = 3,
                Color = "Dark Ash Silver",
                Price = 1399
            },
            new Notebook
            {
                Id = 4,
                BrandId = 4,
                ModelId = 4,
                CpuId = 4,
                DisplayId = 4,
                MemoryId = 4,
                StorageId = 4,
                Color = "Black",
                Price = 999
            },
            new Notebook
            {
                Id = 5,
                BrandId = 5,
                ModelId = 5,
                CpuId = 5,
                DisplayId = 5,
                MemoryId = 5,
                StorageId = 5,
                Color = "Platinum",
                Price = 1099
            },
            new Notebook
            {
                Id = 6,
                BrandId = 6,
                ModelId = 6,
                CpuId = 6,
                DisplayId = 6,
                MemoryId = 6,
                StorageId = 6,
                Color = "Mystic Bronze",
                Price = 1299
            }
        );
    }
}
