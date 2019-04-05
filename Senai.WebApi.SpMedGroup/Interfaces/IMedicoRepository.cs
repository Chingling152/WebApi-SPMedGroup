using Senai.WebApi.SpMedGroup.Domains;
using System.Collections.Generic;

namespace Senai.WebApi.SpMedGroup.Interfaces {
    /// <summary>
    /// Interface com metodos de listagem e cadastro de medicos
    /// </summary>
    public interface IMedicoRepository {

        /// <summary>
        /// Cadastra um medico no banco de dados
        /// </summary>
        /// <param name="medico">Medico a ser cadastrado</param>
        void Cadastrar(Medico medico);

        /// <summary>
        /// Lista todos todos Medicos cadastrados no banco de dados
        /// </summary>
        /// <returns>Uma lista com todos os medicos</returns>
        List<Medico> Listar();

        /// <summary>
        /// Lista um Medico no ID selecionado
        /// </summary>
        /// <param name="ID">ID do Medico que será retornado</param>
        /// <returns>Um Medico com todas as informações completas</returns>
        Medico Listar(int ID);

        /// <summary>
        /// Mostra todas as informações do medico e de todas as suas consultas
        /// </summary>
        /// <param name="ID">Id do medico</param>
        /// <returns>Retorna um medico e suas consultas</returns>
        Medico VerConsultas(int ID);
        void Alterar(Medico medico);
        /*
/// <summary>
/// Procura um medico com o CRM
/// </summary>
/// <param name="CRM"></param>
/// <returns></returns>
Medico Procurar(string CRM);
*/
    }
}
