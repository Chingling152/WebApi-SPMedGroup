using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        public List<Medico> Listar() => new SpMedGroupContext().Medico.Include(i=> i.IdEspecialidadeNavigation).ToList();

        /// <summary>
        /// Lista todas as consultas de um medico selecionado pelo ID
        /// </summary>
        /// <param name="ID">ID Do medico selecionado</param>
        /// <returns></returns>
        public Medico VerConsultas(int ID) {
            Medico medico = new SpMedGroupContext().Medico.Include(x=> x.Consulta).ToList().Find(i => i.IdUsuario == ID);

            if(medico == null)
                throw new System.NullReferenceException("Não existe Medico com este ID");

            return medico;
        }
    }
}
