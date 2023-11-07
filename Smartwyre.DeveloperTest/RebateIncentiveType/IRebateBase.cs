using Smartwyre.DeveloperTest.DTO;
using Smartwyre.DeveloperTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.RebateIncentiveType
{
    public interface IRebateBase
    {
        public bool validate(Product product, Rebate rebate ,CalculateRebateRequest request);
        public decimal calculateRebateAmount(Product product, Rebate rebate, CalculateRebateRequest request);

    }
}
