using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Senai.WebApi.SpMedGroup.Enums;

namespace Senai.WebApi.SpMedGroup.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            Medico = new HashSet<Medico>();
            Paciente = new HashSet<Paciente>();
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="O Email é obrigatorio")]
        [StringLength(maximumLength:200,MinimumLength =5,ErrorMessage ="O Email inserido é muito pequeno ou muito grande")]
        [DataType(DataType.EmailAddress,ErrorMessage ="O Valor inserido não é um email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A senha é obrigatoria")]
        [StringLength(maximumLength:200,MinimumLength = 8,ErrorMessage ="A senha inserida é muito pequena ou muito grande")]
        [DataType(DataType.Password, ErrorMessage = "O Valor inserido não é um senha valida")]
        public string Senha { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Usuario precisa ter um nivel de privilegio")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EnTipoUsuario TipoUsuario { get; set; }

        public ICollection<Medico> Medico { get; set; }
        public ICollection<Paciente> Paciente { get; set; }
    }
}
