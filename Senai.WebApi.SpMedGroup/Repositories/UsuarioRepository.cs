using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories {
    /// <summary>
    /// Classe responsavel por logar e listar Usuarios (independente de serem administradores , medicos ou pacientes) e cadastrar administradores usando SqlClient
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository {

        private const string Database = "Data Source = .\\MEUSERVIDOR; initial catalog = SENAI_SP_MEDGROUP;user id = sa ; pwd = 132";

        /// <summary>
        /// Cadastra um administrador no banco de dados , qualquer outro tipo de usuario será negado
        /// </summary>
        /// <param name="usuario">Administrador a ser cadastrado (ID não será cadastrado)</param>
        public void Cadastrar(Usuario usuario) {
            if (usuario.TipoUsuario.Equals(Enums.EnTipoUsuario.Administrador)) { 
                using (SqlConnection connection = new SqlConnection(Database)) {
                    connection.Open();

                    string Comando = "InserirAdmin @EMAIL,@SENHA,@TIPO_USUARIO;"; 

                    SqlCommand cmd = new SqlCommand(Comando,connection);
                    cmd.Parameters.AddWithValue("@EMAIL",usuario.Email);
                    cmd.Parameters.AddWithValue("@SENHA", usuario.Senha);
                    cmd.Parameters.AddWithValue("@TIPO_USUARIO", usuario.TipoUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
            throw new AccessViolationException("Você não pode cadastrar qualquer outro usuario alem de um administrador neste metodo");
        }

        /// <summary>
        /// Busca uma lista de usuarios no banco de dados e retorna todos os registros
        /// </summary>
        /// <returns>Uma lista com todos os usuarios(Exceto o do tipo Administrador)</returns>
        public List<Usuario> Listar() {
            using (SqlConnection connection = new SqlConnection(Database)) {
                connection.Open();

                string Comando = "SELECT * FROM Usuario";

                SqlCommand cmd = new SqlCommand(Comando,connection);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List <Usuario> lista = new List<Usuario>();

                    while (leitor.Read()) {
                        lista.Add(
                            new Usuario() {
                                Id = Convert.ToInt32(leitor["ID"]),
                                Email = leitor["EMAIL"].ToString(),
                                Senha = leitor["SENHA"].ToString(),
                                TipoUsuario = (Enums.EnTipoUsuario)Convert.ToInt32(leitor["TIPO_USUARIO"])
                            }
                        );
                    }

                    return lista;
                }
            }
            throw new Exception("Não há Usuarios cadastrados no banco de dados");
        }

        /// <summary>
        /// Procura um usuario que tenha o ID selecionado
        /// </summary>
        /// <param name="ID">Chave primaria , ID do usuario a ser procurado</param>
        /// <returns>Retorna o usuario no ID selecionado , se ele não existir joga uma exceção</returns>
        public Usuario Listar(int ID) {
            using (SqlConnection connection = new SqlConnection(Database)) {
                connection.Open();

                string Comando = "SELECT * FROM VerMedicos WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(Comando, connection);
                cmd.Parameters.AddWithValue("@ID", ID);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    while (leitor.Read()) {
                        return new Usuario() {
                            Id = Convert.ToInt32(leitor["ID"]),
                            Email = leitor["EMAIL"].ToString(),
                            Senha = leitor["SENHA"].ToString(),
                            TipoUsuario = (Enums.EnTipoUsuario)Convert.ToInt32(leitor["TIPO_USUARIO"])
                        };
                    }
                }
            }
            throw new NullReferenceException("Não existe usuario com o ID selecionado"); 
        }

        /// <summary>
        /// Procura um usuario no banco de dados que tenha a combinação de Email e Senha
        /// </summary>
        /// <param name="Email">Email do Usuario a ser procurado</param>
        /// <param name="Senha">Senha do Usuario a ser procurado</param>
        /// <returns>Um usuario com todas as informações retorna null</returns>
        public Usuario Logar(string Email, string Senha) {
            using (SqlConnection connection = new SqlConnection(Database)) {
                connection.Open();

                string Comando = "SELECT * FROM Usuario WHERE EMAIL = @EMAIL AND SENHA = @SENHA";

                SqlCommand cmd = new SqlCommand(Comando, connection);
                cmd.Parameters.AddWithValue("@EMAIL",Email);
                cmd.Parameters.AddWithValue("@SENHA", Senha);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    while (leitor.Read()) {
                        return new Usuario() {
                            Email = leitor["EMAIL"].ToString(),
                            Senha = leitor["SENHA"].ToString(),
                            TipoUsuario = (Enums.EnTipoUsuario)Convert.ToInt32(leitor["TIPO_USUARIO"])
                        };
                    }
                }
            }
            return null;
        }
    }
}
