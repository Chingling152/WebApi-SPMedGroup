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
        /// Mostra todas as informações do medico e de todas as suas consultas
        /// </summary>
        /// <param name="ID">Id do medico</param>
        /// <returns>Retorna um medico e suas consultas</returns>
        Medico VerConsultas(int ID);
    }
}
