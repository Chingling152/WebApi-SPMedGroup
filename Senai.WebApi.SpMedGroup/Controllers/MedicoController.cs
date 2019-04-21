using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Senai.WebApi.SpMedGroup.Repositories.EntityFramework;

namespace Senai.WebApi.SpMedGroup.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoRepository Repositorio;

        public MedicoController() {
            Repositorio = new MedicoRepository();
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
        public IActionResult Cadastrar(Medico medico) {
            try {
                Usuario usuario = new UsuarioRepository().Listar(medico.IdUsuario);
                if (!usuario.TipoUsuario.Equals(Enums.EnTipoUsuario.Medico)) {
                    throw new Exception("O Usuario referenciado não pode ter privilegios de um medico");
                }
                Repositorio.Cadastrar(medico);
                return Ok($"Medico {medico.Nome} cadastrado com sucesso");
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet]
        [Route("VerConsultas")]
        [Authorize(Roles = "Medico")]
        public IActionResult VerConsultas() {
            try {
                int ID = Convert.ToInt32(
                    HttpContext.User.Claims.First(i => i.Type == JwtRegisteredClaimNames.Jti).Value
                );
                return Ok(Repositorio.VerConsultas(ID).Consulta);
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPut]
        [Route("Alterar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Alterar(Medico medico) {
            try {
                Repositorio.Alterar(medico);
                return Ok($"Medico {medico.Nome} alterado com sucesso");
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }
    }
}