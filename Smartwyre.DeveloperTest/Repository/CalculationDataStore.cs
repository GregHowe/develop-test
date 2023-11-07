using Smartwyre.DeveloperTest.dboContext;
using Smartwyre.DeveloperTest.DTO;
using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Repository
{
    public class CalculationDataStore : IRebateCalculation
    {
        private readonly dbContext _context = new dbContext();
        public CalculationDataStore()
        {

        }
        public CalculationDataStore(dbContext context)
        {
            _context = context;
        }

        public void StoreCalculationResult(Rebate account)
        {
            _context.Database.EnsureCreated();
            var rebate = _context.Rebates.Where(x => x.Identifier == account.Identifier).FirstOrDefault();
            rebate.Amount = account.Amount;
            _context.SaveChanges();
        }
    }
}
