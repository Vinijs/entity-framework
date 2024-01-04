using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Produtos.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Entity.Produtos.Data.MapeamentosEntidades
{
    public class EnderecoMapeamento : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("enderecos");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Id, "IX_enderecos_id");

            builder.Property(e => e.Id)
                   .HasColumnName("id")
                   .HasColumnType("int");

            builder.Property(e => e.Logradouro)
                   .HasColumnName("logradouro")
                   .HasColumnType("varchar(255)")
                   .IsRequired();

            builder.Property(e => e.Cep)
                   .HasColumnName("cep")
                   .HasColumnType("varchar(10)")
                   .IsRequired();

            builder.Property(e => e.Bairro)
                   .HasColumnName("bairro")
                   .HasColumnType("varchar(150)")
                   .IsRequired();

            builder.Property(e => e.Numero)
                   .HasColumnName("numero")
                   .HasColumnType("varchar(30)")
                   .IsRequired();

            builder.Property(e => e.Complemento)
                   .HasColumnName("complemento")
                   .HasColumnType("varchar(255)")
                   .IsRequired();

            builder.Property(e => e.Cidade)
                   .HasColumnName("cidade")
                   .HasColumnType("varchar(150)")
                   .IsRequired();

            builder.Property(e => e.Estado)
                   .HasColumnName("estado")
                   .HasColumnType("varchar(2)")
                   .IsRequired();

            builder.HasMany(e => e.Fornecedores)
                   .WithOne(f => f.Endereco)
                   .HasForeignKey(f => f.EnderecoId)
                   .HasConstraintName("FK_fornecedores_enderecos_endereco_id");
        }
    }
}