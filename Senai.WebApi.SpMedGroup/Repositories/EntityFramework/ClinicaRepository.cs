using System;
using System.Linq;
using System.Collections.Generic;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;

namespace Senai.WebApi.SpMedGroup.Repositories.EntityFramework {
    /// <summary>
    /// Metodos com cadastro e listagem de clinicas (EntityFramework)
    /// </summary>
    public class ClinicaRepository : IClinicaRepository {

        /// <summary>
        /// Altera os valores de uma clinica no banco de dados (Procura a mesma pelo ID)
        /// </summary>
        /// <param name="clinica">Clinica com os valores ja alterados</param>
        public void Alterar(Clinica clinica) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Clinica.Update(clinica);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Cadastra uma clinica no banco de dados
        /// </summary>
        /// <param name="clinica">Clinica a ser cadastrada</param>
        public void Cadastrar(Clinica clinica) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                ctx.Clinica.Add(clinica);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Lista todas as clinicas do banco de dados
        /// </summary>
        /// <returns>Uma lista com todas as informações de todas as clinicas</returns>
        public List<Clinica> Listar(){
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                if(ctx.Clinica.Count()>0)
                    return ctx.Clinica.ToList();
            } 
            throw new Exception("Não há nenhuma clinica cadastrada no banco de dados");
        }

        /// <summary>
        /// Lista todas as informações e medicos de uma clinica
        /// </summary>
        /// <param name="ID">ID da clinica selecionada</param>
        /// <returns>Retorna todas as informações da clinica selecionada , se ela não existir , retorna ua exceção</returns>
        public Clinica Listar(int ID) {
            using (SpMedGroupContext ctx = new SpMedGroupContext()) {
                Clinica clinica = ctx.Clinica.Find(ID);
                if(clinica != null)
                    return clinica;
            }
            throw new Exception($"Não há nenhuma clinica cadastrada no banco de dados com o id {ID}");
        }
    }
}
