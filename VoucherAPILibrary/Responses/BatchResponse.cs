using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherAPILibrary.Utils;

namespace VoucherAPILibrary.Responses
{
    public class BatchResponse
    {
        public long NoCreated { get; set; }
        public ServiceResponse serviceResponse {get;set;}

        public BatchResponse(long noCreated, ServiceResponse serviceResponse)
        {
            NoCreated = noCreated;
            this.serviceResponse = serviceResponse ?? throw new ArgumentNullException(nameof(serviceResponse));
        }
    }
}
