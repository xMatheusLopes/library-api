﻿using System;
using System.Linq;
using library_api.Config;
using library_api.Entities;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<GeneralStatus> GeneralStatuses { get; set; }
    public DbSet<Book> Books { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GeneralStatusConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
        foreach (var entityEntry in entries)
        {
            try
            {
                entityEntry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedAt").CurrentValue = DateTime.Now;
                } else {
                    entityEntry.Property("CreatedAt").IsModified = false;
                }
            }
            catch (Exception e)
            {
                // Objeto não possui a propriedade
                continue;
            }
        }

        return base.SaveChanges();
    }
}