using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories.EntityFramework {
    /// <summary>
    /// Classe com metodos do IEspecialidadeRepository (EntityFramework)
    /// </summary>
    public class EspecialidadeRepository : IEspecialidadeRepository {
        /// <summary>
        /// Atualiza o nome de uma especialidade
        /// </summary>
        /// <param name="especialidade">Especialidade com os valores ja alterados</param>
        public void Alterar(Especialidade especialidade) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Especialidade.Update(especialidade);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Cadastra uma especialidade medica no banco de dados
        /// </summary>
        /// <param name="especialidade">Especialidade a ser cadastrada</param>
        public void Cadastrar(Especialidade especialidade) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Especialidade.Add(especialidade);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Mostra todas as especialidades medicas cadastradas no banco de dados
        /// </summary>
        /// <returns>Uma lista com todas as especialidades medicas</returns>
        public List<Especialidade> Listar() => new SpMedGroupContext().Especialidade.ToList();
    }
}
