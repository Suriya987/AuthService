using System;
using System.Collections.Generic;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DbContexts;

public partial class ChatApplicationDbContext : DbContext
{
    public ChatApplicationDbContext()
    {
    }

    public ChatApplicationDbContext(DbContextOptions<ChatApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthCredential> AuthCredentials { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ChatApplication;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthCredential>(entity =>
        {
            entity.HasKey(e => e.AuthCredentialId).HasName("PK__AuthCred__5DB27DEF5D08BDD2");

            entity.Property(e => e.AuthCredentialId).HasColumnName("authCredentialId");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DeviceName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordChangedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("passwordChangedAt");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("passwordHash");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("PK__RefreshT__FEAC95C8E6892A9D");

            entity.HasIndex(e => e.TokenHash, "IX_RefreshTokens_TokenHash");

            entity.HasIndex(e => e.UserId, "IX_RefreshTokens_UserId");

            entity.Property(e => e.RefreshTokenId).HasColumnName("refreshTokenId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdAt");
            entity.Property(e => e.ExpiresAt).HasColumnName("expiresAt");
            entity.Property(e => e.RevokedAt).HasColumnName("revokedAt");
            entity.Property(e => e.TokenHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tokenHash");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
