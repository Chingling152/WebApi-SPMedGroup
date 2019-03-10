using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories.EntityFramework {
    /// <summary>
    /// Classe com metodos aplicados do IConsultaRepository (Usando EntityFramework)
    /// </summary>
    public class ConsultaRepository : IConsultaRepository {

        /// <summary>
        /// Altera os valores de uma Consulta
        /// </summary>
        /// <param name="consulta">Consulta com os valores já alterados</param>
        public void Alterar(Consulta consulta) {
            using(SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Consulta.Update(consulta);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Cadastra uma consulta no ultimo registro do banco 
        /// </summary>
        /// <param name="consulta">Consulta a ser cadastrada</param>
        public void Cadastrar(Consulta consulta) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Consulta.Add(consulta);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Lista todas as consultas do banco de dados
        /// </summary>
        /// <returns>Uma lista com todas as consultas registradas</returns>
        public List<Consulta> Listar() => new SpMedGroupContext().Consulta.ToList();

        /// <summary>
        /// Lista todas as consultas com a participação de um determinado medico
        /// </summary>
        /// <param name="ID">ID do medico selecionado</param>
        /// <returns>Uma lista com todas as consultas do medico</returns>
        public List<Consulta> ListarMedico(int ID) => new SpMedGroupContext().Consulta.Where(i => i.IdMedico == ID).ToList();

        /// <summary>
        /// Lista todas as consultas com a participação de um paciente selecionado
        /// </summary>
        /// <param name="ID">ID do paciente</param>
        /// <returns>Uma lista com todas as consultas do paciente</returns>
        public List<Consulta> ListarPaciente(int ID) => new SpMedGroupContext().Consulta.Where(i => i.IdPaciente == ID).ToList();
    }
}
