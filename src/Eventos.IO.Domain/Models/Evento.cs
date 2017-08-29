using Eventos.IO.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eventos.IO.Domain.Models
{
    public class Evento : Entity
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


            ErrosValidacao = new Dictionary<string, string>();

            if (nome.Length < 3)
                ErrosValidacao.Add("Nome","O nome do evento deve ter mais de 3 caracteres");

            if (gratuito && valor != 0)
                ErrosValidacao.Add("Valor", "Não pode ter valor se gratuito");

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

        public Dictionary<string, string> ErrosValidacao { get; set; }

        public bool EhValido()
        {
            return !ErrosValidacao.Any();
        }
    }


    

}
