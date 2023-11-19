using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using tp3.Models;

namespace tp3.Data
{
    public partial class tp3Context : DbContext
    {
        public tp3Context()
        {
        }

        public tp3Context(DbContextOptions<tp3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Aluno> Aluno { get; set; } = null!;
        public virtual DbSet<Curso> Curso { get; set; } = null!;
        public virtual DbSet<Disciplina> Disciplina { get; set; } = null!;
        public virtual DbSet<Professor> Professor { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=tp3;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Aluno__A9D10534813C8367")
                    .IsUnique();

                entity.Property(e => e.AlunoId).HasColumnName("AlunoID");

                entity.Property(e => e.DataNascimento).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Nome).HasMaxLength(255);
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.Property(e => e.CursoId).HasColumnName("CursoID");

                entity.Property(e => e.AlunoId).HasColumnName("AlunoID");

                entity.Property(e => e.DataFim).HasColumnType("date");

                entity.Property(e => e.DataInicio).HasColumnType("date");

                entity.Property(e => e.DisciplinaId).HasColumnName("DisciplinaID");

                entity.Property(e => e.Nome).HasMaxLength(255);

                entity.HasOne(d => d.Aluno)
                    .WithMany(p => p.Curso)
                    .HasForeignKey(d => d.AlunoId)
                    .HasConstraintName("FK__Curso__AlunoID__2D27B809");

                entity.HasOne(d => d.Disciplina)
                    .WithMany(p => p.Curso)
                    .HasForeignKey(d => d.DisciplinaId)
                    .HasConstraintName("FK__Curso__Disciplin__2E1BDC42");
            });

            modelBuilder.Entity<Disciplina>(entity =>
            {
                entity.Property(e => e.DisciplinaId).HasColumnName("DisciplinaID");

                entity.Property(e => e.Nome).HasMaxLength(255);

                entity.Property(e => e.ProfessorId).HasColumnName("ProfessorID");

                entity.HasOne(d => d.Professor)
                    .WithMany(p => p.Disciplina)
                    .HasForeignKey(d => d.ProfessorId)
                    .HasConstraintName("FK__Disciplin__Profe__2A4B4B5E");
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Professo__A9D105346DCC9466")
                    .IsUnique();

                entity.Property(e => e.ProfessorId).HasColumnName("ProfessorID");

                entity.Property(e => e.DataContratacao).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Nome).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
