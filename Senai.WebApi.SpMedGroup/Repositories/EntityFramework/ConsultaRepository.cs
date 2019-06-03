using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Microsoft.EntityFrameworkCore;

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
         public List<Consulta> Listar(){
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                return (
                                from Co in ctx.Consulta
                                join Pa in ctx.Paciente on Co.IdPaciente equals Pa.Id
                                join Me in ctx.Medico on Co.IdMedico equals Me.Id
                                join Cl in ctx.Clinica on Me.IdClinica equals Cl.Id
                                join Es in ctx.Especialidade on Me.IdEspecialidade equals Es.Id

                                select new Consulta() {
                                    Id = Co.Id,
                                    DataConsulta = Co.DataConsulta,
                                    Descricao = Co.Descricao,
                                    StatusConsulta = Co.StatusConsulta,
                                    IdMedico = Co.IdMedico,
                                    IdMedicoNavigation = new Medico() {
                                        Id = Me.Id,
                                        Nome = Me.Nome,
                                        Crm = Me.Crm,
                                        IdClinica = Me.IdClinica,
                                        IdClinicaNavigation = Cl,
                                        IdEspecialidade = Me.IdEspecialidade,
                                        IdEspecialidadeNavigation = Es
                                    },
                                    IdPaciente = Co.IdPaciente,
                                    IdPacienteNavigation = Pa
                                }

                            ).ToList();
            }
        }
    }
}
