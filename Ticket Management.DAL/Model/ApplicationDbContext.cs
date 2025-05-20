using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticket_Management.DAL.Data;
namespace Ticket_Management.DAL.Model;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerTicket> CustomerTickets { get; set; }

    public virtual DbSet<IssueType> IssueTypes { get; set; }

    public virtual DbSet<PriorityType> PriorityTypes { get; set; }

    public virtual DbSet<StatusType> StatusTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HASNAA-ABDELRAH;Database=Ticket Management;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC2731D52BB5");

            entity.ToTable("Customers", "Customers_INFO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(225);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(256);
        });

        modelBuilder.Entity<CustomerTicket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Customer__712CC62750172207");

            entity.ToTable("CustomerTickets", "Tickets_Services");

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.CreatedDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(225);
            entity.Property(e => e.IssueTypeId).HasColumnName("IssueTypeID");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PriorityId).HasColumnName("PriorityID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerTickets)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CustomerTickets_Customers");

            entity.HasOne(d => d.IssueType).WithMany(p => p.CustomerTickets)
                .HasForeignKey(d => d.IssueTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerTickets_IssueType");

            entity.HasOne(d => d.Priority).WithMany(p => p.CustomerTickets)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerTickets_PriorityTypes");

            entity.HasOne(d => d.Status).WithMany(p => p.CustomerTickets)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerTickets_StatusTypes");
        });

        modelBuilder.Entity<IssueType>(entity =>
        {
            entity.HasKey(e => e.IssueTypeId).HasName("PK__IssueTyp__6CC41B598F5530D8");

            entity.ToTable("IssueTypes", "Tickets_order");

            entity.Property(e => e.IssueTypeId).HasColumnName("IssueTypeID");
            entity.Property(e => e.IssueTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PriorityType>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("PK__Priority__D0A3D0DE41822A22");

            entity.ToTable("PriorityTypes", "Tickets_order");

            entity.Property(e => e.PriorityId).HasColumnName("PriorityID");
            entity.Property(e => e.PriorityName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusType>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__StatusTy__C8EE204344AAF843");

            entity.ToTable("StatusTypes", "Tickets_order");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
