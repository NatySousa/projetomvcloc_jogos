using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC_LocJogos.Models
{
    public class JogoEdicaoModel
    {

        //campo oculto no formulário
        public Guid IdJogo { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]

        [Required(ErrorMessage = "Por favor, informe o nome do jogo.")]
        public string Nome { get; set; }

       [Required(ErrorMessage = "Por favor, informe o preço do jogo.")]
       public decimal? Preco { get; set; }

      [Required(ErrorMessage = "Por favor, informe a quantidade do jogo.")]
      public int? Quantidade { get; set; }

    }
}
