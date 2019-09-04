using Cubo.Curso.WebApi.Dominio.Entidades;
using Cubo.Curso.WebApi.Dominio.Interfaces;
using Cubo.Curso.WebApi.Infraestrutura.Data.Repositorio;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Cubo.Curso.WebApi.Servicos.Servicos
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
        
        public void Post<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());
            repository.Insert(obj);
        }

        public void Put<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());
            repository.Update(obj);
        }

        private void Validate(T obj, AbstractValidator<T> validador)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validador.ValidateAndThrow(obj);
        }
    }
}
