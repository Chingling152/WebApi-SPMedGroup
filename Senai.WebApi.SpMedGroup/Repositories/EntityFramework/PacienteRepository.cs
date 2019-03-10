using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories.EntityFramework {
    /// <summary>
    /// Classe com os metodos de IPacienteRepository (EntityFramework) responsavel por manusear dados do Pacientes
    /// </summary>
    public class PacienteRepository : IPacienteRepository {

        public void Alterar(Paciente paciente) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Cadastra um paciente no banco de dados
        /// </summary>
        /// <param name="medico">Paciente a ser cadastrado</param>
        public void Cadastrar(Paciente paciente) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Paciente.Add(paciente);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Lista todos os pacientes do banco de dados
        /// </summary>
        /// <returns>Uma lista com todos os pacientes</returns>
        public List<Paciente> Listar() => new SpMedGroupContext().Paciente.ToList();
    }
}
