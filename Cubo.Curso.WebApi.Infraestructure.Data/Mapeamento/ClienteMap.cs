using Cubo.Curso.WebApi.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo.Curso.WebApi.Infraestructure.Data.Mapeamento
{
    public class ClienteMap : EntityTypeConfiguration<Cliente>
    {
       public ClienteMap()
        {
            ToTable("CLIENTE", "SYS");

            HasKey(c => c.Id);

            Property(c => c.Id)
                .HasColumnName("ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(c => c.Nome)
                .IsRequired()
                .HasColumnName("NOME");

            Property(c => c.Endereco)
                .IsRequired()
                .HasColumnName("ENDERECO");

            Property(c => c.Cep)
                .IsRequired()
                .HasColumnName("CEP");

            Property(c => c.Bairro)
               .IsRequired()
               .HasColumnName("BAIRRO");

            Property(c => c.Uf)
               .IsRequired()
               .HasColumnName("UF");

            Property(c => c.Telefone)
               .IsRequired()
               .HasColumnName("TELEFONE");

        }
    }
   
}
