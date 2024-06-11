﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotebookStoreContext;

#nullable disable

namespace NotebookStoreContext.Migrations
{
    [DbContext(typeof(NotebookStoreContext))]
    partial class NotebookStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.18");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Editor",
                            NormalizedName = "EDITOR"
                        },
                        new
                        {
                            Id = "3",
                            Name = "Viewer",
                            NormalizedName = "VIEWER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d7c686e2-9b77-4049-94ed-d4310081b475",
                            Email = "admin@admin.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.COM",
                            NormalizedUserName = "ADMIN@ADMIN.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEObH4fcXKI5vGEtFuokTLKRAVaGMWqd0Bjd443CDZLo3rdWMQY/u7wSsMMwJGnn8vQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e46eb34b-a6af-4393-8b9c-1d01b7d4f89b",
                            TwoFactorEnabled = false,
                            UserName = "admin@admin.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            RoleId = "1"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NotebookStore.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "Apple"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "Dell"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "HP"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "Lenovo"
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "Microsoft"
                        },
                        new
                        {
                            Id = 6,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "Samsung"
                        });
                });

            modelBuilder.Entity("NotebookStore.Entities.Cpu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Brand", "Model")
                        .IsUnique();

                    b.ToTable("Cpus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Intel",
                            CreatedAt = "2024-06-06 13:04:34",
                            Model = "Core i5"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Intel",
                            CreatedAt = "2024-06-06 13:04:34",
                            Model = "Core i7"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Intel",
                            CreatedAt = "2024-06-06 13:04:34",
                            Model = "Core i9"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "AMD",
                            CreatedAt = "2024-06-06 13:04:34",
                            Model = "Ryzen 5"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "AMD",
                            CreatedAt = "2024-06-06 13:04:34",
                            Model = "Ryzen 7"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "AMD",
                            CreatedAt = "2024-06-06 13:04:34",
                            Model = "Ryzen 9"
                        });
                });

            modelBuilder.Entity("NotebookStore.Entities.Display", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("PanelType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<int>("ResolutionHeight")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ResolutionWidth")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Size")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("Size", "ResolutionWidth", "ResolutionHeight", "PanelType")
                        .IsUnique();

                    b.ToTable("Displays");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = "2024-06-06 13:04:34",
                            PanelType = "IPS",
                            ResolutionHeight = 1600,
                            ResolutionWidth = 2560,
                            Size = 13.300000000000001
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = "2024-06-06 13:04:34",
                            PanelType = "IPS",
                            ResolutionHeight = 1080,
                            ResolutionWidth = 1920,
                            Size = 15.6
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = "2024-06-06 13:04:34",
                            PanelType = "OLED",
                            ResolutionHeight = 1080,
                            ResolutionWidth = 1920,
                            Size = 13.300000000000001
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = "2024-06-06 13:04:34",
                            PanelType = "IPS",
                            ResolutionHeight = 1080,
                            ResolutionWidth = 1920,
                            Size = 14.0
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = "2024-06-06 13:04:34",
                            PanelType = "IPS",
                            ResolutionHeight = 1824,
                            ResolutionWidth = 2736,
                            Size = 12.300000000000001
                        },
                        new
                        {
                            Id = 6,
                            CreatedAt = "2024-06-06 13:04:34",
                            PanelType = "AMOLED",
                            ResolutionHeight = 1440,
                            ResolutionWidth = 2160,
                            Size = 12.0
                        });
                });

            modelBuilder.Entity("NotebookStore.Entities.Memory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("Speed")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Capacity", "Speed")
                        .IsUnique();

                    b.ToTable("Memories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 8,
                            CreatedAt = "2024-06-06 13:04:34",
                            Speed = 2666
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 16,
                            CreatedAt = "2024-06-06 13:04:34",
                            Speed = 2666
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 32,
                            CreatedAt = "2024-06-06 13:04:34",
                            Speed = 2666
                        },
                        new
                        {
                            Id = 4,
                            Capacity = 8,
                            CreatedAt = "2024-06-06 13:04:34",
                            Speed = 3200
                        },
                        new
                        {
                            Id = 5,
                            Capacity = 16,
                            CreatedAt = "2024-06-06 13:04:34",
                            Speed = 3200
                        },
                        new
                        {
                            Id = 6,
                            Capacity = 32,
                            CreatedAt = "2024-06-06 13:04:34",
                            Speed = 3200
                        });
                });

            modelBuilder.Entity("NotebookStore.Entities.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Models");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "MacBook Pro"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "XPS"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "Spectre"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "ThinkPad"
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "Surface"
                        },
                        new
                        {
                            Id = 6,
                            CreatedAt = "2024-06-06 13:04:34",
                            Name = "Galaxy Book"
                        });
                });

            modelBuilder.Entity("NotebookStore.Entities.Notebook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BrandId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("CpuId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("DisplayId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MemoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ModelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StorageId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CpuId");

                    b.HasIndex("DisplayId");

                    b.HasIndex("MemoryId");

                    b.HasIndex("ModelId");

                    b.HasIndex("StorageId");

                    b.HasIndex("BrandId", "ModelId", "CpuId", "DisplayId", "MemoryId", "StorageId", "Color", "Price")
                        .IsUnique();

                    b.ToTable("Notebooks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 1,
                            Color = "Space Gray",
                            CpuId = 1,
                            CreatedAt = "2024-06-06 13:04:34",
                            DisplayId = 1,
                            MemoryId = 1,
                            ModelId = 1,
                            Price = 1299,
                            StorageId = 1
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 2,
                            Color = "Platinum Silver",
                            CpuId = 2,
                            CreatedAt = "2024-06-06 13:04:34",
                            DisplayId = 2,
                            MemoryId = 2,
                            ModelId = 2,
                            Price = 1199,
                            StorageId = 2
                        },
                        new
                        {
                            Id = 3,
                            BrandId = 3,
                            Color = "Dark Ash Silver",
                            CpuId = 3,
                            CreatedAt = "2024-06-06 13:04:34",
                            DisplayId = 3,
                            MemoryId = 3,
                            ModelId = 3,
                            Price = 1399,
                            StorageId = 3
                        },
                        new
                        {
                            Id = 4,
                            BrandId = 4,
                            Color = "Black",
                            CpuId = 4,
                            CreatedAt = "2024-06-06 13:04:34",
                            DisplayId = 4,
                            MemoryId = 4,
                            ModelId = 4,
                            Price = 999,
                            StorageId = 4
                        },
                        new
                        {
                            Id = 5,
                            BrandId = 5,
                            Color = "Platinum",
                            CpuId = 5,
                            CreatedAt = "2024-06-06 13:04:34",
                            DisplayId = 5,
                            MemoryId = 5,
                            ModelId = 5,
                            Price = 1099,
                            StorageId = 5
                        },
                        new
                        {
                            Id = 6,
                            BrandId = 6,
                            Color = "Mystic Bronze",
                            CpuId = 6,
                            CreatedAt = "2024-06-06 13:04:34",
                            DisplayId = 6,
                            MemoryId = 6,
                            ModelId = 6,
                            Price = 1299,
                            StorageId = 6
                        });
                });

            modelBuilder.Entity("NotebookStore.Entities.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Capacity", "Type")
                        .IsUnique();

                    b.ToTable("Storages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 256,
                            CreatedAt = "2024-06-06 13:04:34",
                            Type = "SSD"
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 512,
                            CreatedAt = "2024-06-06 13:04:34",
                            Type = "SSD"
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 1024,
                            CreatedAt = "2024-06-06 13:04:34",
                            Type = "SSD"
                        },
                        new
                        {
                            Id = 4,
                            Capacity = 256,
                            CreatedAt = "2024-06-06 13:04:34",
                            Type = "HDD"
                        },
                        new
                        {
                            Id = 5,
                            Capacity = 512,
                            CreatedAt = "2024-06-06 13:04:34",
                            Type = "HDD"
                        },
                        new
                        {
                            Id = 6,
                            Capacity = 1024,
                            CreatedAt = "2024-06-06 13:04:34",
                            Type = "HDD"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotebookStore.Entities.Notebook", b =>
                {
                    b.HasOne("NotebookStore.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotebookStore.Entities.Cpu", "Cpu")
                        .WithMany()
                        .HasForeignKey("CpuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotebookStore.Entities.Display", "Display")
                        .WithMany()
                        .HasForeignKey("DisplayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotebookStore.Entities.Memory", "Memory")
                        .WithMany()
                        .HasForeignKey("MemoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotebookStore.Entities.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotebookStore.Entities.Storage", "Storage")
                        .WithMany()
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Cpu");

                    b.Navigation("Display");

                    b.Navigation("Memory");

                    b.Navigation("Model");

                    b.Navigation("Storage");
                });
#pragma warning restore 612, 618
        }
    }
}
