using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Senai.WebApi.SpMedGroup.Domains
{
    public partial class Especialidade
    {
        public Especialidade()
        {
            Medico = new HashSet<Medico>();
        }

        public int Id { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage = "Você precisa inserir um nome para a especialização")]
        public string Nome { get; set; }

        public ICollection<Medico> Medico { get; set; }
    }
}
