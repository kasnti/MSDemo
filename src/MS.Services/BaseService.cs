using AutoMapper;
using MS.Common.IDCode;
using MS.Component.Jwt.UserClaim;
using MS.DbContexts;
using MS.UnitOfWork;

namespace MS.Services
{
    public interface IBaseService
    {
    }
    public class BaseService : IBaseService
    {
        public readonly IUnitOfWork<MSDbContext> _unitOfWork;
        public readonly IMapper _mapper;
        public readonly IdWorker _idWorker;
        public readonly IClaimsAccessor _claimsAccessor;

        public BaseService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker, IClaimsAccessor claimsAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _idWorker = idWorker;
            _claimsAccessor = claimsAccessor;
        }
    }
}
