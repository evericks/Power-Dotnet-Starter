using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Context;

public partial class StudentManagementDbContext : DbContext
{
    public StudentManagementDbContext(DbContextOptions<StudentManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<Hash> Hashes { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobParameter> JobParameters { get; set; }

    public virtual DbSet<JobQueue> JobQueues { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<Schema> Schemas { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC07C9FACB3D");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<AggregatedCounter>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("PK_HangFire_CounterAggregated");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Id }).HasName("PK_HangFire_Counter");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Hash>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Field }).HasName("PK_HangFire_Hash");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Job");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName").HasFilter("([StateName] IS NOT NULL)");
        });

        modelBuilder.Entity<JobParameter>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Name }).HasName("PK_HangFire_JobParameter");

            entity.HasOne(d => d.Job).WithMany(p => p.JobParameters).HasConstraintName("FK_HangFire_JobParameter_Job");
        });

        modelBuilder.Entity<JobQueue>(entity =>
        {
            entity.HasKey(e => new { e.Queue, e.Id }).HasName("PK_HangFire_JobQueue");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Id }).HasName("PK_HangFire_List");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Schema>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("PK_HangFire_Schema");

            entity.Property(e => e.Version).ValueGeneratedNever();
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Server");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Value }).HasName("PK_HangFire_Set");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Id }).HasName("PK_HangFire_State");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Job).WithMany(p => p.States).HasConstraintName("FK_HangFire_State_Job");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC076620BDEC");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Account).WithOne(p => p.Student)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
