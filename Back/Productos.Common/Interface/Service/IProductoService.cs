using Productos.Common.Dto;

namespace Productos.Common.Interface.Service
{
    public interface IProductoService
    {
        ResponseDto ObtenerProductos(ProductoDto filtro);
        ResponseDto GuardarProducto(ProductoDto producto);
        ResponseDto ActualizarProducto(ProductoDto producto);
        ResponseDto EliminarProducto(int id);
    }
}
