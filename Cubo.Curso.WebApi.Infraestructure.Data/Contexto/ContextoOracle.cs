using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo.Curso.WebApi.Infraestructure.Data.Contexto
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
            modelBuilder.Properties<string>().Configure(c =>
            {
                c.IsUnicode(false);
                c.HasMaxLength(1999);
            });
        }

    }
}
