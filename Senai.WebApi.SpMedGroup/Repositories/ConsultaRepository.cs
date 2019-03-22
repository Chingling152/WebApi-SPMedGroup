using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Enums;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories {
    /// <summary>
    /// Classe com metodos aplicados do IConsultaRepository (SqlClient)
    /// </summary>
    public class ConsultaRepository : IConsultaRepository {

        private const string Database = "Data Source = .\\MEUSERVIDOR; initial catalog = SENAI_SP_MEDGROUP; user id = sa; pwd = 132";

        /// <summary>
        /// Altera is valores de uma consulta no banco de dados (necessario ter ID)
        /// </summary>
        /// <param name="consulta">Consulta com os valores ja alterados (Paciente será ignorado)</param>
        public void Alterar(Consulta consulta) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "AlterarConsulta @ID , @DESCRICAO , @DATA_CONSULTA , @STATUS_CONSULTA , @MEDICO";

                SqlCommand cmd = new SqlCommand(Comando, Conexao);
                cmd.Parameters.AddWithValue("@ID", consulta.Id);
                cmd.Parameters.AddWithValue("@DECRICAO", consulta.Descricao);
                cmd.Parameters.AddWithValue("@DATA_CONSULTA", consulta.DataConsulta);
                cmd.Parameters.AddWithValue("@STATUS_CONSULTA", (int)EnSituacaoConsulta.Aguardando);
                cmd.Parameters.AddWithValue("@MEDICO", consulta.IdMedico);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cadastra uma consulta no banco de dados com a situação como Aguardado
        /// </summary>
        /// <param name="consulta">Consulta a ser cadastrada (Valor Situação ignorada)</param>
        public void Cadastrar(Consulta consulta) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "InserirConsulta @DESCRICAO , @DATA_CONSULTA , @STATUS_CONSULTA , @PACIENTE , @MEDICO";

                SqlCommand cmd = new SqlCommand(Comando, Conexao);
                cmd.Parameters.AddWithValue("@DECRICAO",consulta.Descricao);
                cmd.Parameters.AddWithValue("@DATA_CONSULTA", consulta.DataConsulta);
                cmd.Parameters.AddWithValue("@STATUS_CONSULTA", (int)EnSituacaoConsulta.Aguardando);
                cmd.Parameters.AddWithValue("@PACIENTE", consulta.IdPaciente);
                cmd.Parameters.AddWithValue("@MEDICO", consulta.IdMedico);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Mostra todas as informações de todas as consultas do banco de dados 
        /// </summary>
        /// <returns>Retorna uma lista com todas as consultas cadastradas no banco de dados (se existirem consultas)</returns>
        public List<Consulta> Listar() {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "SELECT * FROM VerConsultas";

                SqlCommand cmd = new SqlCommand(Comando,Conexao);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<Consulta> Consultas = new List<Consulta>();
                    while (leitor.Read()) {
                        Consultas.Add(
                            new Consulta() {
                                Id = Convert.ToInt32(leitor["CONSULTA"]),
                                Descricao = leitor["DESCRICAO"].ToString(),
                                DataConsulta = Convert.ToDateTime(leitor["DATA"]),
                                StatusConsulta = (EnSituacaoConsulta)Convert.ToInt32(leitor["STATUS"]),
                                // Paciente
                                IdPaciente = Convert.ToInt32(leitor["PACIENTE"]),
                                IdPacienteNavigation = new Paciente() {
                                    Id = Convert.ToInt32(leitor["PACIENTE"]),
                                    Nome = leitor["NOME_PACIENTE"].ToString(),
                                    Cpf = leitor["CPF_PACIENTE"].ToString(),
                                    Rg = leitor["RG_PACIENTE"].ToString(),
                                    Telefone = leitor["TELEFONE_PACIENTE"].ToString(),
                                    DataNascimento = Convert.ToDateTime(leitor["DATA_NASCIMENTO_PACIENTE"]),
                                    // Usuario
                                    IdUsuario = Convert.ToInt32(leitor["ID_PACIENTE"])
                                },
                                IdMedico = Convert.ToInt32(leitor["MEDICO"]),
                                IdMedicoNavigation = new Medico() {
                                    Id = Convert.ToInt32(leitor["MEDICO"]),
                                    Nome = leitor["NOME_MEDICO"].ToString(),
                                    Crm = leitor["CRM_MEDICO"].ToString(),
                                    //Especialidade do medico
                                    IdEspecialidade = Convert.ToInt32(leitor["ID_ESPECIALIDADE"]),
                                    IdEspecialidadeNavigation = new Especialidade() {
                                        Id = Convert.ToInt32(leitor["ID_ESPECIALIDADE"]),
                                        Nome = leitor["ESPECIALIDADE_MEDICO"].ToString()
                                    },
                                    //Clinica do medico
                                    IdClinica = Convert.ToInt32(leitor["ID_CLINICA"]),
                                    IdClinicaNavigation = new Clinica() {
                                        Id = Convert.ToInt32(leitor["ID_CLINICA"]),
                                        NomeFantasia = leitor["CLINICA"].ToString(),
                                        Endereco = leitor["ENDERECO"].ToString(),
                                        Numero = Convert.ToInt32(leitor["NUMERO"]),
                                        RazaoSocial = leitor["RAZAO_SOCIAL"].ToString()
                                    }
                                }
                            }    
                        );
                    }
                    return Consultas;
                }
                
            }

            throw new NullReferenceException("Não existem consultas cadastradas no banco de dados");
        }
    }
}
