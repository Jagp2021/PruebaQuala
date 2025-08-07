using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Productos.Common.Dto;
using Productos.Common.Interface.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Productos.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _config;

        public LoginService(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public ResponseDto Login(UsuarioDto usuario)
        {
            if (!ValidarUsuario(usuario))
            {
                return new ResponseDto
                {
                    Estado = false,
                    Mensaje = "Usuario o contraseña incorrectos"
                };
            }
            return new ResponseDto
            {
                Estado = true,
                Data = GenerateToken(usuario)
            };  
        }

        private string GenerateToken(UsuarioDto usuario)
        {
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Usuario!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool ValidarUsuario(UsuarioDto usuario)
        {
            Dictionary<string, string> usuarios = new Dictionary<string, string>
            {
                { "admin", "admin123" },
                { "user", "user123" }
            };

            return usuarios.Any(u => u.Key == usuario.Usuario && u.Value == usuario.Password);

        }
    }
}
