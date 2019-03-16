using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories {
    /// <summary>
    /// Classe com os metodos de IPacienteRepository , responsavel por manusear dados do Pacientes usando SqlClient
    /// </summary>
    public class PacienteRepository : IPacienteRepository {

        private const string Database = "Data Source = .\\MEUSERVIDOR; initial catalog = SENAI_SP_MEDGROUP;user id = sa ; pwd = 132";

        /// <summary>
        /// Altera todos os valores de um paciente (não pode mudar o usuario atrelado a ele)
        /// </summary>
        /// <param name="paciente">Paciente com os valores já alterados</param>
        public void Alterar(Paciente paciente) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();

                string Comando = "AtualizarPaciente @ID , @EMAIL , @SENHA , @NOME , @CPF , @RG , @TELEFONE , @DATA_NASCIMENTO";

                SqlCommand cmd = new SqlCommand(Comando, Conexao);
                cmd.Parameters.AddWithValue("@ID",paciente.Id);
                cmd.Parameters.AddWithValue("@EMAIL", paciente.IdUsuarioNavigation.Email);
                cmd.Parameters.AddWithValue("@SENHA", paciente.IdUsuarioNavigation.Senha);
                cmd.Parameters.AddWithValue("@NOME", paciente.Nome);
                cmd.Parameters.AddWithValue("@CPF", paciente.Cpf);
                cmd.Parameters.AddWithValue("@RG", paciente.Rg);
                cmd.Parameters.AddWithValue("@TELEFONE", paciente.Telefone);
                cmd.Parameters.AddWithValue("@DATA_NASCIMENTO", paciente.DataNascimento);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cria um paciente e um Usuario ao mesmo tempo (evite usar UsuarioRepository.Cadastrar())
        /// </summary>
        /// <param name="paciente">Paciente/Usuario a ser cadastrado</param>
        public void Cadastrar(Paciente paciente) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();

                string Comando = "InserirPaciente @EMAIL, @SENHA , @NOME , @CPF , @RG , @TELEFONE , @DATA_NASCIMENTO";

                SqlCommand cmd = new SqlCommand(Comando,Conexao);
                cmd.Parameters.AddWithValue("@EMAIL",paciente.IdUsuarioNavigation.Email);
                cmd.Parameters.AddWithValue("@SENHA",paciente.IdUsuarioNavigation.Senha);
                cmd.Parameters.AddWithValue("@NOME",paciente.Nome);
                cmd.Parameters.AddWithValue("@CPF",paciente.Cpf);
                cmd.Parameters.AddWithValue("@RG",paciente.Rg);
                cmd.Parameters.AddWithValue("@TELEFONE",paciente.Telefone);
                cmd.Parameters.AddWithValue("@DATA_NASCIMENTO",paciente.DataNascimento);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Lista todas as informações de todos os Paciente no banco de dados
        /// </summary>
        /// <returns>Uma lista com todas as informações dos Pacientes</returns>
        public List<Paciente> Listar() {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "SELECT * FROM VerPacientes";
                SqlCommand cmd = new SqlCommand(Comando,Conexao);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<Paciente> Retorno = new List<Paciente>();

                    while (leitor.Read()) {
                        Retorno.Add(
                            new Paciente() {
                                //Paciente
                                Id = Convert.ToInt32(leitor["PACIENTE"]),
                                Nome = leitor["NOME_PACIENTE"].ToString(),
                                Cpf = leitor["CPF_PACIENTE"].ToString(),
                                Rg = leitor["RG_PACIENTE"].ToString(),
                                Telefone = leitor["TELEFONE_PACIENTE"].ToString(),
                                DataNascimento = Convert.ToDateTime(leitor["DATA_NASCIMENTO_PACIENTE"]),
                                // Usuario
                                IdUsuario = Convert.ToInt32(leitor["ID_PACIENTE"]),
                                IdUsuarioNavigation = new Usuario() {
                                    Id = Convert.ToInt32(leitor["ID_PACIENTE"]),
                                    Email = leitor["EMAIL_PACIENTE"].ToString(),
                                    Senha = leitor["SENHA_PACIENTE"].ToString(),
                                    TipoUsuario = (Enums.EnTipoUsuario)Convert.ToUInt32(leitor["TIPO_USUARIO_PACIENTE"])
                                }
                            }
                        );
                    }

                    return Retorno;
                }
            }

            throw new Exception("Não existe pacientes registrados no banco de dados");
        }

        /// <summary>
        /// Procura um paciente no ID selecionado. Retorna uma exceção se o mesmo não existir
        /// </summary>
        /// <param name="ID">ID Do paciente a ser procurado</param>
        /// <returns>Retorna todas as informações do paciente</returns>
        public Paciente Listar(int ID) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "SELECT * FROM VerPacientes WHERE PACIENTE = @PACIENTE";

                SqlCommand cmd = new SqlCommand(Comando, Conexao);
                cmd.Parameters.AddWithValue("@PACIENTE", ID);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    while (leitor.Read()) {
                        return new Paciente() {
                            //Paciente
                            Id = Convert.ToInt32(leitor["PACIENTE"]),
                            Nome = leitor["NOME_PACIENTE"].ToString(),
                            Cpf = leitor["CPF_PACIENTE"].ToString(),
                            Rg = leitor["RG_PACIENTE"].ToString(),
                            Telefone = leitor["TELEFONE_PACIENTE"].ToString(),
                            DataNascimento = Convert.ToDateTime(leitor["DATA_NASCIMENTO_PACIENTE"]),
                            // Usuario
                            IdUsuario = Convert.ToInt32(leitor["ID_PACIENTE"]),
                            IdUsuarioNavigation = new Usuario() {
                                Id = Convert.ToInt32(leitor["ID_PACIENTE"]),
                                Email = leitor["EMAIL_PACIENTE"].ToString(),
                                Senha = leitor["SENHA_PACIENTE"].ToString(),
                                TipoUsuario = (Enums.EnTipoUsuario)Convert.ToUInt32(leitor["TIPO_USUARIO_PACIENTE"])
                            }
                        };
                    }
                }
            }

            throw new NullReferenceException($"Não existe Paciente com o ID {ID}");
        }

        /// <summary>
        /// Mostra todas as consultas do Paciente selecionado
        /// </summary>
        /// <param name="IDPaciente">Id do paciente</param>
        /// <returns>Retorna todas as informações de todas as consultas do usuario</returns>
        public Paciente VerConsultas(int IDPaciente) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "SELECT * FROM VerConsultas WHERE PACIENTE = @PACIENTE";

                SqlCommand cmd = new SqlCommand(Comando, Conexao);
                cmd.Parameters.AddWithValue("@PACIENTE", IDPaciente);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    // A lista de consulta será adcionada a este Paciente
                    Paciente paciente = Listar(IDPaciente);
                    while (leitor.Read()) {
                        paciente.Consulta.Add(
                            new Consulta() {
                                Id = Convert.ToInt32(leitor["CONSULTA"]),
                                Descricao = leitor["DESCRICAO"].ToString(),
                                DataConsulta = Convert.ToDateTime(leitor["DATA"]),
                                StatusConsulta = (Enums.EnSituacaoConsulta)Convert.ToInt32(leitor["STATUS_CONSULTA"]),
                                //Será inutil trazer todas as informações do paciente denovo
                                IdPaciente = Convert.ToInt32(leitor["PACIENTE"]),
                                //Medico
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

                    return paciente;
                }

            }

            throw new Exception("Você não tem nenhuma consulta");
        }
    }
}
