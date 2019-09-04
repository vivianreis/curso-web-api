using Cubo.Curso.WebApi.Domain.Entidades;
using Cubo.Curso.WebApi.Domain.Interfaces;
using Cubo.Curso.WebApi.Infraestructure.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cubo.Curso.WebApi.Infraestructure.Data.Repositorio
{
    public class RepositorioBase<T> : IRepositorio<T>, IDisposable where T : EntidadeBase
    {
        private ContextoOracle context = new ContextoOracle();
        public void Delete(int id)
        {
            context.Set<T>().Remove(Select(id));
            context.SaveChanges();
        }

        public void Delete(T obj)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();
        }

        public T Select(int id)
        {
            return context.Set<T>().Find(id);
        }

        public IList<T> Select()
        {
            return context.Set<T>().ToList();
        }

        public void Update(T obj)
        {
            context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
