using Smartwyre.DeveloperTest.dboContext;
using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProduct
{
    private readonly dbContext _context = new dbContext();

    public ProductDataStore()
    {

    }
    public ProductDataStore(dbContext context)
    {
        _context = context;
    }

    public Product GetProduct(string productIdentifier)
    {
        var product = new Product();
        _context.Database.EnsureCreated();
        product = _context.Products.Where(x => x.Identifier == productIdentifier).FirstOrDefault();
        return product;

    }
}
