using Cubo.Curso.WebApi.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo.Curso.WebApi.Domain.Interfaces
{
    public interface IServicos<T> where T : EntidadeBase
    {
        void Post(T obj);
        void Put(T obj);
        void Delete(int id);
        T Get(int id);
        IList<T> Get();
    }
}
