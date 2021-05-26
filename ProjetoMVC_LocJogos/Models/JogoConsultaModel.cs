using ProjetoMVC_LocJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC_LocJogos.Models //A Model na consulta vai levar as informações da Controller e levar p/ View
{
    public class JogoConsultaModel //essa classe vai definir o que vai ser exibido na página de consulta de Jogos
    {   /*
         * A página de consulta de produtos do sistema irá exibir
         * uma lista contendo todos os produtos obtidos do banco de dados
         */

        public List<Jogo> Jogos { get; set; }//A Model na consulta vai levar a lista de jogos da Controller e levar p/ View

    }

}