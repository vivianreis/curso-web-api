using Cubo.Curso.WebApi.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace Cubo.Curso.WebApi.Dominio.ExcecoesCustomizadas
{
    public class PersistirExcecao : Exception
    {
        public string MensagemErro { get; set; }

        public Exception ExcecaoOriginal { get; set; }

        public List<EstadoEntidade> Entidades { get; set; }

        public PersistirExcecao(Exception excecaoOriginal, List<EstadoEntidade> entidades)
        {
            ExcecaoOriginal = excecaoOriginal;
            Entidades = entidades;
        }

        public PersistirExcecao(Exception excecaoOriginal, List<EstadoEntidade> entidades, string mensagemErro)
        {
            ExcecaoOriginal = excecaoOriginal;
            Entidades = entidades;
            MensagemErro = mensagemErro;
        }

    }
}
