using Common.Application.Dto.Business.ApplicationArea;
using Common.Application.Dto.Utility;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Core.Queries
{
    public record GetApplicationAreaListQuery:IRequest<BaseResponse<List<ApplicationAreaDto>>>
    {
        public int ApplicationAreaId { get; init; }
    }
}
