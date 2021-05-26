using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC_LocJogos.Models
{
    public class JogoCadastroModel//A Model no cadastro vai levar as informações da View  e levar p/ Controller
    {

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")] //MinLength minimo de caracteres aceito
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")] //MaxLength maximo de caracteres que foi definido no banco de dados(nvarchar)
        //{1} usado para não precisar escrever o nº de caracteres na msg para o usuário, pq se for preciso mudar o o nº de caracteres, muda só ErroMessage
        [Required(ErrorMessage = "Por favor, informe o nome do jogo.")] //Required faz com que o campo seja de preenchimento obrigatório
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o preço do jogo.")]
        public decimal? Preco { get; set; }

        [Required(ErrorMessage = "Por favor, informe a quantidade do jogo.")]
        public int? Quantidade { get; set; }
    }
}

