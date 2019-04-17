using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Senai.WebApi.SpMedGroup.Repositories.EntityFramework;

namespace Senai.WebApi.SpMedGroup.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaRepository Repositorio;

        public ConsultaController() {
            Repositorio = new ConsultaRepository();
        }

        [HttpGet]
        [Route("Listar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Listar() {
            try {
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc);
            }
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = "Administrador,Medico")]
        public IActionResult Cadastrar(Consulta consulta) {
            try {
                Repositorio.Cadastrar(consulta);
                return Ok("Consulta cadastrada com suecsso");
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPut]
        [Route("Alterar")]
        [Authorize(Roles = "Administrador,Medico")]
        public IActionResult Alterar(Consulta consulta) {
            try {
                Repositorio.Alterar(consulta);
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

    }
}