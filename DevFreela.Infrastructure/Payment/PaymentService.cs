using DevFreela.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Payment
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> CheckPaymentAsync()
        {
            throw new NotImplementedException();
        }
    }
}
