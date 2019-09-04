using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo.Curso.WebApi.Dominio.Entidades
{
    public class Cliente : EntidadeBase
    {
        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string Cep { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string Telefone { get; set; }

    }
}
