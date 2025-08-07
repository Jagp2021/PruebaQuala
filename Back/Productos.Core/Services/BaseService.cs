using AutoMapper;
using Productos.Common.Interface.Repository;

namespace Productos.Core.Services
{
    public class BaseService
    {
        protected IUnitOfWork UnitOfWork;
        protected IMapper Mapper;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


    }
}
