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
    public class AmountPerUom : IRebateBase
    {
        public bool validate(Product product, Rebate rebate, CalculateRebateRequest request)
        {
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
            {
                return false;
            }

            if (rebate.Amount == 0 || request.Volume == 0)
            {
                return false;
            }

            return true; 

        }

        public decimal calculateRebateAmount(Product product, Rebate rebate, CalculateRebateRequest request)
        {
            return rebate.Amount * request.Volume;
        }
    }
}
