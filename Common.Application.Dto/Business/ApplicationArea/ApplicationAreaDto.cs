using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Dto.Business.ApplicationArea
{
    public class ApplicationAreaDto
    {
            public Guid ApplicationAreaId { get; set; }
            public Guid ApplicationId { get; set; }
            public string AreaName { get; set; }
            public string AreaTitle { get; set; }
            public bool Active { get; set; }
            public int SortOrder { get; set; }
    }
}
