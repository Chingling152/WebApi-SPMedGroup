using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Enums;
using System.Collections.Generic;

namespace Senai.WebApi.SpMedGroup.Interfaces {
    /// <summary>
    /// Classe que lida com o cadastro e listagem de Usuarios
    /// </summary>
    public interface IUsuarioRepository {
        /// <summary>
        /// Cadastra um usuario no banco de dados
        /// </summary>
        /// <param name="usuario">Usuario a ser cadastrado</param>
        void Cadastrar(Usuario usuario);

        /// <summary>
        /// Altera todos os valores de um usuario
        /// </summary>
        /// <param name="usuario">Usuario com todos os valores ja alterados</param>
        void Alterar(Usuario usuario);

        /// <summary>
        /// Lista todos os usuarios no banco de dados
        /// </summary>
        /// <returns>Uma lista com todos os usuarios</returns>
        List<Usuario> Listar();

        /// <summary>
        /// Retorna um usuario no ID selecionado
        /// </summary>
        /// <param name="ID">ID Do Usuario a ser procurado</param>
        /// <returns></returns>
        Usuario Listar(int ID);

        /// <summary>
        /// Lista todos os usuarios de determinado tipo
        /// </summary>
        /// <param name="tipoUsuario">Tipo de usuario</param>
        /// <returns>Uma lista com todos usuarios com um nivel de privilegio especifico</returns>
        List<Usuario> Listar(EnTipoUsuario tipoUsuario);

        /// <summary>
        /// Procura um usuario com a combinação de Email e Senha
        /// </summary>
        /// <param name="Email">Email do usuario a ser procurado</param>
        /// <param name="Senha">Senha do usuario a ser procurado</param>
        /// <returns>Retorna um usuario que contenha o Email e Senha</returns>
        Usuario Logar(string Email,string Senha);
    }
}
