using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Senai.WebApi.SpMedGroup.Repositories.EntityFramework;

namespace Senai.WebApi.SpMedGroup.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class ClinicaController : ControllerBase
    {
        private readonly IClinicaRepository Repositorio;

        public ClinicaController() {
            Repositorio = new ClinicaRepository();
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

        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Cadastrar(Clinica clinica) {
            try {
                Repositorio.Cadastrar(clinica);
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }


        [HttpPut]
        [Route("Alterar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Alterar(Clinica clinica) {
            try {
                Repositorio.Alterar(clinica);
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }
    }
}