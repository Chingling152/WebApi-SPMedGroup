using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories {
    /// <summary>
    /// Metodos com cadastro e listagem de clinicas
    /// </summary>
    public class ClinicaRepository : IClinicaRepository {

        private const string Database = "Data Source = .\\MEUSERVIDOR; initial catalog = SENAI_SP_MEDGROUP; user id = sa; pwd = 132";

        public void Alterar(Clinica clinica) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();

                string Comando = "AtualizarClinica @NOME_FANTASIA ,@CEP ,@ENDERECO ,@NUMERO ,@RAZAO_SOCIAL";

                SqlCommand cmd = new SqlCommand(Comando, Conexao);
                cmd.Parameters.AddWithValue("@NOME_FANTASIA", clinica.NomeFantasia);
                cmd.Parameters.AddWithValue("@CEP", clinica.Cep);
                cmd.Parameters.AddWithValue("@ENDERECO", clinica.Endereco);
                cmd.Parameters.AddWithValue("@NUMERO", clinica.Numero);
                cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", clinica.RazaoSocial);

                cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Cadastra uma list ano banco de dados
        /// </summary>
        /// <param name="clinica">Clinica a ser cadastrada</param>
        public void Cadastrar(Clinica clinica) {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();

                string Comando = "InserirClinica @NOME_FANTASIA ,@CEP ,@ENDERECO ,@NUMERO ,@RAZAO_SOCIAL";

                SqlCommand cmd = new SqlCommand(Comando,Conexao);
                cmd.Parameters.AddWithValue("@NOME_FANTASIA", clinica.NomeFantasia);
                cmd.Parameters.AddWithValue("@CEP", clinica.Cep);
                cmd.Parameters.AddWithValue("@ENDERECO", clinica.Endereco);
                cmd.Parameters.AddWithValue("@NUMERO", clinica.Numero);
                cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", clinica.RazaoSocial);

                cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Mostra todas as clinicas cadastradas no banco de dados
        /// </summary>
        /// <returns>Uma lista contendo todas as clinicas do banco de dados</returns>
        public List<Clinica> Listar() {
            using (SqlConnection Conexao = new SqlConnection(Database)) {
                Conexao.Open();
                string Comando = "SELECT * FROM VerClinicas";

                SqlCommand cmd = new SqlCommand(Comando,Conexao);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<Clinica> Clinicas = new List<Clinica>();
                    while (leitor.Read()) {
                        Clinicas.Add(
                          new Clinica() {
                              Id = Convert.ToInt32(leitor["ID"]),
                              NomeFantasia = leitor["NOME_FANTASIA"].ToString(),
                              Endereco = leitor["ENDERECO"].ToString(),
                              Cep = leitor["CEP"].ToString(),
                              Numero = Convert.ToInt32(leitor["NUMERO"]),
                              RazaoSocial = leitor["RAZAO_SOCIAL"].ToString()
                          }  
                        );
                    }
                    return Clinicas;
                }

            }

            throw new NullReferenceException("Não existe nenhuma clinica cadastrada no banco de dados");
        }
    }
}
