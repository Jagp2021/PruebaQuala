namespace Productos.Common.Interface.Repository
{
    public interface IUnitOfWork
    {
        IProductoRepository ProductoRepository { get; }
        void Commit();
        void Rollback();
        void Dispose();
    }
}
