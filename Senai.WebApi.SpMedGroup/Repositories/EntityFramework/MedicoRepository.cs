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
        /// Altera todas as informações de um medico
        /// </summary>
        /// <param name="medico">Medico com os valores ja alterados</param>
        public void Alterar(Medico medico) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Medico.Update(medico);
                ctx.SaveChanges();
            }
        }

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
        /// Procura um medico no ID selecionado no banco de dados
        /// </summary>
        /// <param name="ID">ID do medico</param>
        /// <returns>Retorna um Medico e sua especialidade , ou uma excessão caso ele não exista</returns>
        public Medico Listar(int ID) {
            Medico medico = new SpMedGroupContext().Medico.Include(i => i.IdEspecialidadeNavigation).First(i => i.Id == ID);

            if (medico == null)
                    throw new System.NullReferenceException($"Não existe medico no ID {ID}");

            return medico;
        }

        /// <summary>
        /// Lista todas as consultas de um medico selecionado pelo ID
        /// </summary>
        /// <param name="ID">ID Do medico selecionado</param>
        /// <returns></returns>
        public Medico VerConsultas(int ID) {
        using (SpMedGroupContext ctx = new SpMedGroupContext()) {
            Medico medico = (
                    from Me in ctx.Medico
                    join Us in ctx.Usuario on Me.IdUsuario equals Us.Id
                    join Cl in ctx.Clinica on Me.IdClinica equals Cl.Id
                    join Es in ctx.Especialidade on Me.IdEspecialidade equals Es.Id

                    select new Medico() {
                        Id = Me.Id,
                        Nome = Me.Nome,
                        Crm = Me.Crm,
                        IdClinica = Me.IdClinica,
                        IdClinicaNavigation = Cl,
                        IdEspecialidade = Me.IdEspecialidade,
                        IdEspecialidadeNavigation = Es,
                        IdUsuario = Me.IdUsuario,
                        IdUsuarioNavigation = Us,
                        Consulta = (
                                from Co in ctx.Consulta
                                join Pa in ctx.Paciente on Co.Id equals Pa.Id
                                where Co.IdPaciente == Pa.Id

                                select new Consulta() {
                                    Id = Co.Id,
                                    DataConsulta = Co.DataConsulta,
                                    Descricao = Co.Descricao,
                                    StatusConsulta = Co.StatusConsulta,
                                    IdMedico = Co.IdMedico,
                                    IdPaciente = Co.IdPaciente,
                                    IdPacienteNavigation = Pa
                                }

                            ).ToList()
                    }
                    ).FirstOrDefault(i => i.IdUsuario == ID);
            
                if (medico == null)
                    throw new System.NullReferenceException("Não existe Medico com este ID");

                return medico;
            }
        }
    }
}
