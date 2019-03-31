using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Senai.WebApi.SpMedGroup.Domains
{
    public partial class Clinica
    {
        public Clinica()
        {
            Medico = new HashSet<Medico>();
        }

        public int Id { get; set; }
        
        [Required(AllowEmptyStrings = false , ErrorMessage = "A Clinica precisa ter um nome")]
        [StringLength(maximumLength: 200, MinimumLength = 1,ErrorMessage = "O Nome da empresa é muito grande")]
        public string NomeFantasia { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A Clinica precisa ter um endereço")]
        [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "O endereço inserido é muito grande")]
        public string Endereco { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Insira o numero da Clinica")]
        public int Numero { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "O CEP deve conter apenas numeros")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O CEP é obrigatorio")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "O CEP precisa ter exatos 8 caracteres")]
        public string Cep { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A Clinica precisa de uma Razão Social")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "A Razão Social inserida é muito grande")]
        public string RazaoSocial { get; set; }

        public ICollection<Medico> Medico { get; set; }
    }
}
