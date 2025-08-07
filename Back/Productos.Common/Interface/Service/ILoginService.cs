using Productos.Common.Dto;

namespace Productos.Common.Interface.Service
{
    public interface ILoginService
    {
        ResponseDto Login(UsuarioDto usuario);
    }
}
