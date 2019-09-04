using Cubo.Curso.WebApi.Dominio.Entidades;
using Cubo.Curso.WebApi.Infraestrutura.Data.Contexto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace Cubo.Curso.WebApi.Infraestrutura.Data.Mapeamento
{
    public class ClienteMap : EntityTypeConfiguration<Cliente>
    {
        
       public ClienteMap()
        {
            TypeDescriptor.AddAttributes(typeof(Cliente), new SequenciaOracle($"SYS.CLIENTE_SEQ"));

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
