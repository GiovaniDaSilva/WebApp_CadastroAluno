using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Domain
{
    public class AlunoDTO
    {        
        public int id { get; set; }
        [Required(ErrorMessage ="O Nome é de preenchimento obrigatório.")]
        [StringLength(50, ErrorMessage ="O Nome deve ter no minimo 2 caracteres e no maximo 50.", MinimumLength =2)]

        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        
        [RegularExpression(@"[0-9]{4}\-[0-9]{2}", ErrorMessage ="A data esta fora do formato YYYY-MM")]
        public string data { get; set; }
        
        [Required(ErrorMessage = "O RA é de preenchimento obrigatório.")]
        [Range(1,9099,ErrorMessage ="O intervalo para cadastro de RA deve estar entre 1 e 9099.")]
        public int? ra { get; set; }

    }
}
