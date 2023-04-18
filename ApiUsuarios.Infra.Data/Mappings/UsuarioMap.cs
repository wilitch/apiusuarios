using ApiUsuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento ORM para a entidade Usuário
    /// </summary>
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //nome da tabela do banco de dados
            builder.ToTable("USUARIO");

            //campo chave primária
            builder.HasKey(u => u.IdUsuario);

            //mapeamento dos campos
            builder.Property(u => u.IdUsuario)
                .HasColumnName("IDUSUARIO");

            builder.Property(u => u.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.DataHoraCriacao)
                .HasColumnName("DATAHORACRIACAO")
                .IsRequired();
        }
    }
}



