using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Individuellt_databasproject_V2.Models;

public partial class FinalSchoolAppContext : DbContext
{
    public FinalSchoolAppContext()
    {
    }

    public FinalSchoolAppContext(DbContextOptions<FinalSchoolAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = localhost; Database = Final_School_App;  Integrated Security = true; Trust Server Certificate = true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class__3214EC272C185B64");

            entity.ToTable("Class");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClassName).HasMaxLength(50);
            entity.Property(e => e.MentorId).HasColumnName("MentorID");

            entity.HasOne(d => d.Mentor).WithMany(p => p.Classes)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("FK__Class__MentorID__06CD04F7");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC2703B6AA8B");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepartmentName).HasMaxLength(50);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grade__3214EC27532B78E2");

            entity.ToTable("Grade");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Grade1)
                .HasMaxLength(2)
                .HasColumnName("Grade");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

            entity.HasOne(d => d.Staff).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Grade__StaffID__114A936A");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Grade__StudentID__0F624AF8");

            entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Grade__SubjectID__10566F31");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Position__3214EC270CF12F36");

            entity.HasIndex(e => e.PositionName, "UQ__Position__E46AEF4263E6C0B9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PositionName).HasMaxLength(50);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC270DA7A414");

            entity.HasIndex(e => e.SocialSecurityNumber, "UQ__Staff__2EFDFD393C20E852").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(70);
            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SocialSecurityNumber).HasMaxLength(13);

            entity.HasOne(d => d.Department).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Staff__Departmen__03F0984C");

            entity.HasOne(d => d.Position).WithMany(p => p.Staff)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Staff__PositionI__02FC7413");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC27B3769A4A");

            entity.ToTable("Student");

            entity.HasIndex(e => e.SocialSecurityNumber, "UQ__Student__2EFDFD39F038FD22").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(70);
            entity.Property(e => e.SocialSecurityNumber).HasMaxLength(13);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Student__ClassID__0A9D95DB");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subjects__3214EC2795CA3FB9");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SubjectName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
