using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC_LocJogos.Models
{
    public class JogoRelatorioModel
    {

        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public DateTime? DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public DateTime? DataMax { get; set; }

    }
}
