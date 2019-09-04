using Cubo.Curso.WebApi.Domain.Entidades;
using Cubo.Curso.WebApi.Domain.Interfaces;
using Cubo.Curso.WebApi.Infraestructure.Data.Repositorio;
using System;
using System.Collections.Generic;

namespace Cubo.Curso.WebApi.Services.Servicos
{
    public class ServicoBase<T> : IServicos<T> where T : EntidadeBase
    {

        private RepositorioBase<T> repository = new RepositorioBase<T>(); 
        public void Delete(int id)
        {
            if (id == 0)
                throw new ArgumentException("O Id não pode ser 0.");
            repository.Delete(id);
        }

        public T Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("O Id não pode ser 0.");
            return repository.Select(id);
        }

        public IList<T> Get() => repository.Select();
        
        public void Post(T obj)
        {
            repository.Insert(obj);
        }

        public void Put(T obj)
        {
            repository.Update(obj);
        }
    }
}
