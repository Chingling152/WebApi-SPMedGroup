using Senai.WebApi.SpMedGroup.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Senai.WebApi.SpMedGroup.Domains
{
    public partial class Consulta
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false , ErrorMessage ="Não é possivel agendar uma consulta sem um medico")]
        public int IdMedico { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Não é possivel agendar uma consulta sem um paciente")]
        public int IdPaciente { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "A Consulta precisa ter uma data")]
        [DataType(DataType.DateTime,ErrorMessage = "O Valor inserido não é uma data valida")]
        public DateTime DataConsulta { get; set; }

        [StringLength(maximumLength: 1000,ErrorMessage ="A descrição é muito grande")]
        public string Descricao { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A Consulta precisa ter uma situação")]
        public EnSituacaoConsulta StatusConsulta { get; set; }

        public Medico IdMedicoNavigation { get; set; }
        public Paciente IdPacienteNavigation { get; set; }
    }
}
