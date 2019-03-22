using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Senai.WebApi.SpMedGroup.Repositories.EntityFramework;//remova o "EntityFramework" se quiser mudar para o modo normal

namespace Senai.WebApi.SpMedGroup.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EspecialidadeController : ControllerBase
    {
        private readonly IEspecialidadeRepository Repositorio;

        public EspecialidadeController() {
            Repositorio = new EspecialidadeRepository(); 
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Medico")]
        public IActionResult Listar() {
            try {
                return Ok(Repositorio.Listar());
            }catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Cadastrar(Especialidade especialidade) {
            try {
                Repositorio.Cadastrar(especialidade);
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPut]
        [Route("Alterar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Alterar(Especialidade especialidade) {
            try {
                Repositorio.Alterar(especialidade);
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }
    }
}