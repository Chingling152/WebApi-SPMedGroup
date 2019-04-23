using System;
using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Senai.WebApi.SpMedGroup.Enums;
using Microsoft.EntityFrameworkCore;

namespace Senai.WebApi.SpMedGroup.Repositories.EntityFramework {
    /// <summary>
    /// Classe responsavel por cadastrar e listar Usuarios (independente de serm administradores , medicos ou pacientes)
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository {
           
        /// <summary>
        /// Altera todos os valores de um usuario 
        /// </summary>
        /// <param name="usuario">Usuario com os valores já alterados (precisa ID)</param>
        public void Alterar(Usuario usuario) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Usuario.Update(usuario);
                ctx.SaveChanges();
            }
        }

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

        public List<Usuario> Listar(EnTipoUsuario tipoUsuario) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                List<Usuario> usuarios ;
                switch (tipoUsuario) {
                    case EnTipoUsuario.Paciente:
                        usuarios = ctx.Usuario.Include(i => i.Paciente).Where(t => t.TipoUsuario.Equals(tipoUsuario)).ToList();
                        break;
                    case EnTipoUsuario.Medico:
                        usuarios = ctx.Usuario.Include(i => i.Medico).Where(t => t.TipoUsuario.Equals(tipoUsuario)).ToList();
                        break;
                    case EnTipoUsuario.Administrador:
                        usuarios = ctx.Usuario.Where(t => t.TipoUsuario.Equals(tipoUsuario)).ToList();
                        break;
                    default:
                        throw new NullReferenceException("Valor invalido para tipo e usuario");
                }
                if(usuarios.Count < 1) {
                    throw new NullReferenceException("Não existe nenhum usuario com esse tipo cadastrado");
                }
                return usuarios;
            }
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
