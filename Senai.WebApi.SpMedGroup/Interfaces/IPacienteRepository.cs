﻿using Senai.WebApi.SpMedGroup.Domains;
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
        /// Lista um Paciente no ID selecionado
        /// </summary>
        /// <param name="ID">ID do Paciente que será retornado</param>
        /// <returns>Um Paciente com todas as informações completas</returns>
        Paciente Listar(int ID);
        /// <summary>
        /// Altera as informações do paciente
        /// </summary>
        /// <param name="paciente">Paciente com os valores ja alterados</param>
        void Alterar(Paciente paciente);
        /// <summary>
        /// Mostra todas as infornações do usuario e suas consultas
        /// </summary>
        /// <param name="ID">ID Do usuario</param>
        /// <returns>Um usuario com todas as consultas relacionadas a ele</returns>
        Paciente VerConsultas(int ID);
        /* Metodo não implementado
        /// <summary>
        /// Procura um usuario que tenha o CPF inserido
        /// </summary>
        /// <param name="CPF">CPF do usuario a ser procurado</param>
        /// <returns>Retorna um usuario que tenha</returns>
        Paciente Listar(string CPF);
        */
    }
}
