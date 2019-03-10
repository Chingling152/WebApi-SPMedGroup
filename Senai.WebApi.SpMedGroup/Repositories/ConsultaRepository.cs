using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories {
    /// <summary>
    /// Classe com metodos aplicados do IConsultaRepository (Sem EntityFramework)
    /// </summary>
    public class ConsultaRepository : IConsultaRepository {
        public void Alterar(Consulta consulta) {
            throw new System.NotImplementedException();
        }

        public void Cadastrar(Consulta consulta) {
            throw new System.NotImplementedException();
        }

        public List<Consulta> Listar() {
            throw new System.NotImplementedException();
        }

        public List<Consulta> ListarMedico(int ID) {
            throw new System.NotImplementedException();
        }

        public List<Consulta> ListarPaciente(int ID) {
            throw new System.NotImplementedException();
        }
    }
}
