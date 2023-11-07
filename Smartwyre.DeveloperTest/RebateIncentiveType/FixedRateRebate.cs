using Smartwyre.DeveloperTest.DTO;
using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.RebateIncentiveType
{
    public class FixedRateRebate : IRebateBase
    {
        public bool validate(Product product, Rebate rebate, CalculateRebateRequest request)
        {
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
            {
                return false;
            }

            if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
            {
                return false;
            }

            return true;
        }

        public decimal calculateRebateAmount(Product product, Rebate rebate, CalculateRebateRequest request)
        {
            return product.Price * rebate.Percentage * request.Volume;
        }
    }
}
