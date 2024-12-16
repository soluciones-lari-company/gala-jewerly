using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewerlyGala.Domain.Entities;


namespace JewerlyGala.Domain.Repositories
{
    public interface ISalePaymentRepository
    {
        public Task<Guid> CreateAsync(SalePayment payment);

    }
}
