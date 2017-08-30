using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos.Commands
{
    public class RegistrarEventoCommand : BaseEventoCommand
    {
        public RegistrarEventoCommand(
                string nome,
                DateTime dataInicio,
                DateTime datafim,
                bool gratuito,
                decimal valor,
                bool online,
                string nomeEmpresa)

        {
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = datafim;
            Gratuito = gratuito;
            Valor = valor;
            OnLine = online;
            NomeEmpresa = nomeEmpresa;
        }
    }
}
