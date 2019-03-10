using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Senai.WebApi.SpMedGroup.Domains
{
    public partial class Medico
    {
        public Medico()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int Id { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessage ="O medico precisa ter uma conta")]
        public int IdUsuario { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage ="O Nome do medico é obrigatorio")]
        [StringLength(maximumLength:200,MinimumLength =1,ErrorMessage ="O nome inserido é muito grande")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O medico precisa ter um CRM")]
        [StringLength(maximumLength: 7, MinimumLength = 7, ErrorMessage = "O CRM precisa ter 7 caracteres")]
        public string Crm { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "O Medico precisa trabalhar em uma clinica")]
        public int IdClinica { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Medico precisa ter uma especialidade")]
        public int IdEspecialidade { get; set; }

        public Clinica IdClinicaNavigation { get; set; }
        public Especialidade IdEspecialidadeNavigation { get; set; }
        public Usuario IdUsuarioNavigation { get; set; }
        public ICollection<Consulta> Consulta { get; set; }
    }
}
