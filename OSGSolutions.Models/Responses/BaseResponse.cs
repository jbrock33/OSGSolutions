using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGSolutions.Models.Responses
{
    public abstract class BaseResponse
    {
        public bool IsSuccessful { get; set; }
    }
}
