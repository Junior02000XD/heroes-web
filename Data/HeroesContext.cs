using System;
using System.Collections.Generic;
using HeroesWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HeroesWeb.Data;

public partial class HeroesContext : IdentityDbContext<IdentityUser>
{
    public HeroesContext(DbContextOptions<HeroesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Heroes> Heroes { get; set; }

    public virtual DbSet<SuperPoderes> SuperPoderes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Heroes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Heroes__3214EC07A8A58CBD");
        });

        modelBuilder.Entity<SuperPoderes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SuperPod__3214EC077265C105");

            entity.HasOne(d => d.Heroe).WithMany(p => p.SuperPoderes).HasConstraintName("FK_SuperPoderes_Heroes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
