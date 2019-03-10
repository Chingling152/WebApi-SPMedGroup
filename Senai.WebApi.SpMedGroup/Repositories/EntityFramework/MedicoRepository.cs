using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories.EntityFramework {
    /// <summary>
    /// Classe com os metodos de IMedicoRepository (EntityFramework)
    /// </summary>
    public class MedicoRepository : IMedicoRepository {

        /// <summary>
        /// Cadastra um medico no banco de dados
        /// </summary>
        /// <param name="medico">Medico a ser cadastrado</param>
        public void Cadastrar(Medico medico) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Medico.Add(medico);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Lista todos os medicos do banco de dados
        /// </summary>
        /// <returns>Uma lista com todos os medicos</returns>
        public List<Medico> Listar() => new SpMedGroupContext().Medico.ToList();

    }
}
