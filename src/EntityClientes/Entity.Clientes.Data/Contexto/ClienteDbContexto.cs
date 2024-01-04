using System;
using System.Collections.Generic;
using Entity.Clientes.Data.MapeamentoEntidades;
using Entity.Clientes.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Entity.Clientes.Domain.Entidades;

public partial class ClienteDbContexto : DbContext, IUnitOfWork
{
    public ClienteDbContexto()
    {
    }

    public ClienteDbContexto(DbContextOptions<ClienteDbContexto> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

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

        modelBuilder.ApplyConfiguration<Cliente>(new ClienteMapeamento());
        modelBuilder.ApplyConfiguration<Endereco>(new EnderecoMapeamento());


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

     public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
}
