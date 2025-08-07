using AutoMapper;
using Productos.Common.Dto;
using Productos.Common.Entities;
using Productos.Common.Interface.Repository;
using Productos.Common.Interface.Service;
using Productos.Common.Util;

namespace Productos.Core.Services
{
    public class ProductoService : BaseService, IProductoService
    {
        public ProductoService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public ResponseDto ObtenerProductos(ProductoDto filtro)
        {
            var response = new ResponseDto();
            try
            {
                var productos = UnitOfWork.ProductoRepository.List(filtro);
                response.Data = Mapper.Map<IEnumerable<ProductoDto>>(productos);
                response.Estado = true;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseDto GuardarProducto(ProductoDto producto)
        {
            var response = new ResponseDto();
            try
            {
                var errores = ValidarGuardadoProducto(producto);
                if (errores.Any())
                {
                    response.Estado = false;
                    response.Mensaje = string.Join(" ", errores);
                    return response;
                }

                var entity = Mapper.Map<Producto>(producto);
                UnitOfWork.ProductoRepository.Add(entity);
                UnitOfWork.Commit();
                response.Estado = true;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseDto ActualizarProducto(ProductoDto producto)
        {
            var response = new ResponseDto();
            try
            {
                var errores = ValidarActualizacionProducto(producto);
                if (errores.Any())
                {
                    response.Estado = false;
                    response.Mensaje = string.Join(" ", errores);
                    return response;
                }

                var productoBd = UnitOfWork.ProductoRepository.List(new ProductoDto { CodigoProducto = producto.CodigoProducto });
                if (productoBd.Any())
                {
                    bool validacion = false;
                    var fechaActual = productoBd.First().FechaCreacion;
                    if(fechaActual.HasValue && producto.FechaCreacion.HasValue)
                    {
                        validacion = producto.FechaCreacion.Value.Date == fechaActual.Value.Date;
                    }
                    producto.FechaCreacion = productoBd.First().FechaCreacion.HasValue && validacion ? fechaActual : Functions.ConvertirZonaHoraria(producto.FechaCreacion);
                }

                var entity = Mapper.Map<Producto>(producto);
                UnitOfWork.ProductoRepository.Update(entity);
                UnitOfWork.Commit();
                response.Estado = true;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseDto EliminarProducto(int id)
        {
            var response = new ResponseDto();
            try
            {
                UnitOfWork.ProductoRepository.Delete(id);
                UnitOfWork.Commit();
                response.Estado = true;
                return response;
            }
            catch (Exception)
            {
                throw;
            }


        }

        private List<string> ValidarGuardadoProducto(ProductoDto producto)
        {
            var errores = new List<string>();
            var codigos = UnitOfWork.ProductoRepository.List(new ProductoDto { CodigoProducto = producto.CodigoProducto });
            if (codigos.Any())
            {
                errores.Add("El código del producto ya existe.");
            }
            var nombres = UnitOfWork.ProductoRepository.List(new ProductoDto { Nombre = producto.Nombre });
            if (nombres.Any())
            {
                errores.Add("El nombre del producto ya existe.");
            }

            return errores;
        }


        private List<string> ValidarActualizacionProducto(ProductoDto producto)
        {
            var errores = new List<string>();

            var nombres = UnitOfWork.ProductoRepository.List(new ProductoDto { Nombre = producto.Nombre });
            if (nombres.Any(x => x.CodigoProducto != producto.CodigoProducto))
            {
                errores.Add("El nombre del producto ya existe.");
            }

            return errores;
        }
    }
}
