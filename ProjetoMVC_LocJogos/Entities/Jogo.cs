using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC_LocJogos.Entities
{
    public class Jogo
    {
        public Guid IdJogo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
