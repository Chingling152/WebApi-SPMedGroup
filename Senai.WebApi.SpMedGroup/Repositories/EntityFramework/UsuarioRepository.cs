using System;
using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories.EntityFramework {
    /// <summary>
    /// Classe responsavel por cadastrar e listar Usuarios (independente de serm administradores , medicos ou pacientes)
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository {

        /// <summary>
        /// Cadastra um usuario no banco de dados
        /// </summary>
        /// <param name="medico">Usuario a ser cadastrado</param>
        public void Cadastrar(Usuario usuario) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Usuario.Add(usuario);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Lista todos os usuarios do banco de dados
        /// </summary>
        /// <returns>Uma lista com todos os usuarios</returns>
        public List<Usuario> Listar() => new SpMedGroupContext().Usuario.ToList();

        /// <summary>
        /// Seleciona um usuario no ID selecionado
        /// </summary>
        /// <param name="ID">ID do usuario a ser selecionado</param>
        /// <returns>Um usuario, se não for encontrado , retorna Null</returns>
        public Usuario Listar(int ID) {
            Usuario usuario = new SpMedGroupContext().Usuario.Find(ID);
            if(usuario == null)
                throw new NullReferenceException($"O Usuario no ID {ID} não existe");

            return usuario;
        }

        /// <summary>
        /// Procura um usuario que contenha o Email e Senha inseridos
        /// </summary>
        /// <param name="Email">Email do usuario selecionado</param>
        /// <param name="Senha">Senha do usuario selecionado</param>
        /// <returns>Retorna um usuario , se não existir Email/Senha cadastrado no banco de dados , retorna null</returns>
        public Usuario Logar(string Email, string Senha) => new SpMedGroupContext().Usuario.ToList().Find(X => X.Email == Email && X.Senha == Senha);
    }
}
