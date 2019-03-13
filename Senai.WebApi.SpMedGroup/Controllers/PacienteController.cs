using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Senai.WebApi.SpMedGroup.Repositories.EntityFramework;
using System.IdentityModel.Tokens.Jwt;

namespace Senai.WebApi.SpMedGroup.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepository Repositorio;

        public PacienteController() {
            Repositorio = new PacienteRepository();
        }

        [HttpGet]
        [Authorize(Roles = "Medico,Administrador")]
        public IActionResult Listar() {
            try {
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("VerConsultas")]
        [Authorize(Roles = "Paciente,Administrador")]
        public IActionResult VerConsultas() {
            try {
                int ID = Convert.ToInt32(
                    HttpContext.User.Claims.First(x=> x.Type == JwtRegisteredClaimNames.Jti).Value
                );
                return Ok(Repositorio.VerConsultas(ID));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Cadastrar(Paciente paciente) {
            try {
                if(paciente.DataNascimento > DateTime.Now) {
                    throw new Exception("Data de nascimento invalida");
                }
                Repositorio.Cadastrar(paciente);
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }
    }
}