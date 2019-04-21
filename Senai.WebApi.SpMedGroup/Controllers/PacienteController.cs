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
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepository Repositorio;

        public PacienteController() {
            Repositorio = new PacienteRepository();
        }

        [HttpGet]
        [Route("Listar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Listar() {
            try {
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet]
        [Route("VerConsultas")]
        [Authorize(Roles = "Paciente")]
        public IActionResult VerConsultas() {
            try {
                int ID = Convert.ToInt32(
                    HttpContext.User.Claims.First(x=> x.Type == JwtRegisteredClaimNames.Jti).Value
                );
                return Ok(Repositorio.VerConsultas(ID).Consulta);
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
                return Ok($"Paciente {paciente.Nome} cadastrado com sucesso");
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPut]
        [Route("Alterar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Alterar(Paciente paciente) {
            try {
                Repositorio.Alterar(paciente);
                return Ok($"Paciente {paciente.Nome} alterado com sucesso");
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }
    }
}
