using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Smartwyre.DeveloperTest.dboContext;
using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Tests
{
    public class TestDataHelper
    {

        public dbContext dbMock;

        public TestDataHelper()
        {
            var options = new DbContextOptionsBuilder<dbContext>()
            .UseInMemoryDatabase("InMemoryDbTest")
            .Options;
            dbMock = new dbContext(options);

            dbMock.Products.AddRange(
                       new Product { Id = 1, Identifier = "P1", Price = 1, Uom = "Feet", SupportedIncentives = SupportedIncentiveType.FixedCashAmount },
                       new Product { Id = 2, Identifier = "P2", Price = 1, Uom = "Pounds", SupportedIncentives = SupportedIncentiveType.AmountPerUom },
                       new Product { Id = 3, Identifier = "P3", Price = 1, Uom = "Gallons", SupportedIncentives = SupportedIncentiveType.FixedRateRebate }
                   );

            dbMock.Rebates.AddRange(
                    new Rebate { Identifier = "R1", Amount = 1030, Percentage = 20m, Incentive = IncentiveType.FixedRateRebate },
                    new Rebate { Identifier = "R2", Amount = 5020, Percentage = 10m, Incentive = IncentiveType.FixedCashAmount },
                    new Rebate { Identifier = "R3", Amount = 6000, Percentage = 50m, Incentive = IncentiveType.AmountPerUom }
       );


            dbMock.SaveChanges();
         }


    }

}

