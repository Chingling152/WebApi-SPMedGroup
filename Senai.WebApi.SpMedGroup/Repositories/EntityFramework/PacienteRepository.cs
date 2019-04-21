using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Senai.WebApi.SpMedGroup.Repositories.EntityFramework {
    /// <summary>
    /// Classe com os metodos de IPacienteRepository (EntityFramework) responsavel por manusear dados do Pacientes
    /// </summary>
    public class PacienteRepository : IPacienteRepository {

        /// <summary>
        /// Muda as informações do paciente
        /// </summary>
        /// <param name="paciente">Paciente com os valores já alterados</param>
        public void Alterar(Paciente paciente) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Paciente.Update(paciente);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Cadastra um paciente no banco de dados
        /// </summary>
        /// <param name="paciente">Paciente a ser cadastrado</param>
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
        public List<Paciente> Listar() => new SpMedGroupContext().Paciente.Include(i => i.IdUsuarioNavigation).ToList();

        /// <summary>
        /// Retorna todas as informações de um paciente selecionado pelo ID
        /// </summary>
        /// <param name="ID">ID do paciente</param>
        /// <returns>Retorna um paciente, ou null caso ele não exista</returns>
        public Paciente Listar(int ID) {
            Paciente paciente = new SpMedGroupContext().Paciente.Include(i => i.IdUsuarioNavigation).First(x => x.Id == ID);

            if(paciente == null)
                throw new System.NullReferenceException($"Não existe paciente no ID {ID}");

            return paciente;
        }

        /// <summary>
        /// Lista todas as consulta de um paciente selecionado pelo ID
        /// </summary>
        /// <param name="ID">ID do paciente selecionado</param>
        /// <returns>Todas as consultas do paciente, ou uma exceção caso o Paciente não exista</returns>
        public Paciente VerConsultas(int ID) {
            using(SpMedGroupContext ctx = new SpMedGroupContext()) {
                Paciente paciente = (
                        from Pa in ctx.Paciente 
                        join Us in ctx.Usuario on Pa.IdUsuario equals Us.Id

                        select new Paciente() {
                                Id = Pa.Id,
                                IdUsuario = Pa.IdUsuario,
                                IdUsuarioNavigation = Us,
                                Nome = Pa.Nome,
                                Cpf = Pa.Cpf,
                                Rg = Pa.Rg,
                                DataNascimento = Pa.DataNascimento,
                                Telefone = Pa.Telefone,
                                Consulta = (

                                    from Co in ctx.Consulta
                                    join Me in ctx.Medico on Co.IdMedico equals Me.Id
                                    join Cl in ctx.Clinica on Me.IdClinica equals Cl.Id        // Clinica
                                    join Es in ctx.Especialidade on Me.IdEspecialidade equals Es.Id
                                    where Co.IdPaciente == Pa.Id

                                    select new Consulta() {
                                        Id = Co.Id,
                                        DataConsulta = Co.DataConsulta,
                                        Descricao = Co.Descricao,
                                        StatusConsulta = Co.StatusConsulta,
                                        IdMedico = Co.IdMedico,
                                        IdPaciente = Co.IdPaciente,
                                        IdPacienteNavigation = Pa,
                                        IdMedicoNavigation = new Medico(){
                                            Id = Me.Id,
                                            Nome = Me.Nome,
                                            Crm = Me.Crm,
                                            IdClinica = Me.IdClinica,
                                            IdClinicaNavigation = Cl,
                                            IdEspecialidade = Me.IdEspecialidade,
                                            IdEspecialidadeNavigation = Es
                                        }
                                    } 
                                    
                                ).ToList()
                            }
                        ).First(i => i.IdUsuario == ID);

                if (paciente == null)
                    throw new System.NullReferenceException("Não existe Paciente com este ID");

                return paciente;
            }
        }
    }
}
