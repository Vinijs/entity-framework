using Microsoft.EntityFrameworkCore;
using Entity.Pedidos.Domain.Entidades;
using Entity.Pedidos.Domain.Repositories;

namespace Entity.Pedidos.Data.Contexto;

public partial class PedidoDbContexto : DbContext, IUnitOfWork
{
    public PedidoDbContexto()
    {
    }

    public PedidoDbContexto(DbContextOptions<PedidoDbContexto> options)
        : base(options)
    {
    }

    public virtual DbSet<Endereco> Enderecos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoItem> PedidoItems { get; set; }

    public virtual DbSet<CupomDesconto> CupomDescontos { get; set; }

    


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
             optionsBuilder.UseMySql("server=localhost;database=EntityFrameworkComunidade;uid=root;pwd=carroforte", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidoDbContexto).Assembly);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
}
