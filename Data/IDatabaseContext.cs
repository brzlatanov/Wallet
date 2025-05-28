using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDatabaseContext
    {
        Task<decimal> GetBalanceAsync();
        Task SetBalanceAsync(decimal balance);
    }
}
