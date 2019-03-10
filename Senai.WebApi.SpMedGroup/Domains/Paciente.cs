using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Senai.WebApi.SpMedGroup.Domains
{
    public partial class Paciente
    {
        public Paciente()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "O Paciente precisa ter uma conta")]
        public int IdUsuario { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Paciente precisa ter um nome")]
        [StringLength(maximumLength:200,MinimumLength =1,ErrorMessage ="O Nome inserido é muito grande")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="O Paciente precisa de um CPF")]
        [StringLength(maximumLength:11,MinimumLength =11,ErrorMessage ="O CPF precisa conter 11 caracteres")]
        public string Cpf { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Paciente precisa de um RG")]
        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "O RG precisa conter 9 caracteres")]
        public string Rg { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Paciente precisa de um Numero de telefone")]
        [StringLength(maximumLength: 11, MinimumLength = 10, ErrorMessage = "O Telefone precisa conter 11 ou 10 caracteres")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="O Valor inserido não é um telefone valido")]
        public string Telefone { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage ="O Paciente precisa de uma data de nascimento")]
        [DataType(DataType.Date,ErrorMessage ="O Valor inserido não é uma data valida")]
        public DateTime DataNascimento { get; set; }

        public Usuario IdUsuarioNavigation { get; set; }
        public ICollection<Consulta> Consulta { get; set; }
    }
}
