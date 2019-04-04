using Senai.WebApi.SpMedGroup.Domains;
using System;
using System.Collections.Generic;

namespace Senai.WebApi.SpMedGroup.Interfaces {
    /// <summary>
    /// Interface com metodos de listagem , cadastro e alteração de Consultas
    /// </summary>
    public interface IConsultaRepository {

        /// <summary>
        /// Cadastra uma consulta no banco de dados
        /// </summary>
        /// <param name="consulta">Consulta a ser cadastrada</param>
        void Cadastrar(Consulta consulta);

        /// <summary>
        /// Altera as informações da consulta
        /// </summary>
        /// <param name="consulta">Consulta com os valores ja alterados</param>
        void Alterar(Consulta consulta);

        /// <summary>
        /// Lista todas as consultas do banco de dados
        /// </summary>
        /// <returns>Uma lista com todas as consultas</returns>
        List<Consulta> Listar();
        /// <summary>
        /// Lista todas as consultas que acontecerem entre 2 datas
        /// </summary>
        /// <param name="dataInicial">Data inicial da busca (deve ser menor do que a data final)</param>
        /// <param name="dataFinal">Data final</param>
        /// <returns>Retorna uma lista com todas as consultas que aconteceram nessa data</returns>
        List<Consulta> Listar(DateTime dataInicial,DateTime dataFinal);
    }
}
