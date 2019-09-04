using Cubo.Curso.WebApi.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo.Curso.WebApi.Dominio.Validacoes
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(c => c)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Objeto não enconstrado.");
                });

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("É necessário informar o Nome.")
                .NotNull().WithMessage("É necessário informar o Nome.");
        }
    }
}
