using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Senai.WebApi.SpMedGroup.Repositories.EntityFramework;

namespace Senai.WebApi.SpMedGroup.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaRepository Repositorio;

        public ConsultaController() {
            Repositorio = new ConsultaRepository();
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Listar() {
            try {
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("Paciente/{ID}")]
        [Authorize(Roles = "1")]
        public IActionResult ListarPaciente(int ID) {
            try {
                return Ok(Repositorio.ListarPaciente(ID));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Roles = "Medico")]
        [HttpGet("Medico/{ID}")]
        public IActionResult ListarMedico(int ID) {
            try {
                return Ok(Repositorio.ListarMedico(ID));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = "Administrador,Medico")]
        public IActionResult Cadastrar(Consulta consulta) {
            try {
                Repositorio.Cadastrar(consulta);
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPut]
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