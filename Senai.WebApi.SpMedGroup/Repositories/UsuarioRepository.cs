using System;
using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories {
    /// <summary>
    /// Classe responsavel por cadastrar e listar Usuarios (independente de serm administradores , medicos ou pacientes)
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository {
        public void Cadastrar(Usuario usuario) {
            throw new NotImplementedException();
        }

        public List<Usuario> Listar() {
            throw new NotImplementedException();
        }

        public Usuario Listar(int ID) {
            throw new NotImplementedException();
        }

        public Usuario Logar(string Email, string Senha) {
            throw new NotImplementedException();
        }
    }
}
