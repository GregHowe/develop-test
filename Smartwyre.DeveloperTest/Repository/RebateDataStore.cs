using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;
using Smartwyre.DeveloperTest.dboContext;
using Microsoft.EntityFrameworkCore;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebate
{
    private readonly dbContext _context = new dbContext();
    public RebateDataStore()
    {
    }

    public RebateDataStore(dbContext context)
    {
        _context = context;
    }

    public Rebate GetRebate(string rebateIdentifier)
    {
        var rebate = new Rebate();
        _context.Database.EnsureCreated();
        rebate = _context.Rebates.Where(x => x.Identifier == rebateIdentifier).FirstOrDefault();
        return rebate;

    }

}
