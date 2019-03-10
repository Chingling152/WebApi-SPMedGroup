using Senai.WebApi.SpMedGroup.Domains;
using System.Collections.Generic;

namespace Senai.WebApi.SpMedGroup.Interfaces {
    /// <summary>
    /// Interface com metodos de listagem e cadastro de Pacientes
    /// </summary>
    public interface IPacienteRepository {
        /// <summary>
        /// Cadastra um Paciente no banco de dados
        /// </summary>
        /// <param name="paciente">Paciente a ser cadastrado</param>
        void Cadastrar(Paciente paciente);
        /// <summary>
        /// Lista todos os usuarios cadastrados no banco de dados
        /// </summary>
        /// <returns>Uma lista com todos os Pacientes</returns>
        List<Paciente> Listar();
        /// <summary>
        /// Altera as informações do paciente
        /// </summary>
        /// <param name="paciente">Paciente com os valores ja alterados</param>
        void Alterar(Paciente paciente);
    }
}
