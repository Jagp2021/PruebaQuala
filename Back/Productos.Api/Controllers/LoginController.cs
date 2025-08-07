using Microsoft.AspNetCore.Mvc;
using Productos.Common.Dto;
using Productos.Common.Interface.Service;

namespace Productos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]UsuarioDto usuario)
        {
            try
            {
                return Ok(_loginService.Login(usuario));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
