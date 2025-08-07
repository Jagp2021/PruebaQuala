using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Productos.Common.Dto;
using Productos.Common.Interface.Service;

namespace Productos.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        [Route("ConsultarProductos")]
        public IActionResult ConsultarProductos([FromQuery] ProductoDto filtro)
        {
            try
            {
                return Ok(_productoService.ObtenerProductos(filtro));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        [HttpPost]
        [Route("GuardarProducto")]
        public IActionResult GuardarProducto([FromBody] ProductoDto producto)
        {
            try
            {
                return Ok(_productoService.GuardarProducto(producto));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        [HttpPut]
        [Route("ActualizarProducto")]
        public IActionResult ActualizarProducto([FromBody] ProductoDto producto)
        {
            try
            {
                return Ok(_productoService.ActualizarProducto(producto));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        [HttpDelete]
        [Route("EliminarProducto/{codigoProducto}")]
        public IActionResult EliminarProducto(int codigoProducto)
        {
            try
            {
                return Ok(_productoService.EliminarProducto(codigoProducto));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
