using System;
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
        /// Lista todas as informações de todas as consultas do banco de dados
        /// </summary>
        /// <returns>Uma lista com todas as consultas registradas</returns>
        public List<Consulta> Listar() {
            using(SpMedGroupContext ctx = new SpMedGroupContext()){
                if(ctx.Consulta.Count() > 0) {
                    return (
                        from Co in ctx.Consulta                                         // Consulta
                        join Pa in ctx.Paciente     on Co.IdPaciente equals Pa.Id       // Paciente
                        join Me in ctx.Medico       on Co.IdMedico equals Me.Id         // Medico
                        join Cl in ctx.Clinica      on Me.IdClinica equals Cl.Id        // Clinica
                        join Es in ctx.Especialidade on Me.IdEspecialidade equals Es.Id // Especialidade

                        select new Consulta() {
                            Id = Co.Id,
                            Descricao = Co.Descricao,
                            StatusConsulta = Co.StatusConsulta,
                            DataConsulta = Co.DataConsulta,
                            IdMedico = Co.IdMedico,
                            IdPaciente = Co.IdPaciente,
                            IdPacienteNavigation = new Paciente() {
                                Id = Pa.Id,
                                Nome = Pa.Nome,
                                Cpf = Pa.Cpf,
                                DataNascimento = Pa.DataNascimento,
                                Telefone = Pa.Telefone
                            },
                            IdMedicoNavigation = new Medico() {
                                Id = Me.Id,
                                Nome = Me.Nome,
                                Crm = Me.Crm,
                                IdEspecialidade = Es.Id,
                                IdClinica = Cl.Id,
                                IdEspecialidadeNavigation = new Especialidade() {
                                    Id = Es.Id,
                                    Nome = Es.Nome
                                },
                                IdClinicaNavigation = new Clinica() {
                                    Id = Cl.Id,
                                    NomeFantasia = Cl.NomeFantasia,
                                    Cep = Cl.NomeFantasia,
                                    Endereco = Cl.Endereco,
                                    Numero = Cl.Numero
                                }
                            }
                            
                        }
                    ).ToList();
                }
            }
            throw new Exception("Não existe nenhuma consulta no banco de dados");
        }

        public List<Consulta> Listar(DateTime dataInicial, DateTime dataFinal) {
            throw new NotImplementedException();
        }
    }
}
