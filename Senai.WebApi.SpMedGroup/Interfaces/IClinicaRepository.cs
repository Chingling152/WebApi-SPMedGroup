using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;

namespace Senai.WebApi.SpMedGroup.Interfaces {
    /// <summary>
    /// Interface com metodos para cadastro e listagem de Clinicas
    /// </summary>
    public interface IClinicaRepository {
        /// <summary>
        /// Cadastra uma clinica no banco de dados
        /// </summary>
        /// <param name="clinica">Clinica a ser cadastrada</param>
        void Cadastrar(Clinica clinica);
        /// <summary>
        /// Lista todas as clinicas cadastradas no banco de dados
        /// </summary>
        /// <returns>Uma lista com todas as clinicas cadastradas</returns>
        List<Clinica> Listar();
        /// <summary>
        /// Altera os valores de uma clinica no banco de dados
        /// </summary>
        /// <param name="clinica">Clinica com os valores ja alterado</param>
        void Alterar(Clinica clinica);
    }
}
