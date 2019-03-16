using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Senai.WebApi.SpMedGroup.Repositories;

namespace Senai.WebApi.SpMedGroup.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ClinicaController : ControllerBase
    {
        private readonly IClinicaRepository Repositorio;

        public ClinicaController() {
            Repositorio = new ClinicaRepository();
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
    }
}