using Senai.WebApi.SpMedGroup.Domains;
using System.Collections.Generic;

namespace Senai.WebApi.SpMedGroup.Interfaces {
    /// <summary>
    /// Interface com metodos de cadastro e listagem de especialidades medicas
    /// </summary>
    public interface IEspecialidadeRepository {
        /// <summary>
        /// Cadastra uma especialidade medica no banco de dados
        /// </summary>
        /// <param name="especialidade">Especialidade a ser cadastrada</param>
        void Cadastrar(Especialidade especialidade);
        /// <summary>
        /// Lista todas as especialidades medicas registradas no banco de dados
        /// </summary>
        /// <returns>Uma lista com todas as especialidades medicas</returns>
        List<Especialidade> Listar();
        /// <summary>
        /// Altera o nome da especialidade
        /// </summary>
        /// <param name="especialidade">Especialidade com os valores já alterados</param>
        void Alterar(Especialidade especialidade);
    }
}
