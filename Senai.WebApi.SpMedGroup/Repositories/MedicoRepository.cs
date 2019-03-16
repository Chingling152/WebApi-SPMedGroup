using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories {
    /// <summary>
    /// Classe com os metodos de IMedicoRepository
    /// </summary>
    public class MedicoRepository : IMedicoRepository {

        private const string Database = "Data Source = .\\MEUSERVIDOR; initial catalog = SENAI_SP_MEDGROUP;user id = sa ; pwd = 132";

        /// <summary>
        /// Cadastra um Usuario e um Medico (que referenciara ao Usuario cadastrado) ao mesmo tempo
        /// </summary>
        /// <param name="medico">Medico com todas as suas informações preechidas</param>
        public void Cadastrar(Medico medico) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "InserirMedico @EMAIL , @SENHA , @NOME, @CRM , @ID_CLINICA, @ID_ESPECIALIDADE;";

                SqlCommand cmd = new SqlCommand(Comando,Conexao);
                cmd.Parameters.AddWithValue("@EMAIL",medico.IdUsuarioNavigation.Email);
                cmd.Parameters.AddWithValue("@SENHA",medico.IdUsuarioNavigation.Senha);
                cmd.Parameters.AddWithValue("@NOME",medico.Nome);
                cmd.Parameters.AddWithValue("@CRM",medico.Crm);
                cmd.Parameters.AddWithValue("@ID_CLINICA",medico.IdClinica);
                cmd.Parameters.AddWithValue("@ID_ESPECIALIDADE",medico.IdEspecialidade);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Procura todos os medicos do banco de dados com todas as suas informações 
        /// </summary>
        /// <returns>Retorna todas as informações de todos os medicos cadastrados no banco de dados</returns>
        public List<Medico> Listar() {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "SELECT * FROM VerMedicos";
                SqlCommand cmd = new SqlCommand(Comando, Conexao);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<Medico> Retorno = new List<Medico>();

                    while (leitor.Read()) {
                        Retorno.Add(
                            new Medico() {
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
                                },
                                IdUsuario = Convert.ToInt32(leitor["ID_MEDICO"]),
                                IdUsuarioNavigation = new Usuario() {
                                    Id = Convert.ToInt32(leitor["ID_MEDICO"]),
                                    Email = leitor["EMAIL_MEDICO"].ToString(),
                                }
                            }
                        );
                    }

                    return Retorno;
                }
            }
            throw new NullReferenceException("Não existe Medicos registrados no banco de dados");
        }

        /// <summary>
        /// Procura um medico no ID selecionado e todas as suas informações
        /// </summary>
        /// <param name="ID">ID do medico a ser procurados</param>
        /// <returns>Um medico com todos os seus dados (Clinica e Especialidade)</returns>
        public Medico Listar(int ID) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "SELECT * FROM VerMedicos WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(Comando, Conexao);

                cmd.Parameters.AddWithValue("@ID",ID);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    while (leitor.Read()) {
                        return new Medico() {
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
                            },
                            IdUsuario = Convert.ToInt32(leitor["ID_MEDICO"]),
                            IdUsuarioNavigation = new Usuario() {
                                Id = Convert.ToInt32(leitor["ID_MEDICO"]),
                                Email = leitor["EMAIL_MEDICO"].ToString(),
                            }
                        };
                    }
                }
            }
            throw new NullReferenceException("Não existe Medico registrado com este ID no banco de dados");
        }

        /// <summary>
        /// Mostra todas as consultas de um medico no banco de dados
        /// </summary>
        /// <param name="IDMedico">ID do medico</param>
        /// <returns>Retorna todas as consultas do medico , se ele não tiver nenhuma retorna NullReferenceException</returns>
        public Medico VerConsultas(int IDMedico) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "SELECT * FROM VerConsultas WHERE MEDICO = @MEDICO";

                SqlCommand cmd = new SqlCommand(Comando, Conexao);
                cmd.Parameters.AddWithValue("@MEDICO",IDMedico);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    // Não precisa de verificação pois se o metodo não encontrar um medico ele automaticamente jogará uma exceção
                    Medico medico = Listar(IDMedico);
                    while (leitor.Read()) {
                        medico.Consulta.Add(
                            new Consulta() {
                                Id = Convert.ToInt32(leitor["CONSULTA"]),
                                Descricao = leitor["DESCRICAO"].ToString(),
                                DataConsulta = Convert.ToDateTime(leitor["DATA"]),
                                StatusConsulta = (Enums.EnSituacaoConsulta)Convert.ToInt32(leitor["STATUS_CONSULTA"]),
                                //Paciente
                                IdPaciente = Convert.ToInt32(leitor["PACIENTE"]),
                                IdPacienteNavigation = new Paciente() {
                                    //Paciente
                                    Id = Convert.ToInt32(leitor["PACIENTE"]),
                                    Nome = leitor["NOME_PACIENTE"].ToString(),
                                    Cpf = leitor["CPF_PACIENTE"].ToString(),
                                    Rg = leitor["RG_PACIENTE"].ToString(),
                                    Telefone = leitor["TELEFONE_PACIENTE"].ToString(),
                                    DataNascimento = Convert.ToDateTime(leitor["DATA_NASCIMENTO_PACIENTE"]),
                                    // Não é necessario passar o Email ou Senha do Paciente
                                    IdUsuario = Convert.ToInt32(leitor["ID_PACIENTE"]),
                                },
                                // Só o ID do Medico ja basta
                                IdMedico = Convert.ToInt32(leitor["MEDICO"])
                            }
                        );
                    }
                    return medico;
                }
            }
            throw new NullReferenceException("Você não tem nenhuma consulta");
        }
    }
}
