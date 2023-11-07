using Smartwyre.DeveloperTest.DTO;
using Smartwyre.DeveloperTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Interfaces
{
    public interface IRebateService
    {
        public CalculateRebateResult Calculate(CalculateRebateRequest request);

        public void StoreCalculationResult(Rebate account);

    }
}
