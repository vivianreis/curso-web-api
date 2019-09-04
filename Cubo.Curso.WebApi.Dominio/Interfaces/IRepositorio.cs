using Cubo.Curso.WebApi.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo.Curso.WebApi.Dominio.Interfaces
{
    public interface IRepositorio<T> where T : EntidadeBase
    {
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        T Select(int id);
        IList<T> Select();

    }
}
