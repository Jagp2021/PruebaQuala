using Productos.Common.Dto;
using Productos.Common.Entities;

namespace Productos.Common.Interface.Repository
{
    public interface IProductoRepository
    {
        IEnumerable<Producto> List(ProductoDto producto);
        void Add(Producto producto);
        void Update(Producto producto);
        void Delete(int id);
    }
}
