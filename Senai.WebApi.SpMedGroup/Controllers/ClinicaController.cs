using System;
using System.Linq;
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
        public IActionResult Listar() {
            try {
                return Ok(Repositorio.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("Listar{quant}")]
        public IActionResult ListarQuantidade(int quant) {
            try {
                if (quant <= 0)
                    quant = 1;
                return Ok(Repositorio.Listar().Take(quant));
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
                return Ok($"Clinica {clinica.NomeFantasia} cadastrada com sucesso");
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
                return Ok($"Clinica {clinica.NomeFantasia} alterada com sucesso");
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }
    }
}