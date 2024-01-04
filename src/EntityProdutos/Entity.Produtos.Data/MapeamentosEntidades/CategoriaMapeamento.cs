using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Produtos.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Entity.Produtos.Data.MapeamentosEntidades
{
    public class CategoriaMapeamento : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("categorias");

            builder.HasKey(c => c.Id)
                   .HasName("PK_categorias");
            
            builder.HasIndex(c => c.Id, "IX_categorias_id");

            builder.Property(c => c.Id)
                   .HasColumnName("id")
                   .HasColumnType("int");

            builder.Property(c => c.Descricao)
                   .HasColumnName("descricao")
                   .HasColumnType("varchar(250)");

            //Definição opcional pois categoria não guarda a chave do relacionamento
            builder.HasMany(c => c.Produtos)
                    .WithOne(p => p.Categoria)
                    .HasForeignKey(p => p.CategoriaId);

              builder.HasData(new List<Categoria> {
                     new Categoria
                     {
                            Id = 1,
                            Descricao = "Alimentos"
                     },
                     new Categoria
                     {
                            Id = 2,
                            Descricao = "Bebidas"
                     }

              });

        }
    }
}