﻿using Microsoft.EntityFrameworkCore;
using Entity.Produtos.Entidades;
using Microsoft.Extensions.Logging;
using Entity.Produtos.Domain.Repositories;

namespace Entity.Produtos.Data.Contexto;

public partial class ProdutosDbContexto : DbContext, IUnitOfWork
{
    public ProdutosDbContexto()
    {
    }

    public ProdutosDbContexto(DbContextOptions<ProdutosDbContexto> options)
        : base(options)
    {
    }

    public virtual DbSet<Produto> Produtos { get; set; }
    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Fornecedor> Fornecedores { get; set; }
    public virtual DbSet<Endereco> Enderecos { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("server=localhost;database=EntityFrameworkComunidade;uid=root;pwd=carroforte", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"))
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }

        optionsBuilder.EnableSensitiveDataLogging()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProdutosDbContexto).Assembly);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
}
