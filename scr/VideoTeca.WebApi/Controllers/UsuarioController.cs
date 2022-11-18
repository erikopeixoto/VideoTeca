using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Dtos;

namespace VideoTeca.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IMapper _mapper;

        public UsuarioController(IConfiguration config,
                              UserManager<Usuario> userManager,
                              SignInManager<Usuario> signInManager,
                              IMapper mapper)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;

            //var migracaoPendente = contexto.Database.GetPendingMigrations();
            //if (migracaoPendente.Count() > 0)
            //{
            //    contexto.Database.Migrate();
            //}
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            return Ok(new UserDto());
        }

        [HttpPost("Registro")]
        [AllowAnonymous]
        public async Task<IActionResult> Registro(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<Usuario>(userDto);

                var result = await _userManager.CreateAsync(user, userDto.Senha);

                var userToReturn = _mapper.Map<UserDto>(user);

                if (result.Succeeded)
                {
                    return Created("GetUser", userToReturn);
                }

                return BadRequest(result.Errors);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userLogin.login);
                var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.senha, false);
                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users
                        .FirstOrDefaultAsync(u => u.Nome == userLogin.login.ToUpper());

                    var userToReturn = _mapper.Map<UserLoginDto>(appUser);

                    return Ok(new
                    {
                        token = GerarJWToken(appUser),
                        user = userToReturn
                    });
                }

                return Unauthorized();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
            }
        }
        private string GerarJWToken(Usuario usuario)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Login.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

            //var roles = await _userManager.GetRolesAsync(usuario);

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            var key = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(_config.GetSection("JWTConfig:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
