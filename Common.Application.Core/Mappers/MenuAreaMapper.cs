using AutoMapper;
using Common.Application.Dto.Business.ApplicationArea;
using Common.Domain.Core.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Core.Mappers
{
    public class MenuAreaMapper : Profile
    {
        public MenuAreaMapper()
        {
            CreateMap<ApplicationAreaDto, ApplicationArea>();
            CreateMap<ApplicationArea, ApplicationAreaDto>();
        }
    }
}
