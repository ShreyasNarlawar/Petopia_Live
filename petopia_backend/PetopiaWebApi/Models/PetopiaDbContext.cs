using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PetopiaWebApi.Models;

public partial class PetopiaDbContext : DbContext
{
    public PetopiaDbContext()
    {
    }

    public PetopiaDbContext(DbContextOptions<PetopiaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdoptionStatus> AdoptionStatuses { get; set; }

    public virtual DbSet<Breed> Breeds { get; set; }

    public virtual DbSet<PetCategory> PetCategories { get; set; }

    public virtual DbSet<PetImage> PetImages { get; set; }

    public virtual DbSet<PetProfile> PetProfiles { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__admin__43AA41416FA8F5DF");

            entity.ToTable("admin");

            entity.HasIndex(e => e.Email, "UQ__admin__AB6E6164AA1BBB67").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<AdoptionStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__adoption__3683B53114E6A628");

            entity.ToTable("adoption_status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Pet).WithMany(p => p.AdoptionStatuses)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("FK__adoption___pet_i__5629CD9C");

            entity.HasOne(d => d.User).WithMany(p => p.AdoptionStatuses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__adoption___user___571DF1D5");
        });

        modelBuilder.Entity<Breed>(entity =>
        {
            entity.HasKey(e => e.BreedId).HasName("PK__breed__9C02143553FA916A");

            entity.ToTable("breed");

            entity.Property(e => e.BreedId).HasColumnName("breed_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Category).WithMany(p => p.Breeds)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__breed__category___3F466844");
        });

        modelBuilder.Entity<PetCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__pet_cate__D54EE9B4CF21D854");

            entity.ToTable("pet_category");

            entity.HasIndex(e => e.Name, "UQ__pet_cate__72E12F1BF2955269").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PetImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__pet_imag__DC9AC9554D3966CD");

            entity.ToTable("pet_images");

            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasColumnName("image_path");
            entity.Property(e => e.PetId).HasColumnName("pet_id");

            entity.HasOne(d => d.Pet).WithMany(p => p.PetImages)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("FK__pet_image__pet_i__52593CB8");
        });

        modelBuilder.Entity<PetProfile>(entity =>
        {
            entity.HasKey(e => e.PetId).HasName("PK__pet_prof__390CC5FE6B06AC88");

            entity.ToTable("pet_profile");

            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.BreedId).HasColumnName("breed_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.GoodWithKids).HasColumnName("good_with_kids");
            entity.Property(e => e.GoodWithPets).HasColumnName("good_with_pets");
            entity.Property(e => e.Health).HasColumnName("health");
            entity.Property(e => e.HouseTrained).HasColumnName("house_trained");
            entity.Property(e => e.IsRegisteredWithGovt)
                .HasMaxLength(3)
                .HasDefaultValue("no")
                .HasColumnName("isRegisteredWithGovt");
            entity.Property(e => e.MonthlyExpenses)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monthly_expenses");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Needs).HasColumnName("needs");
            entity.Property(e => e.Personality).HasColumnName("personality");
            entity.Property(e => e.Size)
                .HasMaxLength(10)
                .HasColumnName("size");
            entity.Property(e => e.Spayed).HasColumnName("spayed");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Vaccinated).HasColumnName("vaccinated");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.Breed).WithMany(p => p.PetProfiles)
                .HasForeignKey(d => d.BreedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pet_profi__breed__4E88ABD4");

            entity.HasOne(d => d.Category).WithMany(p => p.PetProfiles)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pet_profi__categ__4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.PetProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pet_profi__user___4F7CD00D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F1C04A088");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E616403F5D9A0").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(15)
                .HasColumnName("phone_no");
            entity.Property(e => e.UserRole)
                .HasMaxLength(10)
                .HasColumnName("user_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
