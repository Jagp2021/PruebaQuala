using Productos.Common.Interface.Repository;
using Productos.Infrastructure.Context;
using System.Data;

namespace Productos.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public IProductoRepository ProductoRepository { get; }

        public UnitOfWork(DapperContext context)
        {
            _connection = context.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            ProductoRepository = new ProductoRepository(_connection, _transaction);
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }

}
