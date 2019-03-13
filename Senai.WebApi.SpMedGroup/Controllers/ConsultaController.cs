using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
                return BadRequest(exc);
            }
        }

        [HttpGet("Pacientes/MinhasConsultas")]
        [Authorize(Roles = "Paciente")]
        public IActionResult ListarPaciente() {
            try {
                int ID = Convert.ToInt32(//convertendo o valor inserido no token para int
                    HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value//value retorna uma string
                    // First procura o primeiro valor na array que contenha uma determinada especificação
                    // nesse caso, é a Claim que é do tipo Jti
                 );
                return Ok(Repositorio.ListarPaciente(ID));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("Medicos/MinhasConsultas")]
        [Authorize(Roles = "Medico")]
        public IActionResult ListarMedico() {
            try {
                int ID = Convert.ToInt32(
                    HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value
                );
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