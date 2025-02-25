using AutoMapper;
using Common.Application.Core.Queries;
using Common.Application.Dto.Business.ApplicationArea;
using Common.Application.Dto.Utility;
using Common.Infrastructure.Core.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Application.Core.Handlers
{
    public class GetApplicationAreaListHandler : IRequestHandler<GetApplicationAreaListQuery, BaseResponse<List<ApplicationAreaDto>>>
    {
        private readonly IBaseUnitOfWork _unitOfWork;
        // private readonly ICommonUnitOfWork _commonUnitOfWork;
        private readonly IMapper _mapper;
        public GetApplicationAreaListHandler(IBaseUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<ApplicationAreaDto>>> Handle(GetApplicationAreaListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var applicationAreaList = await _unitOfWork.ApplicationAreas.GetAllAsync();

                var applicationAreaDtoList = _mapper.Map<List<ApplicationAreaDto>>(applicationAreaList);

                return new BaseResponse<List<ApplicationAreaDto>>() { Data = applicationAreaDtoList };
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
