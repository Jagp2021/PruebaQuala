using Dapper;
using Productos.Common.Dto;
using Productos.Common.Entities;
using Productos.Common.Interface.Repository;
using Productos.Common.Util;
using System.Data;

namespace Productos.Infrastructure.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public ProductoRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public Producto GetById(int id)
        => _connection.QuerySingleOrDefault<Producto>("SELECT * FROM Users WHERE Id = @Id", new { Id = id }, _transaction)!;

        public IEnumerable<Producto> List(ProductoDto producto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@CodigoProducto", producto.CodigoProducto, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@Nombre", producto.Nombre, DbType.String, ParameterDirection.Input, 250);
            parametros.Add("@Descripcion", producto.Descripcion, DbType.String, ParameterDirection.Input, 50);
            parametros.Add("@Estado", producto.Estado, DbType.Boolean, ParameterDirection.Input);

            var storedProcedure = "JG_P_CONSULTAR_PRODUCTO";
            return _connection.Query<Producto>(
                        sql: storedProcedure,
                        param: parametros,
                        commandType: CommandType.StoredProcedure,
                        transaction: _transaction);
        }

        public void Add(Producto producto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@CodigoProducto", producto.CodigoProducto, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@Nombre", producto.Nombre, DbType.String, ParameterDirection.Input, 250);
            parametros.Add("@Descripcion", producto.Descripcion, DbType.String, ParameterDirection.Input, 50);
            parametros.Add("@ReferenciaInterna", producto.ReferenciaInterna, DbType.String, ParameterDirection.Input, 100);
            parametros.Add("@PrecioUnitario", producto.PrecioUnitario, DbType.Decimal, ParameterDirection.Input, 14, 0);
            parametros.Add("@Estado", producto.Estado, DbType.Boolean, ParameterDirection.Input);
            parametros.Add("@UnidadMedida", producto.UnidadMedida, DbType.String, ParameterDirection.Input, 50);
            parametros.Add("@FechaCreacion", Functions.ConvertirZonaHoraria(producto.FechaCreacion), DbType.DateTime, ParameterDirection.Input);

            var storedProcedure = "JG_P_GUARDAR_PRODUCTO";
            _connection.Execute(
                    sql: storedProcedure,
                    param: parametros,
                    transaction: _transaction);
        }

        public void Update(Producto producto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@CodigoProducto", producto.CodigoProducto, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@Nombre", producto.Nombre, DbType.String, ParameterDirection.Input, 250);
            parametros.Add("@Descripcion", producto.Descripcion, DbType.String, ParameterDirection.Input, 50);
            parametros.Add("@ReferenciaInterna", producto.ReferenciaInterna, DbType.String, ParameterDirection.Input, 100);
            parametros.Add("@PrecioUnitario", producto.PrecioUnitario, DbType.Decimal, ParameterDirection.Input, 14,0);
            parametros.Add("@Estado", producto.Estado, DbType.Boolean, ParameterDirection.Input);
            parametros.Add("@UnidadMedida", producto.UnidadMedida, DbType.String, ParameterDirection.Input, 50);
            parametros.Add("@FechaCreacion", producto.FechaCreacion, DbType.DateTime, ParameterDirection.Input);

            var storedProcedure = "JG_P_GUARDAR_PRODUCTO";
            _connection.Execute(
                    sql: storedProcedure,
                    param: parametros,
                    transaction: _transaction);
        }

        public void Delete(int codigoProducto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@CodigoProducto", codigoProducto, DbType.Int32, ParameterDirection.Input);

            var storedProcedure = "JG_P_ELIMINAR_PRODUCTO";
            _connection.Execute(
                    sql: storedProcedure,
                    param: parametros,
                    transaction: _transaction);
        }
    }
}
