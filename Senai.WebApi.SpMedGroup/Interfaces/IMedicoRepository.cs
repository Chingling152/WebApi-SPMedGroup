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
        /// Altera todas as ifnroamções de um medico no banco de dados
        /// </summary>
        /// <param name="medico">Medico com os valores ja alterados</param>
        void Alterar(Medico medico);

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
        /// <summary>
        /// Procura um medico com o CRM inserido
        /// </summary>
        /// <param name="CRM">CRM do medico que será procurado</param>
        /// <returns>Todas as informações do medico</returns>
        Medico ListarPorCrm(string CRM);
        /// <summary>
        /// Procura todos os medicos pelo nome 
        /// </summary>
        /// <param name="Nome">Nome do(s) medico(s) a ser(em) procurado(s)</param>
        /// <returns></returns>
        List<Medico> ListarPorNome(string Nome);
        /// <summary>
        /// Lista todos os medicos de uma determinada especialidade
        /// </summary>
        /// <param name="especialidade">Especialidade</param>
        /// <returns>Uma lista de medicos de uma certa especialidade</returns>
        List<Medico> ListarPorEspecialidade(Especialidade especialidade);
        /// <summary>
        /// Retorna todos os medicos de uma clinica
        /// </summary>
        /// <param name="clinica">Clinica de onde serão buscados os medicos</param>
        /// <returns>Uma lista com todos os medicos daquela clinica</returns>
        List<Medico> ListarPorClincia(Clinica clinica);
        
    }
}
