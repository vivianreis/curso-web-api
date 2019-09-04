using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo.Curso.WebApi.Infraestrutura.Data.Contexto
{
    public class SequenciaOracle : Attribute
    {
        public string NomeSequencia { get; set; }

        public SequenciaOracle(string nomeSequencia)
        {
            NomeSequencia = nomeSequencia;
        }
    }
}
