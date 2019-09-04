using Cubo.Curso.WebApi.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo.Curso.WebApi.Dominio.Interfaces
{
    public interface IServicos<T> where T : EntidadeBase
    {
        void Post<V>(T obj) where V : AbstractValidator<T>;
        void Put<V>(T obj) where V : AbstractValidator<T>;
        void Delete(int id);
        T Get(int id);
        IList<T> Get();
    }
}
