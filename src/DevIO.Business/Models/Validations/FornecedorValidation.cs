using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Business.Models.Validations
{
    class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(c => c.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(c => c.Documento.Length)
                .Equal(CpfValidacao.ValorMaxCpf)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

                RuleFor(c => CpfValidacao.Validar(c.Documento))
                .Equal(true)
                .WithMessage("O documento fornecido é inválido");
            });

            When(c => c.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(c => c.Documento.Length)
                .Equal(CnpjValidacao.ValorMaxCnpj)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

                RuleFor(c => CnpjValidacao.Validar(c.Documento))
                .Equal(true)
                .WithMessage("O documento fornecido é inválido");
            });
        }
    }
}

