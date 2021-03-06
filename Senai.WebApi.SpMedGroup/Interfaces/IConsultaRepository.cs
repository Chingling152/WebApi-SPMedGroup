﻿using Senai.WebApi.SpMedGroup.Domains;
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
        /// Lista todas as consultas do banco de dados
        /// </summary>
        /// <returns>Uma lista com todas as consultas</returns>
        List<Consulta> Listar();

        /// <summary>
        /// Altera as informações da consulta
        /// </summary>
        /// <param name="consulta">Consulta com os valores ja alterados</param>
        void Alterar(Consulta consulta);
    }
}
