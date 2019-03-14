using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Senai.WebApi.SpMedGroup.Repositories.EntityFramework {
    /// <summary>
    /// Metodos com cadastro e listagem de clinicas (EntityFramework)
    /// </summary>
    public class ClinicaRepository : IClinicaRepository {

        /// <summary>
        /// Cadastra uma clinica no banco de dados
        /// </summary>
        /// <param name="clinica">Clinica a ser cadastrada</param>
        public void Cadastrar(Clinica clinica) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Clinica.Add(clinica);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Lista todas as clinicas do banco de dados
        /// </summary>
        /// <returns>Uma lista com todas as informações de todas as clinicas</returns>
        public List<Clinica> Listar() => new SpMedGroupContext().Clinica.Include(i => i.Medico).ToList();
    }
}
