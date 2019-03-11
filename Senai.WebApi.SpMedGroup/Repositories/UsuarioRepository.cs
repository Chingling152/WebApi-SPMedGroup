using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories {
    /// <summary>
    /// Classe responsavel por cadastrar e listar Usuarios (independente de serm administradores , medicos ou pacientes) usando SqlClient
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository {

        private const string Database = "Data Source = .\\MEUSERVIDOR; initial catalog = SENAI_SP_MEDGROUP;user id = sa ; pwd = 132";

        /// <summary>
        /// Cadastra um usuario no banco de dados
        /// </summary>
        /// <param name="usuario">Usuario a ser cadastrado (ID não será cadastrado)</param>
        public void Cadastrar(Usuario usuario) {
            using (SqlConnection connection = new SqlConnection(Database)) {
                connection.Open();

                string Comando = "INSERT INTO Usuario(EMAIL,SENHA,TIPO_USUARIO) VALUES(@EMAIL,@SENHA,@TIPO_USUARIO);"; 

                SqlCommand cmd = new SqlCommand(Comando,connection);
                cmd.Parameters.AddWithValue("@EMAIL",usuario.Email);
                cmd.Parameters.AddWithValue("@SENHA", usuario.Senha);
                cmd.Parameters.AddWithValue("@TIPO_USUARIO", usuario.TipoUsuario);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Busca uma lista de usuarios no banco de dados e retorna todos os registros
        /// </summary>
        /// <returns>Uma lista com todos os usuarios(Exceto o do tipo Administrador)</returns>
        public List<Usuario> Listar() {
            using (SqlConnection connection = new SqlConnection(Database)) {
                connection.Open();

                string Comando = "SELECT * FROM Usuario WHERE TIPO_USUARIO != 100";

                SqlCommand cmd = new SqlCommand(Comando,connection);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List <Usuario> lista = new List<Usuario>();

                    while (leitor.Read()) {
                        lista.Add(
                            new Usuario() {
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

        public Usuario Listar(int ID) {
            throw new NotImplementedException();
        }

        public Usuario Logar(string Email, string Senha) {
            using (SqlConnection connection = new SqlConnection(Database)) {
                connection.Open();

                string Comando = "SELECT * FROM VerUsuarios WHERE EMAIL = @EMAIL AND SENHA = @SENHA";

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
