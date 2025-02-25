using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Dto.Utility
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public Meta MetaData { get; set; }
    }

    public class Meta
    {
        public bool HasErrors { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public Exception Exception { get; set; }
    }
}
