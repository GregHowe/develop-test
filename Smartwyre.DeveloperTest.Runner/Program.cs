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

        var input = new CalculateRebateRequest();
        input.ProductIdentifier = "P3";
        input.RebateIdentifier = "R1";
        input.Volume = 150;

        var result = service.Calculate(input);

        if (result.Success)
            Console.WriteLine("Success Process");
        else
            Console.WriteLine("Failed Process");

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
