using Eventos.IO.Domain.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eventos.IO.Domain.Models
{
    public class Evento : Entity<Evento>
    {
        public Evento(string nome, 
                      DateTime dataInicio,
                      DateTime datafim, 
                      bool gratuito,
                      decimal valor ,
                      bool online, 
                      string nomeEmpresa)
       
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = datafim;
            Gratuito = gratuito;
            Valor = valor;
            OnLine = online;
            NomeEmpresa = nomeEmpresa;


            //ErrosValidacao = new Dictionary<string, string>();

            //if (nome.Length < 3)
            //    ErrosValidacao.Add("Nome","O nome do evento deve ter mais de 3 caracteres");

            //if (gratuito && valor != 0)
            //    ErrosValidacao.Add("Valor", "Não pode ter valor se gratuito");

            //if (ErrosValidacao.Any())
            //    throw new Exception("A Entidade possui " + ErrosValidacao.Count() + " Erros");

        }


        public string Nome { get; private set; }
        public string DescricaoCurta { get; private set; }
        public string DescricaoLonga { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public bool Gratuito { get; private set; }
        public decimal Valor { get; private set; }
        public bool OnLine { get; private set; }
        public string NomeEmpresa { get; private set; }
        public Categoria Categoria { get; private set; }
        public ICollection<Tags> Tags { get; private set; }
        public Endereco Endereco { get; private set; }
        public Organizador Organizador { get; private set; }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        // public Dictionary<string, string> ErrosValidacao { get; set; }
        #region Validações

        private void Validar()
        {
            ValidarNome();
            ValidarValor();
            ValidarData();
            ValidarLocal();
            ValidarNomeEmpresa();

            ValidationResult = Validate(this);
        }



        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do evento precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome do evento precisa ter entre 2 e 150 caracteres ");
        }

        private void ValidarValor()
        {
            if(!Gratuito)
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(1, 50000).When(e => e.Gratuito)
                    .WithMessage("O valor não deve ser entre 1.00 e 50.00 ");

            if(Gratuito)
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(0, 0).When(e => e.Gratuito)
                    .WithMessage("O valor não deve ser diferente de 0 para um evento gratuito");
        }

        private void ValidarData()
        {
            RuleFor(c => c.DataInicio)
                .GreaterThan(c => c.DataFim)
                .WithMessage("A data de início deve ser maior que a data do final do evento");

            RuleFor(c => c.DataInicio)
                .LessThan(DateTime.Now)
                .WithMessage("A data de início deve ser menor que a data atual");
        }

        private void ValidarLocal()
        {
            if (OnLine)
                RuleFor(c => c.Endereco)
                .Null().When(c => c.OnLine)
                .WithMessage("O evento não deve possuir um endereço se for online");

            if (!OnLine)
                RuleFor(c => c.Endereco)
                .NotNull().When(c => c.OnLine == false)
                .WithMessage("O evento deve possuir um endereço");
        }

        private void ValidarNomeEmpresa()
        {
            RuleFor(c => c.NomeEmpresa)
                .NotEmpty().WithMessage("O nome do organizador precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome do organizador precisa ter entre 2 e 150 caracteres");
        }

        #endregion
    }


    

}
