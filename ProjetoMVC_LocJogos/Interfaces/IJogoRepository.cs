using ProjetoMVC_LocJogos.Entities;
using ProjetoMVC_LocJogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC_LocJogos.Interfaces
{
    interface IJogoRepository //A classe Repository é a camada que lida com o banco de dados
    {

        //métodos abstratos
        void Inserir(Jogo jogo);
        void Alterar(Jogo jogo);
        void Excluir(Jogo jogo);

        List<Jogo> Consultar();
        Jogo ObterPorId(Guid idJogo);
        List<Jogo> ConsultarPorDatas(DateTime dataMin, DateTime dataMax);
        List<JogoGraficoModel> ConsultarTotal();


    }
}
