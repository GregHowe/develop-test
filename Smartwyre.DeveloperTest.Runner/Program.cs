using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.DTO;
using Smartwyre.DeveloperTest.Repository;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.RebateIncentiveType;
using System;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Interfaces;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        process();
    }

    static CalculateRebateResult process()
    {
        var serviceProvider = prepare_DependecyInjection();
        var service = serviceProvider.GetService<IRebateService>();
        var result = new CalculateRebateResult();

        try
        {
            var input = new CalculateRebateRequest();
            input.ProductIdentifier = "P9";
            input.RebateIdentifier = "R1";
            input.Volume = 150;
            result = service.Calculate(input);
        }
        catch (DontFindElementException)
        {
            result.Success = false;
        }

        Console.WriteLine("{0} Process", result.Success ? "Sucess" : "Failed");
        Console.Read();

        return result;

    }

    static ServiceProvider prepare_DependecyInjection()
    {
        var serviceProvider = new ServiceCollection()
          .AddSingleton<IRebateService, RebateService>()
          .AddSingleton<IProduct, ProductDataStore>()
          .AddSingleton<IRebate, RebateDataStore>()
           .AddSingleton<IRebateCalculation, CalculationDataStore>()
            .AddSingleton<IRebateBase, FixedRateRebate>()
          .BuildServiceProvider();
        return serviceProvider;
    }
}
