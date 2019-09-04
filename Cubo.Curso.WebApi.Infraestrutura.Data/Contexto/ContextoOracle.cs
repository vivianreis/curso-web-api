using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Reflection;
using Cubo.Curso.WebApi.Dominio.Entidades;
using Cubo.Curso.WebApi.Dominio.ExcecoesCustomizadas;

namespace Cubo.Curso.WebApi.Infraestrutura.Data.Contexto
{
    public class ContextoOracle : DbContext
    {
        public ContextoOracle() : base(ConfigurationManager.AppSettings["ConnectionStringName"])
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties<string>().Configure(c =>
            {
                c.IsUnicode(false);
                c.HasMaxLength(1999);
            });

            modelBuilder.Conventions.AddFromAssembly(typeof(ContextoOracle).Assembly);
            
        }

        public string GetSequenceValue (string sequenceName)
        {
            string command = string.Format("SELECT {0}.NEXTVAL FROM DUAL", sequenceName);

            var oracleCommand = base.Database.Connection.CreateCommand();

            oracleCommand.CommandText = command;

            if(base.Database.Connection.State == System.Data.ConnectionState.Closed)
            {
                base.Database.Connection.Open();
            }

            return oracleCommand.ExecuteScalar().ToString();

        }

        public override int SaveChanges()
        {
            // Obter a instância do Contexto
            ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;

            foreach (var changeSet in ChangeTracker.Entries())
            {
                var entity = changeSet.Entity;

                var entityType = entity.GetType();

                if (entityType == null)
                {
                    continue;
                }

                var entityCustomAttributes = TypeDescriptor.GetAttributes(entityType);

                if (entityCustomAttributes == null)
                {
                    continue;
                }

                if (entityCustomAttributes.Count == 0)
                {
                    continue;
                }

                if (changeSet.State != EntityState.Added)
                {
                    continue;
                }

                foreach (Attribute attribute in entityCustomAttributes)
                {
                    if (attribute is SequenciaOracle)
                    {
                        string sequenceName = ((SequenciaOracle)attribute).NomeSequencia;

                        if (!string.IsNullOrWhiteSpace(sequenceName))
                        {
                            string sequenceValue = GetSequenceValue(sequenceName);

                            // Obter a Entrada da Entidade
                            EntitySetBase entitySet = objectContext
                                                        .ObjectStateManager
                                                        .GetObjectStateEntry(entity)
                                                        .EntitySet;

                            // Obter as Propriedades (Nomes) que compõem o Identificador da Entidade
                            List<string> propertyKeyNames = entitySet
                                                                .ElementType
                                                                .KeyMembers
                                                                .Select(k => k.Name).ToList();

                            // Para entidades definidas com Sequence, não é possível qualquer possibilidade
                            // que a entidade tenha 0 (zero) ou +1 (mais de um) identificador
                            if (propertyKeyNames.Count != 1)
                            {
                                throw new Exception("Mapeamento de chaves incorreto.");
                            }

                            string propertyKeyName = propertyKeyNames[0];

                            // Obter a Propriedade / Identificador da Entidade
                            PropertyInfo propertyKey = entityType.GetProperty(propertyKeyName);

                            // Obter o Tipo da Propridade / Identificador da Entidade
                            // Necessário para tornar possível converter o valor obtido da Sequence
                            // no Tipo correto da Propriedade
                            Type propertyKeyType = Nullable.GetUnderlyingType(propertyKey.PropertyType) ?? propertyKey.PropertyType;

                            // Assegurar o Tipo correto da Propridade de Identificação
                            // Conversão do valor da Sequence de string para Tipo correto
                            object sequenceValueCorrectType = Convert.ChangeType(sequenceValue, propertyKeyType);

                            propertyKey.SetValue(entity, sequenceValueCorrectType);
                        }

                        break;
                    }
                }
            }

            try
            {
                int returnSave = base.SaveChanges();

                base.ChangeTracker.Entries().ToList().ForEach(i =>
                {
                    i.State = EntityState.Detached;
                });

                return returnSave;
            }
            catch (DbUpdateException err)
            {
                List<EstadoEntidade> Entries = new List<EstadoEntidade>();

                foreach (var entry in base.ChangeTracker.Entries())
                {
                    Entries.Add(new EstadoEntidade
                    {
                        Entidade = entry.Entity.GetType().Name,
                        Estado = entry.State.ToString()
                    });
                }

                Exception inner = err;

                while (inner.InnerException != null)
                {
                    inner = inner.InnerException;
                }

                string erroMessage = inner.Message;

                throw new PersistirExcecao(err, Entries, erroMessage);
            }
            catch (Exception err)
            {
                List<EstadoEntidade> Entries = new List<EstadoEntidade>();

                foreach (var entry in base.ChangeTracker.Entries())
                {
                    Entries.Add(new EstadoEntidade
                    {
                        Entidade = entry.Entity.GetType().Name,
                        Estado = entry.State.ToString()
                    });
                }

                throw new PersistirExcecao(err, Entries);
            }
        }


    }
}
