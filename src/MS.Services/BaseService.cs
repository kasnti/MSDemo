using AutoMapper;
using Microsoft.Extensions.Localization;
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
        public readonly IStringLocalizer _localizer;

        public BaseService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker, IClaimsAccessor claimsAccessor, IStringLocalizer localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _idWorker = idWorker;
            _claimsAccessor = claimsAccessor;
            _localizer = localizer;
        }
    }
}
