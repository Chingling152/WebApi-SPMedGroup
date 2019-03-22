using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories {
    /// <summary>
    /// Classe com metodos do IEspecialidadeRepository
    /// </summary>
    public class EspecialidadeRepository : IEspecialidadeRepository {

        private const string Database = "Data Source = .\\MEUSERVIDOR; initial catalog = SENAI_SP_MEDGROUP;user id = sa ; pwd = 132";

        /// <summary>
        /// Altera os valores de uma especialidade no banco de dados 
        /// </summary>
        /// <param name="especialidade">Especialidade a ser alterada</param>
        public void Alterar(Especialidade especialidade) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "AtualizarEspecialidade @ID , @NOME";

                SqlCommand cmd = new SqlCommand(Comando, Conexao);
                cmd.Parameters.AddWithValue("@ID", especialidade.Id);
                cmd.Parameters.AddWithValue("@NOME", especialidade.Nome);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cadastra uma especialidade medica no banco de dados
        /// </summary>
        /// <param name="especialidade">Especialidade a ser cadastrada</param>
        public void Cadastrar(Especialidade especialidade) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "InserirEspecialidade @NOME";

                SqlCommand cmd = new SqlCommand(Comando, Conexao);
                cmd.Parameters.AddWithValue("@NOME",especialidade.Nome);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Mostra todas as especialidades medicas cadastradas no banco de dados
        /// </summary>
        /// <returns>Uma lista com todas especialidades medicas , se não houver nenhuma , retorna uma NullReferenceException</returns>
        public List<Especialidade> Listar() {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "SELECT * FROM Especidalidade";
                SqlCommand cmd = new SqlCommand(Comando,Conexao);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<Especialidade> especialidades = new List<Especialidade>();
                    while (leitor.Read()) {
                        especialidades.Add(
                            new Especialidade() {
                                Id = Convert.ToInt32(leitor["ID"]),
                                Nome = leitor["NOME"].ToString()
                            }
                        );
                    }
                    return especialidades;
                }

            }
            throw new NullReferenceException("Não existe Especialidades no banco de dados");
        }
    }
}
