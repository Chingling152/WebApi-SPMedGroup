using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Senai.WebApi.SpMedGroup.Domains;
using Senai.WebApi.SpMedGroup.Interfaces;
using Senai.WebApi.SpMedGroup.ViewModel;
using Senai.WebApi.SpMedGroup.Repositories.EntityFramework;
using Senai.WebApi.SpMedGroup.Enums;
using System.Collections.Generic;

namespace Senai.WebApi.SpMedGroup.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository Repositorio;

        public UsuarioController() => Repositorio = new UsuarioRepository();

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
        [Route("Listar/{tipousuario}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Listar(int tipousuario) {
            try {
                EnTipoUsuario tipoUsuario = (EnTipoUsuario)tipousuario;
                return Ok(Repositorio.Listar(tipoUsuario));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }


        [HttpPost]
        [Route("Cadastrar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Cadastrar(Usuario usuario) {
            try {
                Repositorio.Cadastrar(usuario);
                return Ok($"Usuario cadastrado com sucesso");
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel usuario) {
            try {
                Usuario user = Repositorio.Logar(usuario.Email, usuario.Senha);

                if (user == null)
                    return NotFound("Email ou senha incorretos");

                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.TipoUsuario.ToString()),
                    new Claim("Role",user.TipoUsuario.ToString()),
                };

                var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Autenticação-SPMedGroup"));

                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "SPMedicalGroup",
                    audience: "SPMedicalGroup",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credenciais
                );

                return Ok(new {
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }
    }
}